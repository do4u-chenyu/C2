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
    version 1118:
        增加打包功能，合并payload解码脚本
@author: Administrator
"""
from Queue import Queue
from threading import Thread
from subprocess import Popen,PIPE
import time
import urllib
import re 
import sys
import datetime
import os
import itertools
import logging
import urllib2
from urllib import unquote
from optparse import OptionParser
import io

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
            return pload.strip(), method
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

    def get_url_pass(self,payload):
        try:
            res = urllib.unquote(payload)
            url = res.strip().split('SiteUrl=')[1].split('&SitePass=')[0]
            pw = res.strip().split('SiteUrl=')[1].split('&SitePass=')[1].split('&nCodePage=')[0]
            res2 = url + '|' + pw
        except Exception as e:
            print(e)
            res2 = ''
        if ('.gov' in res2 or '.edu' in res2):
            res2 = ''
            return res2
        return res2

    def create_post(self,url, pw):
        if ('.php' in url and '.asp' not in url and '.jsp' not in url):
            new_post = pw + '=echo(31415926);'
        elif ('.asp' in url and '.jsp' not in url):
            new_post = pw + '=response.write("31415926")'
        elif ('.jsp' in url):
            new_post = pw + '=out.print("31415926")'
        else:
            new_post = pw + '=echo(31415926);'
        return new_post

    def DataAnalyse(self):
        ##df=pd.read_csv(r'C:\Users\dt\Desktop\sz_ws_out.txt',sep='\t',dtype=str,error_bad_lines=False)
        # df['url_pw']=df['PAYLOAD'].apply(get_url_pass)
        # df['url_pw'].to_csv('url_pw_res.txt',encoding='utf-8',header=False,index=False)
        with open(os.path.join(self.data_path, 'ws_out.txt'), "r") as f:
            with open(os.path.join(self.data_path,"url_pw_res.txt"), "w") as f2:
                lines = f.readlines()
                for line in lines:
                    line = line.strip('\n')
                    data = self.get_url_pass(line.split('\t')[13])
                    f2.writelines(data + '\n')
            f2.close()
        f.close()
        url_pw_res = io.open(os.path.join(self.data_path, 'url_pw_res.txt'), mode='r', encoding='utf-8').readlines()
        url_pw_post = io.open(os.path.join(self.data_path, 'url_pw_post.txt'), mode='wb+')
        for line in url_pw_res:
            l = line.strip().split('|')
            if (len(l) < 2):
                continue
            url_pw_post.write(l[0] + '\t' + self.create_post(l[0], l[1]) + '\t' + l[1] + '\n')
        url_pw_post.close()

    def run_query(self):

        QUREY_TYPE = 'airport_'
        out_file = 'ws_out.txt'
        
        with open(os.path.join(self.data_path, out_file), 'a+') as f:
            f.write('\t'.join(ALL_ITEMS + ['PAYLOAD', 'METHOD', 'KEY_WORDS']) + '\n')
            for KEY_WORDS in Kwl:
                try:
                    LOGGER.info('OUTITMES:{0}\nQUERY_KEYS:{1}\nQUERYTIME:{2}_{3}'.format(self.all_items, KEY_WORDS, self.startTime, self.endTime))
                except Exception, e:
                    LOGGER.info('QUERY_ERROR-{0}'.format(e))
                for data in self.queryclient(KEY_WORDS,QUREY_TYPE):
                    if data.get('_HOST', '') and data.get('_MAINFILE', ''):
                        pload, method, content = self.ext_mainfile(data.get('_MAINFILE', ''))
                        if pload != 'http文件获取失败':
                            try:
                                f.write('\t'.join([data.get(item, '') for item in self.all_items]) +'\t'+ pload+'\t'+method+'\t'+KEY_WORDS+'\n')
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

##打包
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
    #ap.DataAnalyse()
    ZIP_PATH = DATA_PATH + NowTime.strftime("%Y%m%d%H%M%S")  + '.tgz.tmp'
    zip_result(DATA_PATH,ZIP_PATH)
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
    DATA_PATH = './_queryResult_hostDD_' + defaultEnd
    Kwl = [ '_HOST:9128.cc','_HOST:threnfa.com','SiteUrl= SitePass=', '_HOST:c.wwwcd.top','_HOST:tophack.net','_HOST:djking.f3322.net:93','_HOST:www.xazchs.com','_HOST:45677789.com']

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
