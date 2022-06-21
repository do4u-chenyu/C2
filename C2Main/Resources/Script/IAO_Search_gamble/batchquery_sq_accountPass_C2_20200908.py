# -*- coding: utf-8 -*-

from subprocess import Popen, PIPE
import logging
from os import mkdir
from os.path import join
import os
import re
import urllib
import datetime
import sys
from optparse import OptionParser
reload(sys)
sys.setdefaultencoding('utf-8')

def queryclient(data_path,keyWords,startTime,endTime,queryType):
    batch = []
    cont_flag = False
    cont_end_flag = True
    content = ''
    end_item = '_QUERY_MATCHTERMS'
    with open(join(data_path, 'result.log'), 'a+') as f:
        cmd = [
            '/home/search/sbin/queryclient',
            '--server', '127.0.0.1',
            '--port', '9870',
            '--querystring', keyWords,
            '--start', startTime,
            '--end', endTime,
            '--contextlen', '10000',
            '--maxcount', '2147483647'
        ]
        req = Popen(". /home/search/search_profile && {0}".format(" ".join(cmd)), shell=True, stdout=PIPE)
        LOGGER.info('QUERYTIME:{0}_{1}\n wait...'.format(startTime,endTime)) 
        #req = Popen(['/home/search/sbin/queryclient','--server','127.0.0.1','--port', '9870','--querystring', keyWords,'--start', startTime,'--end', endTime,'--contextlen','10000','--maxcount', '10000'],stdout=PIPE)
        for line in req.stdout:
            if queryType == 'gun':
                f.write(line)
            line  = line.replace('\x1a','')
            if line == '\n' and cont_flag and cont_end_flag:
                cont_flag = False
                if batch != []:
                    data = dict(batch)
                    if queryType == 'gun' and (data.get('USERNAME',0) == 0 or data.get('PASSWORD',0) == 0):
                        content = data.get('_QUERY_CONTENT')
                        key,value = 'USERNAME',get_UserKeyfromEd(content)[0]
                        data[key] = value
                        key,value = 'PASSWORD',get_UserKeyfromEd(content)[1]
                        data[key] = value
                        data['_QUERY_CONTENT'] = re.sub('[\n&; \x1a]','',content)
                    yield data
                    batch = []
            elif ':' in line and (cont_end_flag  or '_QUERY_MATCHTERMS' in  line):
                k, v = map(lambda x: x.strip(), line.split(':', 1))
                if k == end_item:
                    # cont_end_flag = True
                    batch.append(('_QUERY_CONTENT',content))
                    content = ''
                    cont_end_flag = True
                elif k == '_QUERY_CONTENT':
                    cont_flag = True
                    cont_end_flag = False
                    content = v
                else:
                    batch.append((k, v))
            else:
                if not cont_end_flag:
                    content += line
                    
                 

def get_UserKeyfromEd(USERPW):
    user_key     = ['username']
    password_key = ['password']
    other_key    = {'safecode':['safepass','code']}
    temp_dict = {}
    key_list = []
    blankchar=re.compile(ur"[\r\n\t ]")
    for line in re.split('[\n&;]+',USERPW):
        try:
            [key, value] = line.split("=")
            if len(value.strip()) < 1:
                continue
            temp_dict[key.lower()] = value
            key_list.append(key.lower())
        except:
            pass
    otherkey_list=other_key.keys()
    if len(key_list) < 2:
        return [""]*(2+len(otherkey_list))
    #获取每一个key值
    user_dis=[]
    pass_dis=[]
    other_dis={}
    for key in otherkey_list:
        other_dis[key]=[]
    for key in key_list:
        temp_dis = []
        for userkey in user_key:
            temp_dis.append(levenshtein(userkey, key))
        user_dis.append(min(temp_dis))
        temp_dis = []
        for passwordkey in password_key:
            temp_dis.append(levenshtein(passwordkey, key))
        pass_dis.append(min(temp_dis))
        for othkey in otherkey_list:
            temp_dis = []
            for codekey in other_key[othkey]:
                temp_dis.append(levenshtein(codekey, key))
            other_dis[othkey].append(min(temp_dis))
    # 获取最优解释组合
    user_mindis = min(user_dis)
    user_index = user_dis.index(user_mindis)
    pass_mindis = min(pass_dis)
    pass_index = pass_dis.index(pass_mindis)
    pass_key = key_list[pass_index]
    user_key = key_list[user_index]
    othcode_key={}
    othcode_mindis={}
    for key in other_key.keys():
        othcode_mindis[key] = min(other_dis[key])
        othcode_index = other_dis[key].index(othcode_mindis[key])
        othcode_key[key] = key_list[othcode_index]
    if user_index == pass_index:
        user_dis[user_index] = 100
        temp_user_mindis = min(user_dis)
        pass_dis[pass_index] = 100
        temp_pass_mindis = min(pass_dis)
        if (user_mindis + temp_pass_mindis) < (temp_user_mindis + pass_mindis):
            pass_mindis = temp_pass_mindis
            pass_key = key_list[pass_dis.index(temp_pass_mindis)]
        else:
            user_mindis = temp_user_mindis
            user_key = key_list[user_dis.index(temp_user_mindis)]
    user_str = ""
    pass_str = ""
    othercode_str=[]
    if(user_mindis < 0.7):
        user_str = user_key + "=" + temp_dict[user_key]
    if(pass_mindis < 0.7):
        pass_str = pass_key + "=" + temp_dict[pass_key]
        try:
            pass_str = urllib.unquote(pass_str)
            if(len(blankchar.findall(pass_str))):
                pass_str = pass_key + "=" + temp_dict[pass_key]
        except:
            pass
    for key in othcode_key.keys():
        if(othcode_mindis[key]<0.7):
            othercode_str.append(othcode_key[key]+"="+temp_dict[othcode_key[key]])
        else:
            othercode_str.append("")
    return user_str,pass_str,"\t".join(othercode_str)

##计算字符串相似度
def levenshtein(first, second):
    if len(first) > len(second):
        first, second = second, first
    if len(first) == 0:
        return len(second)
    if len(second) == 0:
        return len(first)
    first_length = len(first) + 1
    second_length = len(second) + 1
    distance_matrix = [range(second_length) for x in range(first_length)]
    # print distance_matrix
    for i in range(1, first_length):
        for j in range(1, second_length):
            deletion = distance_matrix[i - 1][j] + 1
            insertion = distance_matrix[i][j - 1] + 1
            substitution = distance_matrix[i - 1][j - 1]
            if first[i - 1] != second[j - 1]:
                substitution += 1
            distance_matrix[i][j] = min(insertion, deletion, substitution)
    a = distance_matrix[first_length - 1][second_length - 1]
    b = round(1.0 * a / max(len(first), len(second)), 6)
    return b

##日志文件打印
def init_logger(logname,filename,logger_level = logging.INFO):
    logger = logging.getLogger(logname)
    logger.setLevel(logger_level)
    fh = logging.FileHandler(filename)
    fh.setLevel(logging.DEBUG)
    ch = logging.StreamHandler()
    ch.setLevel(logging.DEBUG)
    formatter = logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s')
    fh.setFormatter(formatter)
    ch.setFormatter(formatter)
    logger.addHandler(fh)
    logger.addHandler(ch)
    return logger

##文件压缩
def zip_result(DATA_PATH,ZIP_PATH):
    pipe = Popen(['tar', '-zcvf', ZIP_PATH, DATA_PATH,  '--remove-files'], stdout=PIPE, stderr=PIPE)
    ##tar -zcf - test |openssl des3 -salt -k 'password' | dd of=test.tgz
    ##cmd = ['tar', '-zcvf -', DATA_PATH, '--remove-files |openssl des3 -salt -k', PASSWORD, '|dd of={}'.format(ZIP_PATH)]
    # LOGGER.info('cmd:{}'.format(' '.join(cmd)))
    # pipe = Popen(' '.join(cmd),shell=True,stdout=PIPE)
    out, err = pipe.communicate()
    if pipe.returncode:
        LOGGER.warning("Compress dirs failed with error code: {0}".format(pipe.returncode))
        LOGGER.warning(err.decode())
    else:
        LOGGER.info("Compress dirs success!.")

def encrypTion(path):
    with open(path,'rb') as f:
        data = f.read()
    data = bytes(PASSWORD) + data[4:] 
    with open(path,'wb') as f:
        f.write(data)
##结果输出str1
def save(items,keyWords,data_path,path,querytype,startTime,endTime):
    auth = []
    with open(join(data_path,path),'w') as f:
        f.write('\t'.join(items)+'\n')
        for data in queryclient(data_path,keyWords,startTime,endTime,querytype):
            auth = list(set(auth + [data.get('AUTH_ACCOUNT','')])-{''})
            f.write('\t'.join([data.get(item,'') for item in items]) + '\n')
    return auth   

def main(config_dict):
    QUERY_TYPE = 'gun'
    [KEY_YALIE, ALL_ITEMS, OUT_HTTP, startTime, KEY_LIEYOU, OUT_PASSWORD, KEY_YLIE, DATA_PATH, AUTH_ITEMS, endTime] = config_dict.values()
    ZIP_PATH =  areacode + DATA_PATH + defaultStart + '_' + defaultEnd + '.tgz.tmp'
    KEY_WORDS = "'" + ' OR '.join(KEY_YLIE + KEY_LIEYOU + KEY_YALIE) + "'"       
    try:
        auth = save(ALL_ITEMS,KEY_WORDS,DATA_PATH,OUT_PASSWORD,QUERY_TYPE,startTime,endTime)
        LOGGER.info('QUERY_GUN END...wait Next...')
        if len(auth) > 0:
            auth_key = 'AUTH_ACCOUNT:'+' OR AUTH_ACCOUNT:'.join(auth)
            LOGGER.info('QUEY_AUTH START...QUERY:{0}\n wait....'.format(auth_key))
            auth_key = "'" +  auth_key  + "'"
            QUERY_TYPE = 'auth'
            auth = save(AUTH_ITEMS,auth_key,DATA_PATH,OUT_HTTP,QUERY_TYPE,secondeStart,endTime)
        LOGGER.info('QUERY END...')
        zip_result(DATA_PATH,ZIP_PATH)
        #encrypTion(ZIP_PATH)
        os.rename(ZIP_PATH,ZIP_PATH.replace('.tmp',''))
    except Exception,e:
        LOGGER.info('QUERY_ERROR-{0}'.format(e))
    
if __name__ == '__main__':
    ##Program description
    usage = 'python bathquery_gun_password.py --start [start_time] --end [end_time] --areacode [areacode]'
    areaformat = '<areacode> xxxxxx eg:530000'
    dataformat = '<time>: yyyyMMddhhmmss eg:20180901000000'
    parser = OptionParser(usage)
    parser.add_option('--start',dest = 'startTime',help = dataformat)
    parser.add_option('--end',dest = 'endTime',help = dataformat)
    parser.add_option('--areacode',dest = 'areacode',help = areaformat)
    ##get input Time  parameter
    option,args = parser.parse_args()
    startTime   = option.startTime
    endTime     = option.endTime
    areacode    = option.areacode
    ##set default Time[ one year]
    NowTime = datetime.datetime.now()
    timedelta = datetime.timedelta(days = 90)
    defaultStart = (NowTime - timedelta).strftime("%Y%m%d%H%M%S")
    defaultEnd   = NowTime.strftime("%Y%m%d%H%M%S")
    timedelta    = datetime.timedelta(days = 10)
    secondeStart = (NowTime - timedelta).strftime("%Y%m%d%H%M%S")
    ##check time input(if NONE:set defaultTime)
    if startTime is None and endTime is None:
            startTime = defaultStart
            endTime   = defaultEnd
    if len(startTime) !=14  or len(endTime) !=14:
            print 'TimeData error:'+ dataformat
            sys.exit(1)
    if areacode is None:
        areacode = '000000'
    if  len(areacode) !=6:
        LOGGER.info('areacode error:'+ areaformat)
        sys.exit(1)
    ##PASSWORD = 'fenghuohuofeng' + NowTime.strftime("%Y%m%d")
    CONFIG_DICT = {
        #'DATA_PATH': areacode + '_queryResult_sq_' + defaultStart + '_' + defaultEnd,
        'DATA_PATH': '_queryResult_sq_',
        'OUT_PASSWORD' : 'sq_out' + '.txt',
        'OUT_HTTP': 'out_http',
        'ALL_ITEMS' : ['_HOST','AUTH_ACCOUNT','AUTH_TYPE','CAPTURE_TIME','STRDST_IP','STRSRC_IP','DST_PORT','SRC_PORT','CONTENT','USERNAME','PASSWORD'],
        'AUTH_ITEMS' : ['_HOST','AUTH_ACCOUNT','AUTH_TYPE','CAPTURE_TIME','FROM_PROTYPE','IM_TYPE','UPAREAID','USERNAME'],
        'KEY_YLIE' : ['www.ylie' + str(i) + '.com' for i in range(24,100)],
        'KEY_LIEYOU' : ['www.lieyou' + str(i) + '.com' for i in range(24,100)],
        'KEY_YALIE' : ['www.yalie' + str(i) + '.com' for i in range(24,100)] + ['www.soubao' + str(i) + '.com' for i in range(9,100)],
        'startTime' : startTime,
        'endTime'   : endTime,
    }
    
    mkdir(CONFIG_DICT['DATA_PATH'])
    LOGGER = init_logger('queryclient_logger',join(CONFIG_DICT['DATA_PATH'],'running.log'))
    main(CONFIG_DICT)

