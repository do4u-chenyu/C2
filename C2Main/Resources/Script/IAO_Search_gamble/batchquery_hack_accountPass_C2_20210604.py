# -*- coding: utf-8 -*-
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
from optparse import OptionParser
import urllib2
import json
from urllib import unquote
reload(sys)
sys.setdefaultencoding('utf-8')

class Airport:
    def __init__(self,data_path,startTime,endTime,LOGGER):
        self.data_path = data_path
        self.startTime = startTime
        self.endTime   = endTime
        self.LOGGER    = LOGGER
        self.all_items = ['AUTH_ACCOUNT', 'AUTH_TYPE', 'CAPTURE_TIME', 'STRSRC_IP', 'SRC_PORT', 'STRDST_IP', 'DST_PORT','_HOST', '_RELATIVEURL','_REFERER']
        self.model_key_dict = {
            "bt" : {
                "col" : self.all_items + ['USERNAME', 'PASSWORD', '_COOKIE', '_MAINFILE', '_QUERY_CONTENT', 'keyWords', 'Path', 'Phone', 'sites_path', 'serverType', 'distribution', 'memSize', 'backup_path'],
                "key" : ["DST_PORT:8888 AND login AND username AND password AND code"]
            },
            "hk" : {
                "col" : self.all_items + ['USERNAME', 'PASSWORD', '_MAINFILE', '_QUERY_CONTENT', '_QUERY_MATCHTERMS','keyWords'],
                "key" : ["eval AND _POST","assert AND _POST","base64_decode AND _POST","Ba”.”SE6”.”4_dEc”.”OdE OR @ev”.”al","Response.Write OR Response.End OR _USERAGENT:antSword","@ini_set “display_errors”,”0″","_HOST:www.mtqyz.com","_HOST:www.hebeilvteng.com","_HOST:www.33ddos.com","_HOST:www.33ddos.cn","_HOST:www.33ddos.org","_HOST:www.33ddos.cc OR _HOST:www.33ddos.net OR _HOST:v1.dr-yun.org OR _HOST:v2.dr-yun.org OR _HOST:v3.dr-yun.org OR _HOST:www.360zs.cn OR _HOST:www2.360zs.cn","_HOST:www.999yingjia.com"]
            },
            "ddos" : {
                "col" : self.all_items + ['USERNAME', 'PASSWORD', 'keyWords', 'post_last_line'],
                "key" : ["register.php AND username AND email AND password AND password_r AND checkcode AND tos AND register"," login.php AND username AND password AND checkcode AND login"," ajax.php  AND reateorder AND tradeno AND gid AND allprice AND price AND qq AND type AND number AND paypass AND coupon AND phone AND email"," activation.php AND value"," ajax AND login.php AND register AND username AND password AND rpassword AND scode AND email AND question AND answer"," ajax AND login.php AND login AND username AND password"," ajax.php createorder AND tradeno AND gid AND allprice AND price AND qq AND type AND number AND paypass AND coupon AND phone AND email"," user AND code.php AND code AND jihuo"," home AND login.php AND username AND email AND qq AND scode AND password AND password2 AND geetest_challenge AND geetest_validate AND geetest_seccode AND agree AND register"," home AND login.php AND username AND password AND Login"," home AND code.php AND code AND jihuo"," ajax.php AND create AND out_trade_no AND gid AND money AND rel AND type"," Register AND user_name AND user_pass AND email_code AND token"," Login AND user_name AND user_pass AND code AND token"," Attack AND ip AND port AND type AND time"," api.php AND username AND password AND host AND port AND time AND method"]
            },
            "apk" : {
                "col" : self.all_items + ['USERNAME', 'PASSWORD','_COOKIE', '_MAINFILE', '_TITLE', '_TEXT','keyWords'],
                "key" : ["_RELATIVEURL:apk"," apk"," api.tw06.xlmc.sec.miui.com PackageName apk /api/ad/fetch/download"," adfilter.imtt.qq.com TURL=http apk"]
            },
            "xss" : {
                "col" : self.all_items + ['USERNAME', 'PASSWORD','_COOKIE', '_MAINFILE', '_TITLE', '_TEXT', 'keyWords', 'post_last_line'],
                "key" : ["_RELATIVEURL:do=register&act=submit AND key AND user AND email AND phone AND pwd AND pwd2","_RELATIVEURL:do=login&act=submit AND user AND pwd","_RELATIVEURL:do=project&act=create_submit AND token AND title AND description","_RELATIVEURL:do=project&act=setcode_submit AND token AND id AND ty AND setkey_1_keepsession AND modules AND setkey_15_info AND code","_RELATIVEURL:do=project&act=delcontent&r AND id AND token","_RELATIVEURL:index/user/doregister.html AND __token__ AND invitecode AND username AND email AND password AND password2","_RELATIVEURL:index/user/dologin.html AND username AND password"]
            },
            "sf" : {
                "col" : self.all_items + ['USERNAME', 'PASSWORD','Pay_Callbackurl'],
                "key" : ["pay_callbackurl"]
            },
            "vps" : {
                "col" : self.all_items + ['USERNAME', 'PASSWORD','_MAINFILE', '_QUERY_CONTENT', 'keyWords', 'post_last_line'],
                "key" : ["_HOST:www.sunnet365.com","_HOST:chm666.com","_HOST:www.beijiacloud.com","_HOST:www.zhekou5.com","_HOST:www.tiebavps.com","_HOST:www.lantuvps.com","_HOST:www.cbvpa.com","_HOST:www.jzvps.net","_HOST:www.5jwl.com","_HOST:www.mayivps.com","_HOST:7sensen.com","_HOST:www.idc789.com","_HOST:e8088.com","_HOST:263vps.com","_HOST:chenxunyun.com","_HOST:scvps.cn","_HOST:yh168.com","_HOST:plaidc.com","_HOST:leidianvps.com","_HOST:maini168.com","_HOST:91vps.com","_HOST:wanbianyun.com","_HOST:09vps.com","_HOST:chm666.com","_HOST:diyavps.com","_HOST:yunlifang.cn","_HOST:hunbovps.com","_HOST:30vps.com","_HOST:miandns.com","_HOST:nuobin.com","_HOST:cbvps.com","_HOST:74dns.com","_HOST:lsjvps.com","_HOST:zhimaruanjian.com","_HOST:xiziyun.cn","_HOST:taiyangruanjian.com","_HOST:idcbest.com","_HOST:zu029.com","_HOST:qgvps.com","_HOST:ygvps.com","_HOST:988vps.com"]
            },
            "qg" : {
                "col" : self.all_items + ['USERNAME', 'PASSWORD','_MAINFILE', '_QUERY_CONTENT', '_QUERY_MATCHTERMS', 'p_num', 'keyWords'],
                "key" : ["contactNum AND contactName","contactphone AND contactname","contactPhone AND contactName","contact_phone AND contact_name","contactMobile AND contactName","ContactMobile AND ContactName","contactPhoneList AND contactName","mobile AND name","mobile AND contactName","number AND name","phone AND name","telNums AND lastName","phoneNumbers AND contactName","phoneList AND contactName","/api/uploads/api data","cee_mobile AND cee_name","tel AND name","address AND body AND date","address AND body AND smsTime","mobile AND content AND send_time","mobile AND sms_body AND sms_time","other AND body AND time AND phone","peer_number AND content AND sms_time AND user_mobile","phone AND content AND date","phone AND content AND dialtime","phone AND messageContent AND date","phone AND messageContent AND messageDate","phone AND text AND date"]
            }
        }
        
    
    def queryclient(self,keyWords,queryType):
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
            '--contextlen', '1000',
            '--maxcount', '1000000'
        ]
        req = Popen(". /home/search/search_profile && {0}".format(" ".join(cmd)), shell=True, stdout=PIPE)
        print ". /home/search/search_profile && {0}".format(" ".join(cmd))
        self.LOGGER.info('QUERYTIME:{0}_{1} {2} ...wait...'.format(self.startTime, self.endTime, keyWords)) 
        
        for line in req.stdout:
            line  = line.replace('\x1a','')
            if line == '\n' and cont_flag and cont_end_flag:
                cont_flag = False
                if batch != []:
                    data = dict(batch)
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

    def re_verify(self,text):
        #p = Phone()
        try:
            # 手机号
            userphone = re.finditer(r"\D(13\d|14[5|7]|15\d|166|17[3|6|7]|18\d)\d{8}\D", text)
            # 验证
            if userphone:
                s = set()
                for m in userphone:
                    phone = m.group()[1:-1]
                    #if p.find(phone):
                    s = s | set([phone])
                return len(s)
            else:
                return 0
        except:
            return 0
            
    def get_phone(self, info):
        tel = ""
        if info and 'username' in info:
            r = re.findall('username":"([^"]+)"}', info)
            a = "".join(r)
            if len(a) == 11:
                tel = a
        return tel
        
    def ext_form_msg(self, content, mimetypes):
        """
        提取上传的form表单的变量名
        @param content:报文数据
        @param res_dic:结果字典
        @return:关键词列表
        """

        # 争对三种常见的POST提交数据方式，分别进行处
        # if 'application/x-www-form-urlencoded' in mimetypes:
        last_line = content.replace('&&','&').strip('...')
        # 判断是不是json格式
        print "last_line-->",last_line
        if last_line.split('=')[0] == 'data' or last_line.split('=')[0] == 'submit_params':
            dic_keys = json.loads(last_line.split('=')[1])
            #print 11111,dic_keys
            return dic_keys.get('pay_callbackurl', '')
        else:
            kv = [x.split('=') for x in last_line.split('&')]
            #print 22222,kv
            for x in kv:
                if x[0] == 'pay_callbackurl':
                    return x[1]
             
    def ext_mainfile(self, file_url):
        try:
            req = urllib2.Request(file_url)
            response = urllib2.urlopen(req, timeout=2)
            content = response.read().decode()
            pload = content.split('\n')[-1].replace('\t','').replace('\r','').replace('\n','').strip()
            if len(pload)>0:
                return pload.strip()

        except Exception, e:
            return ''
            
    def deal(self, data, model):
        if model == "bt":
            if data['_RELATIVEURL'] != "/login":
                return {}
            content = data.get('_QUERY_CONTENT', '').replace("\r","")
            cookie = data.get('_COOKIE', '')
            
            if "username=" in content and "password=" in content:
                a = re.compile(r'login...username=(.*?)password=(.*?)code=', re.S)
                result = a.findall(content)
                if len(result) > 0 and len(list(result[0])) == 2:
                    data['USERNAME'] = list(result[0])[0]
                    data['PASSWORD'] = list(result[0])[1]
                    
            cookie_dict = {}
            for c in cookie.split(";"):
                c_list = c.strip().split("=")
                if len(c_list) != 2:
                    continue
                key = c_list[0]
                value = c_list[1]
                if key not in cookie_dict:
                    cookie_dict[key] = value
                     
            ls = ["Path", "sites_path", "serverType", "distribution", "memSize", "backup_path"]
            for j in ls:
                data[j] = cookie_dict.get(j, "")
            data["sites_path"] = data["Path"].replace(data["sites_path"],"") if data["sites_path"] else ""
            data["Phone"] = self.get_phone(cookie_dict.get("bt_user_info",""))
    
        if model == "sf":
            pay_callbackurl = self.ext_form_msg(data.get('_QUERY_CONTENT',''), data.get('_MIMETYPES',''))
            if pay_callbackurl:
                data['Pay_Callbackurl'] = pay_callbackurl
        if model == "qg":
            Flist = ['hujing-public.oss-cn-beijing.aliyuncs.com', 'img-weimao.oss-cn-shanghai.aliyuncs.com', 'wap.js.10086.cn', 'oss.suning.com', 'ossup.suning.com', 'm.client.10010.com', 'c.pcs.baidu.com', 'pcs.baidu.com', 'c.tieba.baidu.com', 'www.spider.58.com', 'dms-sales.baonengmotor.com']
            if (data.get('_HOST', '') not in Flist) and ('13' or '145' or'147' or '15' or '166' or '173' or '176' or '177' or '18' in data.get('_QUERY_CONTENT', '')):
                tmp = data.get('_QUERY_CONTENT', '')
                p_num = self.re_verify(tmp)
                if p_num >= 2:
                    data['p_num'] = str(p_num)
        if model == "vps" or model == "ddos" or model == "xss":
            post_last_line= self.ext_mainfile(data.get('_MAINFILE',''))
            if post_last_line:
                data['post_last_line'] = post_last_line
        
        return data
        
    def run(self, model):
        kwl = self.model_key_dict[model]["key"]
        items = self.model_key_dict[model]["col"]
        out_file = model + '_out.txt'
        
        with open(os.path.join(self.data_path, out_file), 'a+') as f:
            f.write('\t'.join(items) + '\n')
            for KEY_WORDS in kwl:
                try:
                    for data in self.queryclient(KEY_WORDS,model):
                        data['keyWords'] = KEY_WORDS
                        data = self.deal(data,model)
                        if data:
                            f.write('\t'.join([data.get(item, '').replace("\r","").replace("\t","") for item in items]) + '\n')
                except Exception, e:
                    self.LOGGER.info('QUERY_ERROR-{0}'.format(e)) 

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

def zip_result(DATA_PATH,ZIP_PATH, LOGGER):
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
    for model in model_list:
        DATA_PATH = './_queryResult_' + model + '_'
        init_path(DATA_PATH)
        LOGGER = init_logger('queryclient_logger',os.path.join(DATA_PATH,'running.log'))
        LOGGER.info('START ' + model + ' QUERY BATCH....')
        ap = Airport(DATA_PATH,startTime,endTime,LOGGER)
        ap.run(model)
        LOGGER.info('END ' + model + ' QUERY BATCH')
    
        overTime = datetime.datetime.now()
        ZIP_PATH = DATA_PATH + NowTime.strftime("%Y%m%d%H%M%S") + '_' + overTime.strftime("%Y%m%d%H%M%S") + '.tgz.tmp'
        zip_result(DATA_PATH, ZIP_PATH, LOGGER)
        ZIP_SUCCEED = areacode + ZIP_PATH[2:].replace('.tmp', '')
        os.rename(ZIP_PATH, ZIP_SUCCEED)


if __name__ == '__main__':

    usage = 'python bathquery_db_password.py --start [start_time] --end [end_time] --areacode [areacode] --model [modelType]'
    dataformat = '<time>: yyyyMMddhhmmss eg:20180901000000'
    areaformat = '<areacode> xxxxxx eg:530000'
    modelformat = '<modelType> eg:bt'
    
    parser = OptionParser(usage)
    parser.add_option('--start',dest = 'startTime',help = dataformat)
    parser.add_option('--end',dest = 'endTime',help = dataformat)
    parser.add_option('--areacode',dest = 'areacode',help = areaformat)
    parser.add_option('--model',dest = 'modelType',help = modelformat)
    option,args = parser.parse_args()
    
    modelType = option.modelType
    startTime = option.startTime
    endTime = option.endTime
    areacode = option.areacode
    NowTime = datetime.datetime.now()
    OneYear = datetime.timedelta(days = 90)
    defaultStart = (NowTime - OneYear).strftime("%Y%m%d%H%M%S")
    defaultEnd = NowTime.strftime("%Y%m%d%H%M%S")
    
    if (startTime is None and endTime is None) or len(startTime) != 14  or len(endTime) != 14:
        startTime = defaultStart
        endTime = defaultEnd
    if areacode is None or len(areacode) != 6:
        areacode = '000000'
        
    model_list = ['bt', 'hk', 'ddos', 'apk', 'xss', 'sf', 'vps', 'qg']
    if modelType is None or modelType not in model_list:
        pass
    else:
        model_list = [modelType]
    
    main()
