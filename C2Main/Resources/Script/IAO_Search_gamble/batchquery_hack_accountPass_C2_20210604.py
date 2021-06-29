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
#################
class Scheduler:
    def __init__(self,startTime,endTime,filename,queryWords):
        self.queryclient_keyWordQueue = Queue()
        self.queryclient_resultQueue  = Queue()
        self.workers   = []
        self.startTime = startTime
        self.endTime   = endTime
        self.queryWords = queryWords
        self.init_queryclient_kwQueue()
        self.saver   = Saver(filename, self.queryclient_resultQueue)
        
    def init_queryclient_kwQueue(self):
        LOGGER.info('queryWords: '+ str(self.queryWords))
        for line in self.queryWords:
            self.queryclient_keyWordQueue.put(line)  #TODO
        LOGGER.info("keyword query group number：{0}".format(len(self.queryWords)))

            
    def scheduling(self):
        print "启动写入线程"
        self.saver.daemon = True
        self.saver.start()
        
        print "启动查询线程"
        for i in xrange(len(self.queryWords)):
            worker = Query(self.queryclient_keyWordQueue, self.queryclient_resultQueue, self.startTime, self.endTime)
            worker.daemon = True
            worker.start()
            self.workers.append(worker)
        self.saver.workers = self.workers
        
        self.queryclient_keyWordQueue.join()  # init before the thread start
        self.queryclient_resultQueue.join()

class Query(Thread):
    def __init__(self, queryclient_keyWordQueue, queryclient_resultQueue,start_time,end_time):
        Thread.__init__(self)
        self.configDict   = {'AUTH_ACCOUNT': 0,'STRDST_IP': 1,'STRSRC_IP': 2,'_REFERER': 3,'_QUERY_CONTENT': 4,'SRC_IPID': 5,'DST_IPID': 6,'CAPTURE_TIME': 7}
        self.lineEnd      = '_USERAGENT'
        self.user_key     = ['account','uid','name','phone','username','userid']
        self.password_key = ['password','pwd','pass','key']
        self.other_key    = {'safecode':['safepass','code']}##方便扩展新解析项
       
        self.lineEndMarkLength  =len(self.lineEnd)
        self.exitRefer          = False
        self.queryContentFlag   = False## content mabye MultiLine 
        self.value_num          = max(self.configDict.values()) + 1
        self.content            = [""] * self.value_num
        self.newAddColumneNum   = 2 + len(self.other_key.keys())
        self.queryContent_index = self.configDict['_QUERY_CONTENT']
        self.queryclient_keyWordQueue = queryclient_keyWordQueue
        self.queryclient_resultQueue  = queryclient_resultQueue
        self.start_time = start_time
        self.end_time   = end_time
        self.get_num    = 0

    def run(self):
        while True:
            queryStart = datetime.datetime.now().strftime("%Y%m%d%H%M%S")
            key_word = self.queryclient_keyWordQueue.get()
            self.get_num += 1
            self.queryClient(key_word)
            queryEnd = datetime.datetime.now().strftime("%Y%m%d%H%M%S")
            LOGGER.info('query Time:{0}_{1}'.format(queryStart,queryEnd))
            self.queryclient_keyWordQueue.task_done()  # 

    def queryClient(self, key_word):
        startTime = self.start_time
        endTime   = self.end_time
        cmd = [
            '/home/search/sbin/queryclient',
            '--server', '127.0.0.1',
            '--port', '9870',
            '--querystring', "'" + key_word + "'",
            '--start', startTime,
            '--end', endTime,
            '--contextlen', '1000',
            '--maxcount', '2147483647'
        ]
        if '=' not in key_word:
            dbfilter = '\'' + ' OR '.join(['"' + value + '"' +  ' in _REFERER'  for value in  LOGIN_VALUE]) + '\''
            cmd = cmd + ['--dbfilter',dbfilter]
        req = Popen(". /home/search/search_profile && {0}".format(" ".join(cmd)), shell=True, stdout=PIPE)
        for line in req.stdout:
            if 'query finished' in line:
                LOGGER.info('queryCmd:' + ". /home/search/search_profile && {0}".format(" ".join(cmd)) + '-----' +line.strip())
            if self.queryContentFlag:
                self.content[self.configDict.get('_QUERY_CONTENT', -1)] += (';'+line.strip())
            if ":" in line:
                self.queryContentFlag= False
                [key, vlaue] = line.rstrip().split(":", 1)
                index = self.configDict.get(key.strip(), -1)
                if index != -1:
                    if key == '_REFERER': self.exitRefer = True
                    if key == '_QUERY_CONTENT':self.queryContentFlag = True
                    self.content[index] = vlaue.strip().replace("\t", " ")         
                if key == self.lineEnd:
                    self.content[self.configDict.get('_QUERY_CONTENT', -1)].replace('\n',';')
                    if self.exitRefer:
                            self.get_pwd()
                    self.content   = [""] * self.value_num
                    self.exitRefer = False
    
    def get_pwd(self):
        USERPW   = self.content[self.queryContent_index]
        pre_str  = "\t".join(self.content[0:self.queryContent_index])
        next_str = "\t".join(self.content[self.queryContent_index+1:])
        try:
            user_str, pass_str,code_str = self.get_UserKeyfromEd(USERPW)
            tmp_line = "\t".join([pre_str.encode('utf-8',"ignore").decode("utf8", "ignore"), USERPW.encode('utf-8',"ignore").decode("utf8", "ignore"), user_str.encode('utf-8',"ignore").decode("utf8", "ignore"),pass_str.encode('utf-8',"ignore").decode("utf8", "ignore"),code_str.encode('utf-8',"ignore").decode("utf8", "ignore"), next_str.encode('utf-8',"ignore").decode("utf8", "ignore")])
        except UnicodeDecodeError as e:
            tmp_line = "\t".join([pre_str, USERPW, user_str, pass_str,code_str, next_str])
        except Exception as e:
            tmp_line = "\t".join([pre_str,"",""])+ "\t\t" + "\t".join([""]*self.newAddColumneNum) + "\t" + next_str + "\t"
        finally:
            self.queryclient_resultQueue.put(tmp_line)
        
    def get_UserKeyfromEd(self,USERPW):
        temp_dict = {}
        key_list = []
        blankchar=re.compile(ur"[\r\n\t]")
        for line in re.split('[&;]+',USERPW):
            try:
                [key, value] = line.split("=")
                if len(value.strip()) < 1:
                    continue
                temp_dict[key.lower()] = value
                key_list.append(key.lower())
            except:
                pass
        otherkey_list=self.other_key.keys()
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
            for userkey in self.user_key:
                temp_dis.append(self.levenshtein(userkey, key))
            user_dis.append(min(temp_dis))
            temp_dis = []
            for passwordkey in self.password_key:
                temp_dis.append(self.levenshtein(passwordkey, key))
            pass_dis.append(min(temp_dis))
            for othkey in otherkey_list:
                temp_dis = []
                for codekey in self.other_key[othkey]:
                    temp_dis.append(self.levenshtein(codekey, key))
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
        for key in self.other_key.keys():
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
    def levenshtein(self, first, second):
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
    
class Saver(Thread):
    def __init__(self, save_path,queryclient_resultQueue):
        Thread.__init__(self)
        self.queryclient_resultQueue = queryclient_resultQueue
        self.rets = []
        self.save_path = save_path
        self.writer = open(os.path.join(DATA_PATH,self.save_path), 'a+')
        self.done_sum = 0
        self.last_time = time.time()

    def run(self):
        queryStart = datetime.datetime.now().strftime("%Y%m%d%H%M%S")
        while True:
            queryclient_result= self.queryclient_resultQueue.get()  # ret
            self.save(queryclient_result)
            self.queryclient_resultQueue.task_done()
    def save(self, info):
        self.writer.write(info.replace('\r','')+"\n")
        self.done_sum +=  1
        if self.get_FileSize(DATA_PATH) > 50:
            succeedFilename = self.save_path.strip('_') + '_' + datetime.datetime.now().strftime("%Y%m%d%H%M%S") + '.txt'
            os.rename(os.path.join(DATA_PATH,self.save_path), os.path.join(DATA_PATH,succeedFilename))
            ZIP_PATH =  os.path.join(DATA_PATH,succeedFilename).replace('txt','')
            zip_result(os.path.join(DATA_PATH,succeedFilename),ZIP_PATH)
            self.writer.close()
            self.writer = open(os.path.join(DATA_PATH,self.save_path), 'a+')
    def get_FileSize(self,dataPath):
        filePath = unicode(self.save_path,'utf-8')
        fsize    = os.path.getsize(os.path.join(dataPath,filePath))
        fsize    = fsize/float(1024*1024)
        return round(fsize,2)
# only have
class Airport:
    def __init__(self,data_path,startTime,endTime,all_items):
        self.data_path = data_path
        self.startTime = startTime
        self.endTime   = endTime
        self.all_items = ['AUTH_ACCOUNT', 'AUTH_TYPE', 'CAPTURE_TIME', 'STRSRC_IP', 'SRC_PORT', 'STRDST_IP', 'DST_PORT','_HOST', '_RELATIVEURL','_REFERER']

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
            if len(pload)>0:
                return pload.strip()

        except Exception, e:
            return ''

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


    def ext_apk(content):
        """
        处理.http文件体
        @param input_path: 输入文件路径
        @return: 抽取的规则结果字典
        """
            #req = urllib2.Request(file_url)
            #response = urllib2.urlopen(req, timeout=1)
            #content = unquote(response.read().decode())
        regex = re.compile(r'https?://[-A-Za-z0-9+&@#/%?=~_|!:,.;]+.apk')
        url_list = regex.findall(content)
        return url_list

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
                if queryType == 'airport1':
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
        for part in re.split(r'..', content):
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


    def run_query(self):
        KEY_WORDS = '_RELATIVEURL:auth/login AND email= AND (passwd= OR password=)'
        QUREY_TYPE = 'airport'
        self.all_items += ['USERNAME', 'PASSWORD']
        out_file = 'airport_out.txt'
        
        with open(os.path.join(self.data_path, out_file), 'a+') as f:
            f.write('\t'.join(self.all_items) + '\n')
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
    def run_bt(self):
        KEY_WORDS = 'DST_PORT:8888 AND login AND username AND password AND code'
        QUREY_TYPE = 'bt'
        self.all_items += ['USERNAME', 'PASSWORD', '_COOKIE', '_MAINFILE', '_QUERY_CONTENT','post_last_line','keyWords']
        out_file = 'bt_out.txt'

        with open(os.path.join(self.data_path, out_file), 'a+') as f:
            f.write('\t'.join(self.all_items) + '\n')
            try:
                #save(f, ALL_ITEMS, KEY_WORDS, DATA_PATH, OUT_PATH, QUERY_TYPE, startTime, endTime)
                for data in self.queryclient(KEY_WORDS,QUREY_TYPE):
                    if data.get('_HOST', ''):
                        #post_last_line= self.ext_mainfile(data.get('_MAINFILE',''))
                        #if post_last_line:
                            #data['post_last_line'] = post_last_line
                        data['keyWords'] = KEY_WORDS
                        try:
                            f.write('\t'.join([data.get(item, '') for item in self.all_items]) + '\n')
                        except:
                            pass
                LOGGER.info('OUTITMES:{0}\nQUERY_KEYS:{1}\nQUERYTIME:{2}_{3}'.format(self.all_items, KEY_WORDS, self.startTime, self.endTime))
            except Exception, e:
                LOGGER.info('QUERY_ERROR-{0}'.format(e))

    def run_hk(self):
        #KEY_WORDS = '(eval AND _POST) OR (assert AND _POST) OR (base64_decode AND _POST) OR (Ba”.”SE6”.”4_dEc”.”OdE OR @ev”.”al) OR (Response.Write OR Response.End OR _USERAGENT:antSword) OR (@ini_set “display_errors”,”0″) OR (_HOST:www.mtqyz.com) OR (_HOST:www.hebeilvteng.com) OR (_HOST:www.33ddos.com) OR (_HOST:www.33ddos.cn) OR (_HOST:www.33ddos.org) OR (_HOST:www.33ddos.cc OR _HOST:www.33ddos.net OR _HOST:v1.dr-yun.org OR _HOST:v2.dr-yun.org OR _HOST:v3.dr-yun.org OR _HOST:www.360zs.cn OR _HOST:www2.360zs.cn) OR (_HOST:www.999yingjia.com)'
        QUREY_TYPE = 'hk'
        self.all_items += ['USERNAME', 'PASSWORD', '_MAINFILE', '_QUERY_CONTENT', '_QUERY_MATCHTERMS','keyWords']
        out_file = 'hk_out.txt'
        kwl=["eval AND _POST","assert AND _POST","base64_decode AND _POST","Ba”.”SE6”.”4_dEc”.”OdE OR @ev”.”al","Response.Write OR Response.End OR _USERAGENT:antSword","@ini_set “display_errors”,”0″","_HOST:www.mtqyz.com","_HOST:www.hebeilvteng.com","_HOST:www.33ddos.com","_HOST:www.33ddos.cn","_HOST:www.33ddos.org","_HOST:www.33ddos.cc OR _HOST:www.33ddos.net OR _HOST:v1.dr-yun.org OR _HOST:v2.dr-yun.org OR _HOST:v3.dr-yun.org OR _HOST:www.360zs.cn OR _HOST:www2.360zs.cn","_HOST:www.999yingjia.com"]
        with open(os.path.join(self.data_path, out_file), 'a+') as f:
            f.write('\t'.join(self.all_items) + '\n')
            for KEY_WORDS in kwl:
                try:
                    for data in self.queryclient(KEY_WORDS,QUREY_TYPE):
                        if data.get('_HOST', ''):
                            data['keyWords'] = KEY_WORDS
                            f.write('\t'.join([data.get(item, '') for item in self.all_items]) + '\n')
                    LOGGER.info('OUTITMES:{0}\nQUERY_KEYS:{1}\nQUERYTIME:{2}_{3}'.format(self.all_items, KEY_WORDS, self.startTime, self.endTime))
                except Exception, e:
                    LOGGER.info('QUERY_ERROR-{0}'.format(e))


    def run_ddos(self):
        #KEY_WORDS = '(register.php AND username AND email AND password AND password_r AND checkcode AND tos AND register) OR ( login.php AND username AND password AND checkcode AND login) OR ( ajax.php  AND reateorder AND tradeno AND gid AND allprice AND price AND qq AND type AND number AND paypass AND coupon AND phone AND email) OR ( activation.php AND value) OR ( ajax AND login.php AND register AND username AND password AND rpassword AND scode AND email AND question AND answer) OR ( ajax AND login.php AND login AND username AND password) OR ( ajax.php createorder AND tradeno AND gid AND allprice AND price AND qq AND type AND number AND paypass AND coupon AND phone AND email) OR ( user AND code.php AND code AND jihuo) OR ( home AND login.php AND username AND email AND qq AND scode AND password AND password2 AND geetest_challenge AND geetest_validate AND geetest_seccode AND agree AND register) OR ( home AND login.php AND username AND password AND Login) OR ( home AND code.php AND code AND jihuo) OR ( ajax.php AND create AND out_trade_no AND gid AND money AND rel AND type) OR ( Register AND user_name AND user_pass AND email_code AND token) OR ( Login AND user_name AND user_pass AND code AND token) OR ( Attack AND ip AND port AND type AND time) OR ( api.php AND username AND password AND host AND port AND time AND method)'
        QUREY_TYPE = 'ddos'
        self.all_items += ['USERNAME', 'PASSWORD','post_last_line','keyWords']
        out_file = 'ddos_out.txt'
        kwl=["register.php AND username AND email AND password AND password_r AND checkcode AND tos AND register"," login.php AND username AND password AND checkcode AND login"," ajax.php  AND reateorder AND tradeno AND gid AND allprice AND price AND qq AND type AND number AND paypass AND coupon AND phone AND email"," activation.php AND value"," ajax AND login.php AND register AND username AND password AND rpassword AND scode AND email AND question AND answer"," ajax AND login.php AND login AND username AND password"," ajax.php createorder AND tradeno AND gid AND allprice AND price AND qq AND type AND number AND paypass AND coupon AND phone AND email"," user AND code.php AND code AND jihuo"," home AND login.php AND username AND email AND qq AND scode AND password AND password2 AND geetest_challenge AND geetest_validate AND geetest_seccode AND agree AND register"," home AND login.php AND username AND password AND Login"," home AND code.php AND code AND jihuo"," ajax.php AND create AND out_trade_no AND gid AND money AND rel AND type"," Register AND user_name AND user_pass AND email_code AND token"," Login AND user_name AND user_pass AND code AND token"," Attack AND ip AND port AND type AND time"," api.php AND username AND password AND host AND port AND time AND method"]
        with open(os.path.join(self.data_path, out_file), 'a+') as f:
            f.write('\t'.join(self.all_items) + '\n')
            for KEY_WORDS in kwl:
                try:
                    #save(f, ALL_ITEMS, KEY_WORDS, DATA_PATH, OUT_PATH, QUERY_TYPE, startTime, endTime)
                    for data in self.queryclient(KEY_WORDS,QUREY_TYPE):
                        if data.get('_HOST', ''):
                            #post_last_line= self.ext_mainfile(data.get('_MAINFILE',''))
                            #if post_last_line:
                                #data['post_last_line'] = post_last_line
                            data['keyWords'] = KEY_WORDS
                            try:
                                f.write('\t'.join([data.get(item, '') for item in self.all_items]) + '\n')
                            except:
                                pass
                    LOGGER.info('OUTITMES:{0}\nQUERY_KEYS:{1}\nQUERYTIME:{2}_{3}'.format(self.all_items, KEY_WORDS, self.startTime, self.endTime))
                except Exception, e:
                    LOGGER.info('QUERY_ERROR-{0}'.format(e))

    def run_apk(self):
        #KEY_WORDS = '(_RELATIVEURL:apk) OR (apk) OR ( api.tw06.xlmc.sec.miui.com PackageName apk /api/ad/fetch/download) OR ( adfilter.imtt.qq.com TURL=http apk)'
        QUREY_TYPE = 'apk'
        self.all_items += ['USERNAME', 'PASSWORD','_COOKIE', '_MAINFILE', '_TITLE', '_TEXT','keyWords']
        out_file = 'apk_out.txt'
        kwl=["_RELATIVEURL:apk"," apk"," api.tw06.xlmc.sec.miui.com PackageName apk /api/ad/fetch/download"," adfilter.imtt.qq.com TURL=http apk"]
        with open(os.path.join(self.data_path, out_file), 'a+') as f:
            f.write('\t'.join(self.all_items) + '\n')
            for KEY_WORDS in kwl:
                try:
                    for data in self.queryclient(KEY_WORDS,QUREY_TYPE):
                        if data.get('_HOST', ''):
                            #url_list = ext_mainfile(data.get('_QUERY_CONTENT',''))
                            data['keyWords'] = KEY_WORDS
                            try:
                                f.write('\t'.join([data.get(item, '') for item in self.all_items]) + '\n')
                            except:
                                pass
                    LOGGER.info('OUTITMES:{0}\nQUERY_KEYS:{1}\nQUERYTIME:{2}_{3}'.format(self.all_items, KEY_WORDS, self.startTime, self.endTime))
                except Exception, e:
                    LOGGER.info('QUERY_ERROR-{0}'.format(e))

    def run_xss(self):
        #KEY_WORDS = '(_RELATIVEURL:do=register&act=submit AND (key AND user AND email AND phone AND pwd AND pwd2)) OR ( _RELATIVEURL:do=login&act=submit AND (user AND pwd)) OR ( _RELATIVEURL:do=project&act=create_submit AND (token AND title AND description)) OR ( _RELATIVEURL:do=project&act=setcode_submit AND (token AND id AND ty AND setkey_1_keepsession AND modules AND setkey_15_info AND code)) OR ( _RELATIVEURL:do=project&act=delcontent&r AND (id AND token)) OR ( _RELATIVEURL:index/user/doregister.html AND (__token__ AND invitecode AND username AND email AND password AND password2)) OR ( _RELATIVEURL:index/user/dologin.html AND (username AND password))'
        QUREY_TYPE = 'xss'
        self.all_items += ['USERNAME', 'PASSWORD','_COOKIE', '_MAINFILE', '_TITLE', '_TEXT','post_last_line','keyWords']
        out_file = 'xss_out.txt'
        kw1=["_RELATIVEURL:do=register&act=submit AND key AND user AND email AND phone AND pwd AND pwd2","_RELATIVEURL:do=login&act=submit AND user AND pwd","_RELATIVEURL:do=project&act=create_submit AND token AND title AND description","_RELATIVEURL:do=project&act=setcode_submit AND token AND id AND ty AND setkey_1_keepsession AND modules AND setkey_15_info AND code","_RELATIVEURL:do=project&act=delcontent&r AND id AND token","_RELATIVEURL:index/user/doregister.html AND __token__ AND invitecode AND username AND email AND password AND password2","_RELATIVEURL:index/user/dologin.html AND username AND password"]
        
        with open(os.path.join(self.data_path, out_file), 'a+') as f:
            f.write('\t'.join(self.all_items) + '\n')
            #f.write('\t'.join(ALL_ITEMS) + '\tpost_last_line\tkeyWords' + '\n')
            for KEY_WORDS in kw1:
                try:
                    #save(f, ALL_ITEMS, KEY_WORDS, DATA_PATH, OUT_PATH, QUERY_TYPE, startTime, endTime)
                    for data in self.queryclient(KEY_WORDS,QUREY_TYPE):
                        if data.get('_HOST', ''):
                            #post_last_line= self.ext_mainfile(data.get('_MAINFILE',''))
                            #if post_last_line:
                                #data['post_last_line'] = post_last_line
                            data['keyWords'] = KEY_WORDS
                            try:
                                f.write('\t'.join([data.get(item, '') for item in self.all_items]) + '\n')
                            except:
                                pass
                    LOGGER.info('OUTITMES:{0}\nQUERY_KEYS:{1}\nQUERYTIME:{2}_{3}'.format(self.all_items, KEY_WORDS, self.startTime, self.endTime))
                except Exception, e:
                    LOGGER.info('QUERY_ERROR-{0}'.format(e))

    def run_sf(self):
        KEY_WORDS = 'pay_callbackurl'
        QUREY_TYPE = 'sf'
        self.all_items += ['USERNAME', 'PASSWORD','Pay_Callbackurl']
        out_file = 'sf_out.txt'
        
        with open(os.path.join(self.data_path, out_file), 'a+') as f:
            f.write('\t'.join(self.all_items) + '\n')
            try:
                #save(f, ALL_ITEMS,KEY_WORDS,DATA_PATH,OUT_PATH,QUERY_TYPE,startTime,endTime)
                for data in self.queryclient(KEY_WORDS,QUREY_TYPE):
                    if data.get('_HOST',''):
                        pay_callbackurl = self.ext_form_msg(data.get('_QUERY_CONTENT',''), data.get('_MIMETYPES',''))
                        #if pay_callbackurl:
                            #data['Pay_Callbackurl'] = pay_callbackurl
                        data['keyWords'] = KEY_WORDS
                        try:
                            f.write('\t'.join([data.get(item, '') for item in self.all_items]) + '\n')
                        except:
                            pass
                LOGGER.info('OUTITMES:{0}\nQUERY_KEYS:{1}\nQUERYTIME:{2}_{3}'.format(self.all_items, KEY_WORDS, self.startTime, self.endTime))
            except Exception,e:
                LOGGER.info('QUERY_ERROR-{0}'.format(e))

    def run_vps(self):
        #KEY_WORDS = '(www.sunnet365.com) OR ( chm666.com) OR ( www.beijiacloud.com) OR ( www.zhekou5.com) OR ( www.tiebavps.com) OR ( www.lantuvps.com) OR ( www.cbvpa.com) OR ( www.jzvps.net) OR ( www.5jwl.com) OR ( www.mayivps.com) OR ( 7sensen.com) OR ( www.idc789.com) OR ( e8088.com) OR ( 263vps.com) OR ( chenxunyun.com) OR ( scvps.cn) OR ( yh168.com) OR ( plaidc.com) OR ( leidianvps.com) OR ( maini168.com) OR ( 91vps.com) OR ( wanbianyun.com) OR ( 09vps.com) OR ( chm666.com) OR ( diyavps.com) OR ( yunlifang.cn) OR ( hunbovps.com) OR ( 30vps.com) OR ( miandns.com) OR ( nuobin.com) OR ( cbvps.com) OR ( 74dns.com) OR ( lsjvps.com) OR ( zhimaruanjian.com) OR ( xiziyun.cn) OR ( taiyangruanjian.com) OR ( idcbest.com) OR ( zu029.com) OR ( qgvps.com) OR ( ygvps.com) OR ( 988vps.com)'
        QUREY_TYPE = 'vps'
        self.all_items += ['USERNAME', 'PASSWORD','_MAINFILE', '_QUERY_CONTENT','post_last_line','keyWords']
        out_file = 'vps_out.txt'
        kwl=["_HOST:www.sunnet365.com","_HOST:chm666.com","_HOST:www.beijiacloud.com","_HOST:www.zhekou5.com","_HOST:www.tiebavps.com","_HOST:www.lantuvps.com","_HOST:www.cbvpa.com","_HOST:www.jzvps.net","_HOST:www.5jwl.com","_HOST:www.mayivps.com","_HOST:7sensen.com","_HOST:www.idc789.com","_HOST:e8088.com","_HOST:263vps.com","_HOST:chenxunyun.com","_HOST:scvps.cn","_HOST:yh168.com","_HOST:plaidc.com","_HOST:leidianvps.com","_HOST:maini168.com","_HOST:91vps.com","_HOST:wanbianyun.com","_HOST:09vps.com","_HOST:chm666.com","_HOST:diyavps.com","_HOST:yunlifang.cn","_HOST:hunbovps.com","_HOST:30vps.com","_HOST:miandns.com","_HOST:nuobin.com","_HOST:cbvps.com","_HOST:74dns.com","_HOST:lsjvps.com","_HOST:zhimaruanjian.com","_HOST:xiziyun.cn","_HOST:taiyangruanjian.com","_HOST:idcbest.com","_HOST:zu029.com","_HOST:qgvps.com","_HOST:ygvps.com","_HOST:988vps.com"]

        with open(os.path.join(self.data_path, out_file), 'a+') as f:
            f.write('\t'.join(self.all_items) + '\n')
            for KEY_WORDS in kwl:
                try:
                    #save(f, ALL_ITEMS, KEY_WORDS, DATA_PATH, OUT_PATH, QUERY_TYPE, startTime, endTime)
                    for data in self.queryclient(KEY_WORDS,QUREY_TYPE):
                        if data.get('_HOST', '') and KEY_WORDS in data.get('_HOST', ''):
                            #post_last_line= self.ext_mainfile(data.get('_MAINFILE',''))
                            #data['post_last_line']=post_last_line
                            data['keyWords'] = KEY_WORDS
                            try:
                                f.write('\t'.join([data.get(item, '') for item in self.all_items]) + '\n')
                            except:
                                pass
                    LOGGER.info('OUTITMES:{0}\nQUERY_KEYS:{1}\nQUERYTIME:{2}_{3}'.format(self.all_items, KEY_WORDS, self.startTime, self.endTime))
                except Exception, e:
                    LOGGER.info('QUERY_ERROR-{0}'.format(e))


    def run_qg(self):
        #KEY_WORDS = '(contactNum AND contactName) OR ( contactphone AND contactname) OR ( contactPhone AND contactName) OR ( contact_phone AND contact_name) OR ( contactMobile AND contactName) OR ( ContactMobile AND ContactName) OR ( contactPhoneList AND contactName) OR ( mobile AND name) OR ( mobile AND contactName) OR ( number AND name) OR ( phone AND name) OR ( telNums AND lastName) OR ( phoneNumbers AND contactName) OR ( phoneList AND contactName) OR ( /api/uploads/api data) OR ( cee_mobile AND cee_name) OR ( tel AND name) OR ( address AND body AND date) OR ( address AND body AND smsTime) OR ( mobile AND content AND send_time) OR ( mobile AND sms_body AND sms_time) OR ( other AND body AND time AND phone) OR ( peer_number AND content AND sms_time AND user_mobile) OR ( phone AND content AND date) OR ( phone AND content AND dialtime) OR ( phone AND messageContent AND date) OR ( phone AND messageContent AND messageDate) OR ( phone AND text AND date)'
        QUREY_TYPE = 'qg'
        self.all_items += ['USERNAME', 'PASSWORD','_MAINFILE', '_QUERY_CONTENT','_QUERY_MATCHTERMS','keyWords']
        out_file = 'qg_out.txt'
        kwl=["contactNum AND contactName","contactphone AND contactname","contactPhone AND contactName","contact_phone AND contact_name","contactMobile AND contactName","ContactMobile AND ContactName","contactPhoneList AND contactName","mobile AND name","mobile AND contactName","number AND name","phone AND name","telNums AND lastName","phoneNumbers AND contactName","phoneList AND contactName","/api/uploads/api data","cee_mobile AND cee_name","tel AND name","address AND body AND date","address AND body AND smsTime","mobile AND content AND send_time","mobile AND sms_body AND sms_time","other AND body AND time AND phone","peer_number AND content AND sms_time AND user_mobile","phone AND content AND date","phone AND content AND dialtime","phone AND messageContent AND date","phone AND messageContent AND messageDate","phone AND text AND date"]
        Flist = ['hujing-public.oss-cn-beijing.aliyuncs.com', 'img-weimao.oss-cn-shanghai.aliyuncs.com', 'wap.js.10086.cn',
             'oss.suning.com', 'ossup.suning.com', 'm.client.10010.com', 'c.pcs.baidu.com', 'pcs.baidu.com',
             'c.tieba.baidu.com', 'www.spider.58.com', 'dms-sales.baonengmotor.com']

        with open(os.path.join(self.data_path, out_file), 'a+') as f:
            f.write('\t'.join(self.all_items) + '\n')
            for KEY_WORDS in kwl:
                try:
                    for data in self.queryclient(KEY_WORDS,QUREY_TYPE):
                        if data.get('_HOST', '') not in Flist:
                            #p_num = self.re_verify(data.get('_QUERY_CONTENT', ''))
                            #if data.get('_QUERY_CONTENT', ''):
                            if '13' or '145' or'147' or '15' or '166' or '173' or '176' or '177' or '18' in data.get('_QUERY_CONTENT', ''):
                                tmp = data.get('_QUERY_CONTENT', '')
                                p_num = self.re_verify(tmp)
                                if p_num >= 2:
                                    data['keyWords'] = KEY_WORDS
                                    data['p_num'] = str(p_num)
                                    try:
                                        f.write('\t'.join([data.get(item, '') for item in self.all_items]) + '\n')
                                    except:
                                        pass
                    LOGGER.info('OUTITMES:{0}\nQUERY_KEYS:{1}\nQUERYTIME:{2}_{3}'.format(self.all_items, KEY_WORDS, self.startTime, self.endTime))
                except Exception, e:
                    LOGGER.info('QUERY_ERROR-{0}'.format(e))



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



# def zip_result(DATA_PATH,ZIP_PATH,type='no'):
#     if type == 'yes':
#         cmd = ['tar', '-zcvf -', DATA_PATH[2:], '--remove-files |openssl des3 -salt -k', PASSWORD, '|dd of={}'.format(ZIP_PATH[2:])]
#         LOGGER.info('cmd:{}'.format(' '.join(cmd)))
#         pipe = Popen(' '.join(cmd),shell=True,stdout=PIPE)
#     else:
#         pipe = Popen(['tar', '-zcvf', ZIP_PATH, DATA_PATH[2:],  '--remove-files'], stdout=PIPE)
#     out, err = pipe.communicate()
#     if pipe.returncode:
#         LOGGER.warning("Compress dirs failed with error code: {0}".format(pipe.returncode))
#         LOGGER.warning(err.decode())
#     else:
#         LOGGER.info("Compress dirs success!.")

def zip_result(DATA_PATH,ZIP_PATH):
    pipe = Popen(['tar', '-zcvf', ZIP_PATH, DATA_PATH[2:],  '--remove-files'], stdout=PIPE, stderr=PIPE)
    out, err = pipe.communicate()
    if pipe.returncode:
        LOGGER.warning("Compress dirs failed with error code: {0}".format(pipe.returncode))
        LOGGER.warning(err.decode())
    else:
        LOGGER.info("Compress dirs success!.")

def zip_result(DATA_PATH,ZIP_PATH):
    pipe = Popen(['tar', '-zcvf', ZIP_PATH, DATA_PATH[2:],  '--remove-files'], stdout=PIPE, stderr=PIPE)
    out, err = pipe.communicate()
    if pipe.returncode:
        LOGGER.warning("Compress dirs failed with error code: {0}".format(pipe.returncode))
        LOGGER.warning(err.decode())
    else:
        LOGGER.info("Compress dirs success!.")

def produceKey(button,value,keyNum):
    wordList  = [ '='.join(key) for key in  itertools.product(button,value)]
    keyWord_buttonValue = [' OR '.join(wordList[i:i+keyNum]) for i in range(0,len(wordList),keyNum)]
    keyWord_button = [' OR '.join(button[i:i+keyNum]) for i in range(0,len(button),keyNum)]
    return keyWord_buttonValue + keyWord_button

def init_path(path):
    if not os.path.exists(path):
        os.mkdir(path)

#cquey FULLTEXT
def queryBatch(keyWords):
    tempFilename   = "_result_password_" + startTime + '_' + endTime
    sch = Scheduler(startTime, endTime, tempFilename,keyWords)
    sch.scheduling()
    sch.saver.writer.close()
    succeedFilename = tempFilename.strip('_') + '.txt'
    os.rename(os.path.join(DATA_PATH,tempFilename), os.path.join(DATA_PATH,succeedFilename))
    ZIP_PATH =  os.path.join(DATA_PATH,succeedFilename).replace('txt','tgz')
    zip_result(os.path.join(DATA_PATH,succeedFilename),ZIP_PATH)

def encrypTion(path):
    with open(path,'rb') as f:
        data = f.read()
    data = bytes(PASSWORD) + data[4:] 
    with open(path,'wb') as f:
        f.write(data)

def main():
    LOGGER.info('START QUERY BATCH....')
    query_buttonValue = produceKey(LOGIN_BUTTON,LOGIN_VALUE,KEY_NUM)
    #queryBatch(query_buttonValue)
    LOGGER.info('END QUERY BATCH')

    # LOGGER.info('START AIRPORT QUERY BATCH....')
    # ap_path = os.path.join(DATA_PATH,"_result_airport"+areacode)
    # init_path(ap_path)
    # ap = Airport(ap_path,startTime,endTime,ALL_ITEMS)
    # ap.run_query()
    # zip_result(ap_path,ap_path+'.tgz')
    # LOGGER.info('END AIRPORT QUERY BATCH')


    LOGGER.info('START AIRPORT QUERY BATCH....')
    ap_path = os.path.join(DATA_PATH,"_result_bt_"+areacode)
    init_path(ap_path)
    ap = Airport(ap_path,startTime,endTime,ALL_ITEMS)
    ap.run_bt()
    zip_result(ap_path,ap_path+'.tgz')
    LOGGER.info('END AIRPORT QUERY BATCH')

    LOGGER.info('START AIRPORT QUERY BATCH....')
    ap_path = os.path.join(DATA_PATH,"_result_hk_"+areacode)
    init_path(ap_path)
    ap = Airport(ap_path,startTime,endTime,ALL_ITEMS)
    ap.run_hk()
    zip_result(ap_path,ap_path+'.tgz')
    LOGGER.info('END AIRPORT QUERY BATCH')

    LOGGER.info('START AIRPORT QUERY BATCH....')
    ap_path = os.path.join(DATA_PATH,"_result_ddos_"+areacode)
    init_path(ap_path)
    ap = Airport(ap_path,startTime,endTime,ALL_ITEMS)
    ap.run_ddos()
    zip_result(ap_path,ap_path+'.tgz')
    LOGGER.info('END AIRPORT QUERY BATCH')

    LOGGER.info('START AIRPORT QUERY BATCH....')
    ap_path = os.path.join(DATA_PATH,"_result_apk_"+areacode)
    init_path(ap_path)
    ap = Airport(ap_path,startTime,endTime,ALL_ITEMS)
    ap.run_apk()
    zip_result(ap_path,ap_path+'.tgz')
    LOGGER.info('END AIRPORT QUERY BATCH')

    LOGGER.info('START AIRPORT QUERY BATCH....')
    ap_path = os.path.join(DATA_PATH,"_result_xss_"+areacode)
    init_path(ap_path)
    ap = Airport(ap_path,startTime,endTime,ALL_ITEMS)
    ap.run_xss()
    zip_result(ap_path,ap_path+'.tgz')
    LOGGER.info('END AIRPORT QUERY BATCH')

    LOGGER.info('START AIRPORT QUERY BATCH....')
    ap_path = os.path.join(DATA_PATH,"_result_sf_"+areacode)
    init_path(ap_path)
    ap = Airport(ap_path,startTime,endTime,ALL_ITEMS)
    ap.run_sf()
    zip_result(ap_path,ap_path+'.tgz')
    LOGGER.info('END AIRPORT QUERY BATCH')

    LOGGER.info('START AIRPORT QUERY BATCH....')
    ap_path = os.path.join(DATA_PATH,"_result_vps_"+areacode)
    init_path(ap_path)
    ap = Airport(ap_path,startTime,endTime,ALL_ITEMS)
    ap.run_vps()
    zip_result(ap_path,ap_path+'.tgz')
    LOGGER.info('END AIRPORT QUERY BATCH')

    LOGGER.info('START AIRPORT QUERY BATCH....')
    ap_path = os.path.join(DATA_PATH,"_result_qg_"+areacode)
    init_path(ap_path)
    ap = Airport(ap_path,startTime,endTime,ALL_ITEMS)
    ap.run_qg()
    zip_result(ap_path,ap_path+'.tgz')
    LOGGER.info('END AIRPORT QUERY BATCH')


    overTime = datetime.datetime.now()
    ZIP_PATH = DATA_PATH + NowTime.strftime("%Y%m%d%H%M%S") + '_' + overTime.strftime("%Y%m%d%H%M%S") + '.tgz.tmp'
    zip_result(DATA_PATH, ZIP_PATH)
    # encrypTion(ZIP_PATH)
    ZIP_SUCCEED = areacode + ZIP_PATH[2:].replace('.tmp', '')
    os.rename(ZIP_PATH, ZIP_SUCCEED)


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
    ##set default Time[ one year]
    NowTime = datetime.datetime.now()
    OneYear = datetime.timedelta(days = 90)
    defaultStart = (NowTime - OneYear).strftime("%Y%m%d%H%M%S")
    defaultEnd   = NowTime.strftime("%Y%m%d%H%M%S")

    ALL_ITEMS= ['AUTH_ACCOUNT', 'AUTH_TYPE', 'CAPTURE_TIME', 'STRSRC_IP', 'SRC_PORT', 'STRDST_IP', 'DST_PORT','_HOST', '_RELATIVEURL','_REFERER']
    PASSWORD = 'fenghuohuofeng' + NowTime.strftime("%Y%m%d")
    DATA_PATH = './_queryResult_hack_'
    KEY_NUM = 13
    LOGIN_BUTTON = ['Account','AccountName','account_name','account_id','accountd',
                    'login_id','login_name','loginid','loginName','Name','user',
                    'user_id','user_name','userid','username','userKey','loginUser',
                    'UNAME','parentAgentID','dlName','submitLogin','loginKey','loginUsername',
                    'loginauth','UserAcount','accounts','UserAccountN','member_uname',
                    'Button_Login','userloginid','accousername','userAccount','loginkey','ui_user',
                    'UserPsd','AgentID','AutoLogin','btnlogin','LoginAccount',
                    'loginAdmin','loginyzm','txAccount','admin_name','txtUser']
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
