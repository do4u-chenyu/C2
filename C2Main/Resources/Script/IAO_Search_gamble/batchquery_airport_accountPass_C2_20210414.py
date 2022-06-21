# -*- coding: utf-8 -*-
"""
2022.06.21
Modify by AnTi
格式规范
"""
from subprocess import Popen,PIPE
import re 
import sys
import datetime
import os
import logging
from optparse import OptionParser
reload(sys)
sys.setdefaultencoding('utf-8')

class Airport:
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
                '--querystring', '"{0}"'.format(keyWords),
                '--start', self.startTime,
                '--end', self.endTime,
                '--contextlen', '1000',
                '--maxcount', '1000000'
            ]
            req = Popen(". /home/search/search_profile && {0}".format(" ".join(cmd)), shell=True, stdout=PIPE)
            print ". /home/search/search_profile && {0}".format(" ".join(cmd))
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
                            data['USERNAME'],data['PASSWORD'] = self.get_airport_user(content)
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


    def get_airport_user(self, content):
        user_key     = ['email']
        password_key = ['passwd','password']
        user_str = ''
        pass_str = ''
        for part in re.split(r'\.\.\.',content):
            for line in re.split('[\r\n&;]+',part):
                try:
                    [key, value] = line.strip().split("=")
                    if len(value.strip()) < 1:
                        continue
                    if key in user_key:
                        user_str = value
                    elif key in password_key:
                        pass_str = value
                except:
                    pass
        return user_str,pass_str


    def run_query(self, KEY_WORDS, out_file):
        QUREY_TYPE = 'airport'
        if out_file == 'airport_out.txt':
            self.all_items += ['USERNAME', 'PASSWORD']
        with open(os.path.join(self.data_path, out_file), 'a+') as f:
            f.write('\t'.join(ALL_ITEMS) + '\n')
            try:
                LOGGER.info('OUTITMES:{0}\nQUERY_KEYS:{1}\nQUERYTIME:{2}_{3}'.format(self.all_items, KEY_WORDS, self.startTime, self.endTime))
            except Exception, e:
                LOGGER.info('QUERY_ERROR-{0}'.format(e))
            for data in self.queryclient(KEY_WORDS,QUREY_TYPE):
                if data.get('_HOST', ''):
                    try:
                        f.write('\t'.join([data.get(item, '') for item in self.all_items]) + '\n')
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

def zip_result(DATA_PATH,ZIP_PATH):
    pipe = Popen(['tar', '-zcvf', ZIP_PATH, DATA_PATH[2:],  '--remove-files'], stdout=PIPE, stderr=PIPE)
    out, err = pipe.communicate()
    if pipe.returncode:
        LOGGER.warning("Compress dirs failed with error code: {0}".format(pipe.returncode))
        LOGGER.warning(err.decode())
    else:
        LOGGER.info("Compress dirs success!.")


def init_path(path):
    if not os.path.exists(path):
        os.mkdir(path)


def main():
    LOGGER.info('START AIRPORT QUERY BATCH....')
    key_word_1 = '_RELATIVEURL:auth/login AND email= AND (passwd= OR password=)'
    key_word_2 = '_RELATIVEURL:admin AND _COOKIE:email= AND _COOKIE:expire_in'
    out_file_1 = 'airport_out.txt'
    out_file_2 = 'airport_admin_out.txt'
    ap = Airport(DATA_PATH, startTime, endTime, ALL_ITEMS)
    ap.run_query(key_word_1, out_file_1)
    ap.run_query(key_word_2, out_file_2)
    LOGGER.info('END AIRPORT QUERY BATCH')

    ZIP_PATH = DATA_PATH + startTime + '_' + endTime + '.tgz.tmp'
    logger = init_logger('queryclient_logger',os.path.join(DATA_PATH,'running.log'))
    zip_result(DATA_PATH,ZIP_PATH)
    ZIP_SUCCEED = areacode +  ZIP_PATH[2:].replace('.tmp','')
    os.rename(ZIP_PATH,ZIP_SUCCEED)

if __name__ == '__main__':
    usage = 'python bathquery_db_password.py --start [start_time] --end [end_time] --areacode [areacode]'
    dataformat = '<time>: yyyyMMddhhmmss eg:20180901000000'
    areaformat = '<areacode> xxxxxx eg:530000'
    parser = OptionParser(usage)
    parser.add_option('--start',dest = 'startTime',help = dataformat)
    parser.add_option('--end',dest = 'endTime',help = dataformat)
    parser.add_option('--areacode',dest = 'areacode',help = areaformat)
    parser.add_option('-t', '--test', action='store_true', dest='test', help='test mode')
    ##get input Time  parameter
    option,args = parser.parse_args()
    startTime   = option.startTime
    endTime     = option.endTime
    areacode    = option.areacode
    IS_TEST_MODE        = option.test
    ##set default Time[ one year]
    NowTime = datetime.datetime.now()
    OneYear = datetime.timedelta(days = 90)
    defaultStart = (NowTime - OneYear).strftime("%Y%m%d%H%M%S")
    defaultEnd   = NowTime.strftime("%Y%m%d%H%M%S")
   
    ALL_ITEMS= ['AUTH_ACCOUNT', 'AUTH_TYPE', 'CAPTURE_TIME', 'STRSRC_IP', 'SRC_PORT', 'STRDST_IP', 'DST_PORT','_HOST', '_RELATIVEURL','_REFERER','_COOKIE']                                                                                                                                             
    PASSWORD = 'fenghuohuofeng' + NowTime.strftime("%Y%m%d")
    DATA_PATH = './_queryResult_airport_'
    LOGIN_VALUE = ['admin', 'administrator', 'root','system','sys']
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
