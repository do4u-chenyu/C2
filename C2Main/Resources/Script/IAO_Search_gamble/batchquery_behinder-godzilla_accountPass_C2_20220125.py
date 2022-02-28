# -*- coding: utf-8 -*-
"""
Created on 2022/01/25
version 0125:查询冰蝎哥斯拉流量
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
reload(sys)
sys.setdefaultencoding('utf-8')
#################

class BatchQuery:
    def __init__(self,data_path,startTime,endTime,all_items):
        self.data_path = data_path
        self.startTime = startTime
        self.endTime   = endTime
        self.all_items = all_items
        self.out_file = 'ws_out.txt'

    def queryclient(self,keyWords):
        batch = []
        cont_flag = False
        cont_end_flag = True
        content = ''
        end_item = '_QUERY_MATCHTERMS'        
        cmd = [
            '/home/search/sbin/queryclient',
            '--server', '127.0.0.1',
            '--port', '9870',
            '--querystring', "'{}'".format(keyWords),
            '--start', self.startTime,
            '--end', self.endTime,
            '--contextlen', '10000',
            '--maxcount', '1000000',
            '--timeout', '120'
        ]
        req = Popen(". /home/search/search_profile && {}".format(" ".join(cmd)), shell=True, stdout=PIPE)
        print ". /home/search/search_profile && {}".format(" ".join(cmd))
        LOGGER.info('QUERYTIME:{0}_{1}\n wait...'.format(self.startTime,self.endTime)) 
        for line in req.stdout:
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

    def ext_mainfile(self, file_url,query_type):
        """
        处理.http文件体,获取请求体前30个字符
        @param input_path: 输入文件路径
        @return: 请求提，请求方法
        """
        try:
            req = urllib2.Request(file_url)
            response = urllib2.urlopen(req, timeout=2)
            content = response.read().decode()
            if query_type =='partial_req_content':
                pload = "请求体前50字符：" + content.split('\n')[-1][:50]
            else:
                pload = content.split('\n')[-1].replace('\r','').replace('\n','').replace('\t','')
                pload = pload[:4000]
            method = content.split(' ')[0]            
            return pload, method
        except Exception, e:
            return 'http文件获取失败','http文件获取失败'   
    
    def run_query(self,key_words,query_type,trojan_type):
        with open(os.path.join(self.data_path, self.out_file), 'a+') as f:
            for KEY_WORD in key_words:
                try:
                    LOGGER.info('OUTITMES:{0}\nQUERY_KEYS:{1}\nQUERYTIME:{2}_{3}'.format(self.all_items, KEY_WORD, self.startTime, self.endTime))
                except Exception, e:
                    LOGGER.info('QUERY_ERROR-{0}'.format(e))
                query_result = self.queryclient(KEY_WORD)
                for data in query_result:
                    if not (data.get('_HOST', '') and data.get('_MAINFILE', '')):
                        continue
                    if not self.result_filter(data, trojan_type):
                        continue
                    pload, method = self.ext_mainfile(data.get('_MAINFILE', ''),query_type)
                    if pload == 'http文件获取失败':
                        continue
                    try:
                        mark_field =  pload + '\t' + method + '\t' + trojan_type
                        f.write('\t'.join([data.get(item, '') for item in self.all_items]) +'\t'+ mark_field + '\n')
                    except:
                        pass
    def result_filter(self,data, trojan_type):
        url = data.get('_RELATIVEURL', '')
        if not trojan_type.endswith("加密流量"):
            return True
        if not (url.endswith("php") or url.endswith('jsp')):
            return False
        return True
    def create_result_file(self):
        with open(os.path.join(self.data_path, self.out_file), 'a+') as f:
            f.write('\t'.join(ALL_ITEMS + ['PAYLOAD_KEYWORD', 'HTTP_REQ_METHOD', 'LABEL']) + '\n')
        
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
    ap.create_result_file()
    ap.run_query(key_words_bx_0,"partial_req_content","疑似冰蝎加密流量")
    ap.run_query(key_words_bx_1,"whole_req_content","疑似冰蝎上传木马")
    ap.run_query(key_words_gsl_0,"whole_req_content","疑似哥斯拉加密流量")
    ap.run_query(key_words_gsl_1,"whole_req_content","疑似哥斯拉上传木马")

    ZIP_PATH = DATA_PATH + '_' + defaultStart + '.tgz.tmp'
    zip_result(DATA_PATH, ZIP_PATH)
    ZIP_SUCCEED = areacode + ZIP_PATH[2:].replace('.tmp', '')
    os.rename(ZIP_PATH, ZIP_SUCCEED)


    LOGGER.info('END BatchQuery QUERY BATCH')


if __name__ == '__main__':
    ##Program description
    usage = 'python batchquery_behinder-godzilla_accountPass_C2_20220125.py --start [start_time] --end [end_time] --areacode [areacode]'
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

    ALL_ITEMS = ['AUTH_ACCOUNT', 'AUTH_TYPE', 'CAPTURE_TIME', 'STRSRC_IP', 'SRC_PORT', 'STRDST_IP', 'DST_PORT','_HOST', '_RELATIVEURL','_REFERER','_COOKIE','_USERAGENT','_MAINFILE']
    DATA_PATH = './_queryResult_behinder-godzilla_' + defaultEnd
    key_words_bx_0 = ['3Mn1yNMtoZViV5wotQHPJtwwj0F4b2lyToNK7LfdUnN7zmyQFfx OR j7cEF7zpdtyAnFYCSqiRX OR bbRvs1NfjV6iKKs65VTnlSIbCArJv OR I25fHDt6u7G5EAckKvobL3rxD OR a2TU3gN1PGeSroZ0I3CRFwbMmKQ0 OR P04Fr67bQFg5BxkurjYDC OR mH5oEL2ABTwaCIBShMiIx OR WWKmDkw3Kcel93W3oWv0KXZQRkSy7kBAh OR htOltdTMF92bbN28yJxYnRARCUq4 OR GGCPNAMpRgj1auGlaRQjYm2F915iDKRYkUaJgPChZRszVQYBKyGdM1hVz6a OR 3KOLSBt1I1ZnFMZRLMFqSHpt4doUPO OR ExLvjPnbInJ6YXhfP4 OR G6w8vmJ19L4qDWrwfB1CjAazQ OR Gl0VoIUsjez6l7Srsq2q OR cwQWSEeUTpX36oyiHerjUEInWN','sx5X62RwZRQ0K53cUho3ZcpzuEzbpJIHlKr7ejJgVHTGxowzaKYbHQtzeT OR PY9Hn91L4tc20DC07NjqcmA4HYdff1lX OR rPUCIckXgO2CJj2ljU OR 2GDjEJ0fAVfcI098hIRSr6HfWoM7vK OR ELEAmHTnwn7vXwsNCLNiMq8YybbfnuhnrSuuruNubVqC OR 0YTC6XKE9yW0crualykZatikCuSP1sMnTHVYkcKUFz4hWlauBxCDP OR nKmPC3yHoor4AAOtX1rMS9zLPHYsnxSHOFGb OR OxdIC4JoGQy62Ub6ObmP9iLdRmcyM6 OR j7C8lvSho6UsjWQ62HQMLKsbcn3v OR Us124AELlIcEfN73EtdcDVOTP67qhIZqIN','7Pwvfbbo3ZplI40L OR roWObO9rbonnTa52P57V8OwUz1prlDUDt OR Ibe1t2QOh5u3gYalL7BydmXhbTp5v7ANlzjdDVOZ86mdIKyHOyUG OR SNTrHDYrbzsSKm02akqqVZBJg0QgzYXrQ3UPHDAR3RAjcbtxkYdx OR FDdBRRYSjPE5wCA4YMLC OR ig3n74hqPwizOVAf0JqVOkDIg2lXkp3XDbeF9wlrN1Rn4HVe OR uv3X2g9EQOD2rKto2VU2OAQQO','3Mn1yNMtoZViV5wotQHPJrbk14HK OR 80JlWajoOTxIoyrpBYXckAxSvT3Ts4j01r9LcEjiRCSA1JRmuv OR r947t9h2ivWfrlsivm1Jp11AjolE OR NBC1dk7bfpfykNxog7aJ1hk5uTwn OR oPx8arwQ0Cpz3JyFEbkEd OR yMSNXNMy5z5KcEM2aI OR 6PszZuzhQ3yQVLoKMvou1 OR XlzYHwnd7z9tjmB4rV0FP14WTU2UJcN OR 3NPNhsTkoBIWyiuequfshZfK0hQDZeJylHEm0i OR qH1qbuxiaPrbk6JVAq00THY8Dw2z','4CjYJrxbtMx5LClH98qDtXQFga9SAXLOxzU91LoW4ApnSdzmNnK9wULA OR PsZ9ASbR1zeGoMbWz82ARMsH6 OR ZLvjpQEl3RJMGvyZaD50tZDf8E OR nPIpoQ5dwzUA8twdkAKwePoL9 OR soEsLeYFQZnklB1o0w OR QuZq0ma13iveDdMBn4ce1yvLbCKMt4 OR H6gw60BtVvxjl02KL3Eha2mkUsuJv9tJ8JyZVJ4wrM2 OR G6kY1h5Odlwdeg8gAw0zJwCMuPSKfi OR CYMFa223mTpcdkvOPyL7CvMhK69p0JUyX05BCzIWtzpWFpaTS41 OR VoQdhXTMbCRTQJ1nTxlhjSIgVymENmaSMu8DBJC72l69J OR tjIvjJWYrkmpfb3okhaA1hMHJVqQQDHD3Tb0y1sHDpwmNWT OR 9knFBq6ilUYuCUt364V3KkABh OR yiGsmtmbebxUyP0HuS8DXgRAYAIJ OR BoFTfcxqubzx2JXvPRbZiU9E3YAad3m56','wSSPA0ArR93nA3BGLXw OR wC2DOkS8llEv5HNmnTi3FUS8KFptDVJIOlSwP8Sw6 OR 4eLVr8U54QGZuFF OR Jpm8Y5LMTIVnWRfNY OR zhD9FYZKK2ooXjTdnaGkKnJU4uc OR yElwQR3ZzkGDMozJYZocmQhUmDrf OR 9PYPBrSz6ZvxsHFa9FNhPCEp5dUNgEBCu14qngEMkYn8nmvfSmt','LW2nE69tBzfjLnibYPu OR ZrmCdYoXOOBOyviiZUFuY OR jbUXIulx0KjDszgRMXgzJigHOF OR q4cENYjZVythwRxXa5W OR Jpm8Y5LMTIVnWRfNY OR 0naXesh8ae4aWmRju OR OcJW7oO2J7DXhF0GgBfpgNI','rDaW4amYVjObpXMtNhA3Tj7wNM74aXeYLprtsP OR pmnMA4q73oQzDhYB8qWZwfzKWDW7WOrnwt OR SpEo3anzKMLnNuZsrK3Ria8ySxl8V6wMVRIA8Mvwc3CyM4mEnnzYH3 OR 1wkStRtSaacA1hY2kE OR MADe9r4peLnfTWiBCKoeFJzTyXDLzRO OR whcK3Mm6TLhHkUwuAi4gWJQH OR yz1wu9ucBBkGJkNVqlsVlY1hWBcQc04iZBF6m8kpY3G1iocC8 OR 7ncCH5SFU2k8l8d8CrVvhbshTOqBtWMwbcbrZSy1VUVqofxqaaFnb4 OR qKWMkyRosuLssRPZqx7rD0Rq OR nwxV7cG4qj6R8fH9NoeaqSZ9nBVAhTYAsIOL OR PTyYw4GYHfsnDFZkGZwjLg8ikEa0C OR 5YErFg7aqkUBfTXYC50pBMybE3 OR 3DXsEHEO4QC3RxrpJMo8MeFltk5ZMcB0xRMM OR K8iUiv8Me9eDDDC4CXKUj33Jeaull OR ZSHj3r7ibOrfEAXnwX7M3dWms65lLuXuXIsufW2ja9fN']
    key_words_bx_1 = ['e45e329feb5d925b','error_reporting session_start _SESSION file_get_contents eval openssl_decrypt explode','Response.CharSet Request.ServerVariables Request.QueryString response.write execute Request.BinaryRead','.equals import= java.util. javax.crypto. request.getParameter BASE64Decoder','Language= Import Response.Write Request.BinaryRead .Equals Request Session.','.equals import= java.util. javax.crypto. request.getParameter BASE64Decoder','error_reporting session_start file_get_contents openssl_decrypt eval call_user_func explode','Response.CharSet Session Request.TotalBytes Request.BinaryRead execute','page import= java.util. javax.crypto. request.getMethod .equals .BASE64Decoder request.getReader','Page Language= Import Namespace= Session. Request.BinaryRead .Equals','page import= java.util. javax.crypto. session .BASE64Decoder .equals']
    key_words_gsl_0 = ['eval base64_decode strrev urldecode']
    key_words_gsl_1 = ['3c6e0b8a9c15224a','session_start set_time_limit error_reporting base64_decode _SESSION eval _POST md5 substr','session_start set_time_limit error_reporting file_get_contents encode _SESSION getBasicsInfo','BinaryStream request.Form Session Base64Decode response.Write bypassDictionary','bypassDictionary Request.BinaryRead Session Execute response.End response.BinaryWrite','bypassDictionary BinaryStream request.BinaryRead Session response.End Execute','BinaryStream request.Form Session Base64Decode response.Write bypassDictionary','pass= md5= javax.crypto.Cipher base64.getMethod base64Encode response.getWriter']

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
