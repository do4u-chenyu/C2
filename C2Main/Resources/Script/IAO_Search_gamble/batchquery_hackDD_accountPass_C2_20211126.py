# -*- coding: utf-8 -*-
"""
Created on Mon Oct 29 09:08:44 2018
  version 0621:
      增加根据登陆控件和控件值生成keyword功能，实现函数：prdoucKey
  version 0628:
      增加查询：
          去除登陆控件值；
          挑选 只包含admin的url 
  version 0708:
      增加线程时间检测功能
      增加文件》50M即压缩一次功能
  version 0803:
       修改了脚本临时文件命名
       修改了脚本压缩加密方式
  version 0224:
       增加机场查询脚本
@author: Administrator
"""
from Queue import Queue
from threading import Thread
from subprocess import Popen,PIPE
import time
import urllib
import re
import io 
import sys
import datetime
import os
import itertools
import logging
import urllib2
from urllib import unquote
from optparse import OptionParser
reload(sys)
sys.setdefaultencoding('utf-8')
#################

class BatchQuery:
    def __init__(self,data_path,startTime,endTime,all_items):
        self.data_path = data_path
        self.startTime = startTime
        self.endTime   = endTime
        self.all_items = all_items

    def queryclient(self,keyWords,queryType):
        batch = []
        cont_flag = False
        cont_end_flag = True
        content = ''
        end_item = '_QUERY_MATCHTERMS'
        with open(os.path.join(self.data_path,'result1.log'),'a+') as f:
            cmd = [
                '/home/search/sbin/queryclient',
                '--server', '127.0.0.1',
                '--port', '9870',
                '--querystring', '"{}"'.format(keyWords),
                '--start', self.startTime,
                '--end', self.endTime,
                '--contextlen', '10000',
                '--maxcount', '1000000'
            ]
            req = Popen(". /home/search/search_profile && {}".format(" ".join(cmd)), shell=True, stdout=PIPE)
            print ". /home/search/search_profile && {}".format(" ".join(cmd))
            LOGGER.info('QUERYTIME:{0}_{1}\n wait...'.format(self.startTime,self.endTime)) 
            #req = Popen(['/home/search/sbin/queryclient','--server','127.0.0.1','--port', '9870','--querystring', keyWords,'--start', startTime,'--end', endTime,'--contextlen','10000','--maxcount', '10000'],stdout=PIPE)
            for line in req.stdout:
                if queryType == 'airport':
                    f.write(line)
                line  = line.replace('\x1a','')
                if line == '\n' and cont_flag and cont_end_flag:
                    cont_flag = False
                    if batch != []:
                        data = dict(batch)
                        if data.get('USERNAME',0) == 0 or data.get('PASSWORD',0) == 0:
                            content = data.get('_QUERY_CONTENT')
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
                        content = v + ';'
                    else:
                        batch.append((k, v))
                else:
                    if not cont_end_flag:
                        content += line + ';'

    def ext_mainfile(self, file_url):
        """
        处理.http文件体
        @param input_path: 输入文件路径
        @return: 抽取的规则结果字典
        """
        try:
            req = urllib2.Request(file_url)
            response = urllib2.urlopen(req, timeout=2)
            content = response.read().decode()
            pload = content.split('\n')[-1].replace('\t','').replace('\r','').replace('\n','').strip()
            method = content.split(' ')[0]
            return pload.strip(), method,content

        except Exception, e:
            return 'http文件获取失败','http文件获取失败','http文件获取失败'


    def get_airport_user(self, content):
        user_key     = ['email','account','uid','name','phone','username','userid']
        password_key = ['passwd','password','userpwd','pwd','userpass']
        user_str = ''
        pass_str = ''
        for part in re.split(r'\.\.\.',content):
            for line in re.split('[\r\n&;]+',part):
                try:
                    [key, value] = line.split("=")
                    if len(value.strip()) < 1:
                        continue
                    if key in user_key:
                        user_str = value
                    elif key in password_key:
                        pass_str = value
                except:
                    pass
        return user_str,pass_str

    def get_position(self):
        dict1 = {}
        for line in kwl2:
            l = line.strip().split('\t')
            if (l[1] not in dict1.keys()):
                dict1[l[1]] = []
                dict1[l[1]].append(l[0])
            else:
                dict1[l[1]].append(l[0])
        # print(dict1)
        return dict1

    def find_keyword_position(self,kw, d):
        lk = []
        for k, v in d.items():
            if kw in v:
                lk.append(k)

        lv = kw.strip().split(' ')
        return lv, lk

    def position_match(self,data, KEY_WORDS, pload, d):
        lv, lk = self.find_keyword_position(KEY_WORDS, d)
        for i in lk:
            flag = 0
            if (i == 'PAYLOAD'):
                k = pload
            else:
                k = data.get(i, '')
            for j in lv:
                if j not in k:
                    flag = 1
                    break
            if (flag == 0):
                return data, pload
        pload = 'http文件获取失败'
        return data, pload

    

    def handle_res(self):
        ##df = pd.read_csv(r'C:\Users\Administrator\Desktop\ws_out.txt', sep='\t', dtype=str, error_bad_lines=False)
        ##df1 = df[['_HOST', '_RELATIVEURL', 'PAYLOAD']]
        ##df2 = df1.drop_duplicates()
        # print(df2)
        ##df2.to_csv('qw_res.txt', sep='\t', index=False, header=False)
        tmplist = []
        out_file = 'ws_out.txt'
        result_file = 'qw_res.txt'
        with open(os.path.join(self.data_path, out_file), "r") as f:
            with open(os.path.join(self.data_path,result_file), "w") as f1:
                lines = f.readlines()
                for line in lines[1:]:  ##跳过第一行title
                    line = line.strip('\n')
                    ##data = "{0}\t{1}{\t}{2}\n".format(line.split('\t')[8],line.split('\t')[9],line.split('\t')[16])
                    try:
                        data = line.split('\t')[7] + '\t' + line.split('\t')[8] + '\t' + line.split('\t')[15] + '\n'
                    except:
                        data = ""
                    tmplist.append(data)
                tmplist = list(set(tmplist))  ##去重
                for line in tmplist:
                    f1.writelines(line)
            f1.close()
        f.close()

        qw_result = io.open(os.path.join(self.data_path,result_file), mode='r', encoding='utf-8').readlines()
        data1 = io.open(os.path.join(self.data_path,'request_post.txt'), mode='wb+')
        data1.write('url' + '\t' + 'post' + '\t' + 'password' + '\n')
        for line in qw_result:
            l = line.strip().split('\t')
            # print(l)
            if (len(l) < 3):
                continue
            url = l[0] + l[1]
            if 'http://' not in url:
                url_res = 'http://' + url
            l[2] = l[2].strip('"').replace('""', '"')
            password = l[2].strip().split('=')[0]
            if (len(l[2]) < 10000 and '.gov' not in url_res and '.edu' not in url_res):
                data1.write(url_res + '\t' + urllib.unquote(l[2]) + '\t' + password + '\n')
                data1.write(url_res + '\t' + self.create_new_post(url_res, password) + '\t' + password + '\n')
        data1.close()

    def create_new_post(url, pw):
        if ('.php' in url and '.asp' not in url and '.jsp' not in url):
            new_post = pw + '=echo(31415926);'
        elif ('.asp' in url and '.jsp' not in url):
            new_post = pw + '=response.write("31415926")'
        elif ('.jsp' in url):
            new_post = pw + '=out.print(31415926)'
        else:
            new_post = pw + '=echo(31415926);'
        return new_post


    def run_query(self):
        QUREY_TYPE = 'airport_'
        out_file = 'ws_out.txt'
        d=self.get_position()
        with open(os.path.join(self.data_path, out_file), 'a+') as f:
            f.write('\t'.join(ALL_ITEMS + ['USERNAME','PASSWORD','PAYLOAD', 'KEY_WORDS']) + '\n')
            for KEY_WORDS in kwl:
                try:
                    LOGGER.info('OUTITMES:{0}\nQUERY_KEYS:{1}\nQUERYTIME:{2}_{3}'.format(self.all_items, KEY_WORDS, self.startTime, self.endTime))
                except Exception, e:
                    LOGGER.info('QUERY_ERROR-{0}'.format(e))
                for data in self.queryclient(KEY_WORDS,QUREY_TYPE):
                    if data.get('_HOST', '') and data.get('_MAINFILE', ''):
                        pload, method,content = self.ext_mainfile(data.get('_MAINFILE', ''))
                        data, pload=self.position_match(data, KEY_WORDS, pload, d)
                        username,password=self.get_airport_user(content)
                        
                        if pload != 'http文件获取失败':
                            try:
                                f.write('\t'.join([data.get(item, '') for item in self.all_items]) +'\t'+ username+'\t'+password+'\t'+pload+'\t'+KEY_WORDS+ '\n')
                            except:
                                pass


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


def init_path(path):
    if not os.path.exists(path):
        os.mkdir(path)

def zip_result(DATA_PATH,ZIP_PATH):
    pipe = Popen(['tar', '-zcvf', ZIP_PATH, DATA_PATH[2:],  '--remove-files'], stdout=PIPE, stderr=PIPE)
    out, err = pipe.communicate()
    if pipe.returncode:
        LOGGER.warning("Compress dirs failed with error code: {0}".format(pipe.returncode))
        LOGGER.warning(err.decode())
    else:
        LOGGER.info("Compress dirs success!.")


def main():
    LOGGER.info('START BatchQuery QUERY BATCH....')
    ap = BatchQuery(DATA_PATH,startTime,endTime,ALL_ITEMS)
    ap.run_query()
    ap.handle_res()

    ZIP_PATH = DATA_PATH + '_' + defaultStart + '.tgz.tmp'
    zip_result(DATA_PATH, ZIP_PATH)
    ZIP_SUCCEED = areacode + ZIP_PATH[2:].replace('.tmp', '')
    os.rename(ZIP_PATH, ZIP_SUCCEED)

    LOGGER.info('END BatchQuery QUERY BATCH')


if __name__ == '__main__':
    ##Program description
    usage = 'python bathquery_db_password.py --start [start_time] --end [end_time] --areacode [areacode]'
    dataformat = '<time>: yyyyMMddhhmmss eg:20180901000000'
    areaformat = '<areacode> xxxxxx eg:530000'
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
    OneYear = datetime.timedelta(days = 30)
    defaultStart = (NowTime - OneYear).strftime("%Y%m%d%H%M%S")
    defaultEnd   = NowTime.strftime("%Y%m%d%H%M%S")

    #ALL_ITEMS= ['AUTH_ACCOUNT', 'AUTH_TYPE', 'CAPTURE_TIME', 'STRSRC_IP', 'SRC_PORT', 'STRDST_IP', 'DST_PORT','_HOST', '_RELATIVEURL','_REFERER', '_MAINFILE', '_QUERY_CONTENT']
    ALL_ITEMS= ['AUTH_ACCOUNT', 'AUTH_TYPE', 'CAPTURE_TIME', 'STRSRC_IP', 'SRC_PORT', 'STRDST_IP', 'DST_PORT','_HOST', '_RELATIVEURL','_REFERER','_COOKIE','_USERAGENT','_MAINFILE']
    DATA_PATH = './_queryResult_hackDD_' + defaultEnd
    kwl = ['eval _POST =', 'eval _REQUEST =', 'array_map @ev =', 'ini_set display_errors dirname _SERVER =', 'Dizep cTSWX', 'response.write response.end =', 'ASPXSpy =', 'eval = BaSE64_dEcOdE', 'ute isnumeric =', 'Action=getTerminalInfo', 'DarkBladePass= goaction=', 'JspSpyPwd', 'eval System.Text.Encoding.GetEncoding', 'A17filelist folder=', 'fopen base64_decode', 'assert _POST', 'dbhost= dbport= dbuser= dbpass= connect= ', 'Execute password= system', 'Response.Write eval', '%eval request', 'exec request.getParameter', 'execute request', 'eval _SERVER', 'system _REQUEST', 'create_funtion _POST', 'preg_replace _POST']
    kwl2 = ['eval _POST =	PAYLOAD', 'eval _REQUEST =	PAYLOAD', 'array_map @ev =	PAYLOAD', 'ini_set display_errors dirname _SERVER =	PAYLOAD', 'Dizep cTSWX	PAYLOAD', 'response.write response.end =	PAYLOAD', 'ASPXSpy =	PAYLOAD', 'eval = BaSE64_dEcOdE	PAYLOAD', 'ute isnumeric =	PAYLOAD', 'Action=getTerminalInfo	PAYLOAD', 'DarkBladePass= goaction=	PAYLOAD', 'JspSpyPwd	PAYLOAD', 'eval System.Text.Encoding.GetEncoding	PAYLOAD', 'A17filelist folder=	PAYLOAD', 'fopen base64_decode	PAYLOAD', 'assert _POST	PAYLOAD', 'dbhost= dbport= dbuser= dbpass= connect= 	PAYLOAD', 'Execute password= system	PAYLOAD', 'Response.Write eval	PAYLOAD', '%eval request	PAYLOAD', 'exec request.getParameter	PAYLOAD', 'execute request	PAYLOAD', 'eval _SERVER	PAYLOAD', 'system _REQUEST	PAYLOAD', 'create_funtion _POST	PAYLOAD', 'preg_replace _POST	PAYLOAD']
    init_path(DATA_PATH)
    LOGGER = init_logger('queryclient_logger',os.path.join(DATA_PATH,'running.log'))
    if startTime is None and endTime is None:
        startTime = defaultStart
        endTime   = defaultEnd
    if len(startTime) !=14  or len(endTime) !=14:
        LOGGER.info('TimeData error:'+ dataformat)
        sys.exit(1)
    if areacode is None:
        areacode = '000000'
    if  len(areacode) !=6: 
        LOGGER.info('areacode error:'+ areaformat)
        sys.exit(1)
    main()
