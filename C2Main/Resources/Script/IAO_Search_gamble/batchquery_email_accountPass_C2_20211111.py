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
        self.mainfile_content = []
        self.fullfile_content = []
        

    def queryclient(self, keyWords, model):
        cmd = [
            '/home/search/sbin/queryclient',
            '--server', '127.0.0.1',
            '--port', '9870',
            '--querystring', '"{0}"'.format(keyWords),
            '--start', self.startTime,
            '--end', self.endTime,
            '--contextlen', '1000000',
            '--maxcount', '1000000',         
        ]
        dbfilter = ['--dbfilter', '\'' + ' OR '.join(['"' + value.decode('utf-8').encode('GBK') + '"' + ' in _SUBJECT' for value in LOGIN_VALUE]) + '\'']
        if model == 'domain':
            cmd = cmd + dbfilter
        querystring = ". /home/search/search_profile && {0} ".format(" ".join(cmd))      
        return querystring
    
    def get_mainfile(self, mainfiles, mainfilename):
        with open(os.path.join(self.data_path, mainfilename), 'w') as f:
            f.write('\n'.join(mainfiles))
        email = "if [ -s " + DATA_PATH[2:] + "/" + mainfilename + " ]; then wget -P " + DATA_PATH[2:] + " -i " + DATA_PATH[2:] + "/" + mainfilename + "; fi"
        rep = Popen(email, shell= True, stdout=PIPE)
        for line in rep.stdout:
            print line

    def run_query(self):
        domain_keys = ["donotreply_SG@godaddy.com", "support@namesilo.com", "orders@dynadot.com"] 
        KEY_WORDS_DOMAIN = " OR ".join(domain_keys)
        
        vpn_keys = ["noreply@cloudss.co", "a7728051@gmail.com", "noreply@mg.greenss.co", "seeocloud@seoo.vip", "noreply@qiuyin.co", "hi@paoluz.net", "no-reply@linkhub.store", "speed_notice@qq.com", "support@suying666.pw", "leisu@mail.lei-su.link", "no-replay@mail.flyint.date", "sales@hostwinds.com", "support@hostease.com", "china@resellerclub.com", "support@raksmart.com", "noreply@vultr.com", "no-reply-aws@amazon.com", "support@gigsgigscloud.com", "no-reply@sugarhosts.com", "support@megalayer.net", "robot@app.cloudcone.email", "support@krypt.com", "sales@cn.bluehost.com", "support@bandwagonhost.com", "support@gcorelabs.com", "billing@gcore.lu", "support@hosteons.com", "no.reply@frantech.ca", "no-reply@virmach.com", "sales@racknerd.com", "no-reply@referrals.digitalocean.com", "no-reply@antpool.com", "no-reply@f2pool.com", "noreply@em720.notify.blockin.com", "hello@foundrydigital.com", "noreply@mail2.viabtc.com", "noreply@btc.com",  "noreply@slushpool.com", "noreply@mail.huobi.mn", "noreply@prod.sbicrypto.com", "no-reply@emcd.io", "hello@luxor.tech", "support@greatpool.ca", "no-reply@sigmapool.com", "no-reply@email2.trustpool.ru", "service@2009pool.com", "noreply@qubtc.com", "Notification@mg.lincoinpool.com", "no-reply@hashcity.org", "385561983@qq.com", "no-reply@cruxpool.com", "info@laurentiapool.org", "Sales@BlockwareSolutions.com", "noreply@mining-dutch.nl","hpool@email.hpool.bz", "accounts@email.sparkpool.com", "hpool@email.hpool.online", "service@intl.paypal.com", "daniel@email.revuto.com"]
        KEY_WORDS_VPN = " OR ".join(vpn_keys)
        
        try:        
            querystring = self.queryclient(KEY_WORDS_DOMAIN, 'domain') + "; " + self.queryclient(KEY_WORDS_VPN, 'vpn')
            pipe = Popen(querystring, shell=True, stdout=PIPE)
            
            for line in pipe.stdout:
                line = line.replace('\n','')
                self.fullfile_content.append(line)
                if '_MAINFILE' in line:
                    MAINFILE=line.replace('_MAINFILE: ', '')
                    self.mainfile_content.append(MAINFILE)
            with open(os.path.join(self.data_path, "fullfile"), 'w') as f:
                f.write('\n'.join(self.fullfile_content))
            self.get_mainfile(self.mainfile_content, 'mainfile')
            LOGGER.info('QUERY_KEYS:{0}\nQUERYTIME:{1}_{2}\n'.format(KEY_WORDS_DOMAIN, self.startTime,self.endTime))
            LOGGER.info('QUERY_KEYS:{0}\nQUERYTIME:{1}_{2}\n'.format(KEY_WORDS_VPN, self.startTime,self.endTime))
        except Exception, e:
            LOGGER.info('QUERY_ERROR-{0}\n'.format(e))
        
            
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
    usage = 'python bathquery_db_password.py --start [start_time] --end [end_time] --areacode [areacode]'
    dataformat = '<time>: yyyyMMddhhmmss eg:20180901000000'
    areaformat = '<areacode> xxxxxx eg:530000'

    parser = OptionParser(usage)
    parser.add_option('--start', dest='startTime', help=dataformat)
    parser.add_option('--end', dest='endTime', help=dataformat)
    parser.add_option('--areacode', dest='areacode', help=areaformat)
    
    option, args = parser.parse_args()
    startTime = option.startTime
    endTime = option.endTime
    areacode = option.areacode

    NowTime = datetime.datetime.now()
    OneYear = datetime.timedelta(days=360)
    defaultStart = (NowTime - OneYear).strftime("%Y%m%d%H%M%S")
    defaultEnd = NowTime.strftime("%Y%m%d%H%M%S")

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

    if len(areacode) != 6:
        LOGGER.info('areacode error:' + areaformat)
        sys.exit(1)
    main()
