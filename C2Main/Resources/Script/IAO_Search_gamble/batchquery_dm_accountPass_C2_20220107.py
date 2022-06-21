# -*- coding: utf-8 -*-
"""
2022.06.21
modify by AnTi
"""
from subprocess import Popen,PIPE
import re 
import sys
import datetime
import os
import logging
import urllib2

from optparse import OptionParser
reload(sys)
sys.setdefaultencoding('utf-8')


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
        with open(os.path.join(self.data_path, 'result.log'), 'a+') as f:
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
                if queryType == 'dm':
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
            if(len(pload)<2000):
                return content, method
            else:
                return 'http文件获取失败','http文件获取失败'

        except Exception, e:
            return 'http文件获取失败','http文件获取失败'


    def get_airport_user(self, content):
        user_key     = ['email']
        password_key = ['passwd','password']
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

    def handle_res(self):
        result = []
        out_file = 'dm_out.txt'
        result_file = 'cd_dama_res.txt'
        data = open(os.path.join(self.data_path, out_file), mode='r', errors='ignore').read()
        data1 = open(os.path.join(self.data_path,result_file), mode='w')
        line = data.split('	POST ')
        for i in line:
            if ('	POST	' in i):
                res = i.split('	POST	')[0]
                res1 = data.split('\t')[0] + '\t' + data.split('\t')[2] + '\t' + data.split('\t')[3] + '\t' + 'POST ' + res.replace('\n', 'daman_replace').replace('\t', 'damat_replace')
                result.append(res1)
        uniq_res = set(result)
        for i in uniq_res:
            if ('.php' in i.split('POST ')[1].split(' HTTP')[0]):
                if ('ip=127.0.0.1' not in i):
                    data1.write(i + '\n')
                elif ('_port' not in i):
                    data1.write(i + '\n')
                else:
                    pass
            else:
                pass


    def run_query(self):
        
        QUREY_TYPE = 'dm'
        out_file = 'dm_out.txt'
        
        with open(os.path.join(self.data_path, out_file), 'a+') as f:
            f.write('\t'.join(ALL_ITEMS + ['PAYLOAD', 'METHOD', 'KEY_WORDS']) + '\n')
            for KEY_WORD in key_words:
                try:
                    LOGGER.info('OUTITMES:{0}\nQUERY_KEYS:{1}\nQUERYTIME:{2}_{3}'.format(self.all_items, KEY_WORD, self.startTime, self.endTime))
                except Exception, e:
                    LOGGER.info('QUERY_ERROR-{0}'.format(e))
                for data in self.queryclient(KEY_WORD,QUREY_TYPE):
                    if data.get('_HOST', '') and data.get('_MAINFILE', ''):
                        pload, method = self.ext_mainfile(data.get('_MAINFILE', ''))
                        if pload != 'http文件获取失败':
                            try:
                                f.write('\t'.join([data.get(item, '') for item in self.all_items]) +'\t'+ pload+'\t'+method+'\t'+KEY_WORD+ '\n')
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

    ZIP_PATH = DATA_PATH + defaultEnd +  '_' + defaultStart + '.tgz.tmp'
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
    OneYear = datetime.timedelta(days = 90)
    defaultStart = (NowTime - OneYear).strftime("%Y%m%d%H%M%S")
    defaultEnd   = NowTime.strftime("%Y%m%d%H%M%S")

    #ALL_ITEMS= ['AUTH_ACCOUNT', 'AUTH_TYPE', 'CAPTURE_TIME', 'STRSRC_IP', 'SRC_PORT', 'STRDST_IP', 'DST_PORT','_HOST', '_RELATIVEURL','_REFERER', '_MAINFILE', '_QUERY_CONTENT']
    ALL_ITEMS= ['AUTH_ACCOUNT', 'AUTH_TYPE', 'CAPTURE_TIME', 'STRSRC_IP', 'SRC_PORT', 'STRDST_IP', 'DST_PORT','_HOST', '_RELATIVEURL','_REFERER','_COOKIE','_USERAGENT','_MAINFILE']
    DATA_PATH = './_queryResult_dm_'
    key_words = ['envlpass=','postpass=','admin_spiderpass=','admin_silicpass=','serveru= serverp=','eanver= .php','ip=127.0.0.1 port=2 3306','ip=localhost port=2 3306','host=localhost port= user= pass=','host=127.0.0.1 port= user= pass=','yourip= yourport= use=perl','yourip= yourport= use=nc','SUPort= SUUser= SUPass= SUCommand=net','phpspypass=','_COOKIE:loginpass=phpspy2014']

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
