# -*- coding: utf-8 -*-
import time
import urllib
import re
import sys
import datetime
import os
import io
import codecs
import logging
from subprocess import Popen, PIPE
from optparse import OptionParser

reload(sys)
sys.setdefaultencoding('utf-8')


class Email:
    def __init__(self, data_path, startTime, endTime):
        self.data_path = data_path
        self.startTime = startTime
        self.endTime = endTime
        self.querycontent = []

    def queryclient(self, keyWords):
        batch = []
        cont_flag = False
        cont_end_flag = True
        content = ''
        end_item = '_QUERY_MATCHTERMS'
        cmd = [
            '/home/search/sbin/queryclient',
            '--server', '127.0.0.1',
            '--port', '9870',
            '--querystring', '"{0}"'.format(keyWords),
            '--start', self.startTime,
            '--end', self.endTime,
            '--contextlen', '1000000',
            '--maxcount', '1000000',
            '--dbfilter', '\'' + ' OR '.join(['"' + value.decode('utf-8').encode('GBK') + '"' + ' in _SUBJECT' for value in LOGIN_VALUE]) + '\''
        ]

        querystring = ". /home/search/search_profile && {0} ".format(" ".join(cmd))
        pipe = Popen(querystring, shell=True, stdout=PIPE)
        print querystring
        for line in pipe.stdout:
            line = line.replace('\n','')
            if '_MAINFILE' in line:
                MAINFILE=line.replace('_MAINFILE: ', '')
                self.querycontent.append(MAINFILE)
        return self.querycontent
        

    def run_query(self):
        KEY_WORDS = "donotreply_SG@godaddy.com OR support@namesilo.com OR orders@dynadot.com"
        out_file = 'email_out.txt'

        try:
            LOGGER.info(
                'QUERY_KEYS:{0}\nQUERYTIME:{1}_{2}'.format(KEY_WORDS, self.startTime,self.endTime))
        except Exception, e:
                LOGGER.info('QUERY_ERROR-{0}'.format(e))
        mainfiles = self.queryclient(KEY_WORDS)
        with open(os.path.join(self.data_path, 'mainfile'), 'a+') as f:
            f.write('\n'.join(mainfiles))
        email = "if [ -s " + DATA_PATH[2:] + "/mainfile ]; then wget -P " + DATA_PATH[2:] + " -i " + DATA_PATH[2:] + "/mainfile; fi"
        rep = Popen(email, shell= True, stdout=PIPE)
        for line in rep.stdout:
            print line 
            
# #日志文件打印
def init_logger(logname, filename, logger_level=logging.INFO):
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


def zip_result(DATA_PATH, ZIP_PATH):
    pipe = Popen(['tar', '-zcvf', ZIP_PATH, DATA_PATH[2:], '--remove-files'], stdout=PIPE, stderr=PIPE)
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
    LOGGER.info('START EMAIL QUERY BATCH....')
    ap = Email(DATA_PATH, startTime, endTime)
    ap.run_query()
    ZIP_PATH = DATA_PATH + startTime + '_' + endTime + '.tgz.tmp'
    zip_result(DATA_PATH, ZIP_PATH)
    ZIP_SUCCEED = areacode + ZIP_PATH[2:].replace('.tmp', '')
    os.rename(ZIP_PATH, ZIP_SUCCEED)
    LOGGER.info('END EMAIL QUERY BATCH')


if __name__ == '__main__':
    usage = 'python bathquery_db_password.py --start [start_time] --end [end_time] --areacode [areacode] --query [queryContent]'
    dataformat = '<time>: yyyyMMddhhmmss eg:20180901000000'
    areaformat = '<areacode> xxxxxx eg:530000'
    queryformat = '<queryContent> eg:key and password'

    parser = OptionParser(usage)
    parser.add_option('--start', dest='startTime', help=dataformat)
    parser.add_option('--end', dest='endTime', help=dataformat)
    parser.add_option('--areacode', dest='areacode', help=areaformat)
    parser.add_option('-t', '--test', action='store_true', dest='test', help='test mode')
    parser.add_option('--query', dest='queryContent', help=queryformat)
    
    option, args = parser.parse_args()
    startTime = option.startTime
    endTime = option.endTime
    areacode = option.areacode
    queryContent = option.queryContent
    IS_TEST_MODE = option.test

    NowTime = datetime.datetime.now()
    OneYear = datetime.timedelta(days=360)
    defaultStart = (NowTime - OneYear).strftime("%Y%m%d%H%M%S")
    defaultEnd = NowTime.strftime("%Y%m%d%H%M%S")
    PASSWORD = 'fenghuohuofeng' + NowTime.strftime("%Y%m%d")
    LOGIN_VALUE = ["订单", "账户信息", "Thank you for your order", "Order Finished"]
    
    DATA_PATH = './_queryResult_email_'
    init_path(DATA_PATH)
    LOGGER = init_logger('queryclient_logger',os.path.join(DATA_PATH,'running.log'))
    
    if startTime is None and endTime is None:
        startTime = defaultStart
        endTime = defaultEnd
    if len(startTime) != 14 or len(endTime) != 14:
        LOGGER.info('TimeData error:' + dataformat)
        sys.exit(1)
    if areacode is None:
        areacode = '000000'
    if queryContent is None:
        pass
    if len(areacode) != 6:
        LOGGER.info('areacode error:' + areaformat)
        sys.exit(1)
    main()
