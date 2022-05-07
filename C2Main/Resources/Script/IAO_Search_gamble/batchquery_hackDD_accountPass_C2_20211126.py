# -*- encoding: utf-8 -*-
# @File : batchquery_hackDD_accountPass_C2_20211126.py
# @Author : 安替
# @Time : 2022/5/7 11:38
# @Software : PyCharm
# Python版本：3.6.3

from subprocess import Popen,PIPE
import re
import io 
import sys
import datetime
import os
import logging
import urllib2
from optparse import OptionParser
reload(sys)
sys.setdefaultencoding('utf-8')


class BatchQuery:
    def __init__(self,data_path, startTime, endTime, all_items):
        self.data_path = data_path
        self.startTime = startTime
        self.endTime   = endTime
        self.all_items = all_items

    def queryclient(self, keyWords, queryType):
        """
        :param keyWords
        :param queryType
        :return data
        """
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
                if queryType == 'hackDD':
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
                        batch.append(('_QUERY_CONTENT', content))
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
            pload = content.split('\n')[-1].replace('\t', '').replace('\r', '').replace('\n', '').strip()
            method = content.split(' ')[0]
            if(len(pload)>100 and len(pload)<1000):
                return pload.strip(), method
            else:
                return 'http文件获取失败','http文件获取失败'

        except Exception, e:
            return 'http文件获取失败','http文件获取失败'


    def handle_res(self):
        tmplist = []
        out_file = 'ws_out.txt'
        result_file = 'qw_res.txt'
        with open(os.path.join(self.data_path, out_file), "r") as f:
            with open(os.path.join(self.data_path,result_file), "w") as f1:
                lines = f.readlines()
                for line in lines[1:]:  #跳过第一行title
                    line = line.strip('\n')
                    try:
                        #AUTH_ACCOUNT、CAPTURE_TIME、STRSRC_IP、_HOST、_RELATIVEURL、PAYLOAD
                        data = line.split('\t')[0] + '\t' + line.split('\t')[2] + '\t'+ line.split('\t')[3] + '\t'+ line.split('\t')[7] + '\t' + line.split('\t')[8] + '\t' + line.split('\t')[13] + '\n'
                    except:
                        data = ""
                    tmplist.append(data)
                tmplist = list(set(tmplist))  ##去重
                for line in tmplist:
                    f1.writelines(line)
            f1.close()
        f.close()

        qw_result = io.open(os.path.join(self.data_path,result_file), mode='r', encoding='utf-8').readlines()
        data1 = io.open(os.path.join(self.data_path, 'request_post.txt'), mode='wb+')
        data1.write('AUTH_ACCOUNT' + '\t' + 'CAPTURE_TIME' + '\t' + 'STRSRC_IP' + '\t' + 'url' + '\t' + 'password' + '\n')
        for line in qw_result:
            l = line.strip().split('\t')
            if (len(l) < 6):#写的很隐晦，大胆猜测如果每行数据有缺失值，直接跳过
                continue
            url = l[3] + l[4]
            if 'http://' not in url:
                url_res = 'http://' + url
            l[5] = l[5].strip('"').replace('""', '"')
            password = l[5].strip().split('=')[0]
            if (len(l[5]) < 10000 and '.gov' not in url_res and '.edu' not in url_res):
                data1.write(l[0] + '\t' + l[1] + '\t' + l[2] + '\t' + url_res + '\t' + password + '\n')
        data1.close()

    def run_query(self):
        QUREY_TYPE = 'hackDD'
        out_file = 'ws_out.txt'
        with open(os.path.join(self.data_path, out_file), 'a+') as f:
            f.write('\t'.join(ALL_ITEMS + ['PAYLOAD', 'METHOD', 'KEY_WORDS']) + '\n')
            for KEY_WORDS in kwl:
                try:
                    LOGGER.info('OUTITMES:{0}\nQUERY_KEYS:{1}\nQUERYTIME:{2}_{3}'.format(self.all_items, KEY_WORDS, self.startTime, self.endTime))
                except Exception, e:
                    LOGGER.info('QUERY_ERROR-{0}'.format(e))
                for data in self.queryclient(KEY_WORDS,QUREY_TYPE):
                    if data.get('_HOST', '') and data.get('_MAINFILE', ''):
                        pload, method = self.ext_mainfile(data.get('_MAINFILE', ''))
                        
                        if pload != 'http文件获取失败':
                            try:
                                f.write('\t'.join([data.get(item, '') for item in self.all_items]) +'\t'+ pload+'\t'+method+'\t'+KEY_WORDS+ '\n')
                            except:
                                pass
#日志文件打印
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
    ap = BatchQuery(DATA_PATH, startTime, endTime, ALL_ITEMS)
    ap.run_query()
    ap.handle_res()

    ZIP_PATH = DATA_PATH + '_' + defaultStart + '.tgz.tmp'
    zip_result(DATA_PATH, ZIP_PATH)
    ZIP_SUCCEED = areacode + ZIP_PATH[2:].replace('.tmp', '')
    os.rename(ZIP_PATH, ZIP_SUCCEED)

    LOGGER.info('END BatchQuery QUERY BATCH')


if __name__ == '__main__':
    usage = 'python bathquery_db_password.py --start [start_time] --end [end_time] --areacode [areacode]'
    dataformat = '<time>: yyyyMMddhhmmss eg:20180901000000'
    areaformat = '<areacode> xxxxxx eg:530000'
    parser = OptionParser(usage)
    parser.add_option('--start',dest = 'startTime',help = dataformat)
    parser.add_option('--end',dest = 'endTime',help = dataformat)
    parser.add_option('--areacode',dest = 'areacode',help = areaformat)

    option,args = parser.parse_args()
    startTime   = option.startTime
    endTime     = option.endTime
    areacode    = option.areacode

    NowTime = datetime.datetime.now()
    OneYear = datetime.timedelta(days = 15)
    defaultStart = (NowTime - OneYear).strftime("%Y%m%d%H%M%S")
    defaultEnd   = NowTime.strftime("%Y%m%d%H%M%S")

    ALL_ITEMS = ['AUTH_ACCOUNT', 'AUTH_TYPE', 'CAPTURE_TIME', 'STRSRC_IP', 'SRC_PORT', 'STRDST_IP', 'DST_PORT','_HOST', '_RELATIVEURL','_REFERER','_COOKIE','_USERAGENT','_MAINFILE']
    DATA_PATH = './_queryResult_hackDD_' + defaultEnd

    kwl = ['_RELATIVEURL:aspx System.Convert System.Text FromBase64String Response.Write NOT UmVzcG9uc2UuV3JpdGUoImhhb3JlbmdlLmNvbVFRMzE3Mjc1NzM4Iik7',
    '_RELATIVEURL:aspx System.Convert System.Text FromBase64String Request.Item',
    '_RELATIVEURL:aspx System.Convert System.Text Request.Item HexAsciiConvert',
    '.php @ini_set display_errors @set_time_limit @is_writable',
    '.php @eval $_POST @base64_decode',
    '.php @eval $_POST @str_rot13 @vav_frg @frg_gvzr_yvzvg qvfcynl_reebef',
    'yv66vgAAADEBFgoAQgB7CQBfAHwJAF8AfQgAfgkAXwB OR yv66vgAAADEBDQoAQQB5CQBeAHoJAF4AewgAfAkAXgB9CAB OR ALIIALMHALQKAD8AtQcAtgoAtwC4CAC5CABfCgA OR ALoKALsAvAoAuwC9CABhBwC OR BwDACQCZAMEKALcAvAoAPwDCCgCZAMMKAD8AxAoAxQDGCgBBAJQIAMcKAMgAyQgAygoAKADLCADMCADNCg OR ACBBwD5DAD6APsMAPwA OR CgAdAL8LAGAAwAoACwC OR woAVQD8CgBVAP0IAP4KAFUA',
    '_RELATIVEURL:asp byVal Error (IsNumeric OR I%s%N%u%m%e%r%i%c) NOT ((Mozilla/5.0 compatible Baiduspider/2.0 www.baidu.com/search/spider.html) OR (Mozilla/4.0 compatible; MSIE 9.0; Windows NT 6.1))',
    '_RELATIVEURL:php ((z0= z1= z9=  BaSE64_dEcOdE) OR (z0= z9=  BaSE64_dEcOdE ((Mozilla/5.0 compatible Baiduspider/2.0 www.baidu.com/search/spider.html) OR (Mozilla/5.0+ compatible Googlebot/2.1 www.google.com/bot.html))))',
    'Response.Write Text.Encoding.GetEncoding GetString FromBase64String Response.End z1=',
    '_RELATIVEURL:asp Function hextostr byVal IsNumeric Description',
    '_RELATIVEURL:aspx Response.Write System.Text.Encoding System.Convert.FromBase64String',
    '_RELATIVEURL:jsp V3d3Um9vdFBhdGhDb2Rl',
    '_RELATIVEURL:php @ini_set @set_time_limit @set_magic_quotes_runtime base64_decode $_POST',
    '_RELATIVEURL:php ABC @eval base64_decode REQUEST',
    '_RELATIVEURL:php array_map @ev',
    '_RELATIVEURL:php eval _SERVER',
    '_RELATIVEURL:php _TEXT:eval _TEXT:_SERVER NOT DOCUMENT_ROOT',
    '_RELATIVEURL:asp Execute Server.ScriptTimeout Resume byVal IsNumeric Response.Write']

    #kwl =['eval System.Text.Encoding.GetEncoding', 'edoced_46esab z0=', 'system _POST', 'array_map @ev', 'eval _SERVER', 'Function  To IsNumeric Else Response.Write Response.End z1=', 'BaSE64 %40eval%2F .27']

    init_path(DATA_PATH)
    LOGGER = init_logger('queryclient_logger', os.path.join(DATA_PATH, 'running.log'))
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
