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
reload(sys)
sys.setdefaultencoding('utf-8')

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
                self.queryclient_keyWordQueue.put(line)
        LOGGER.info("keyword query group number：{}".format(len(self.queryWords)))
            
    def scheduling(self):
        print "start flush thread"
        self.saver.daemon = True
        self.saver.start()
        
        print "start query thread"
        for i in xrange(len(self.queryWords)):
            worker = Query(self.queryclient_keyWordQueue, self.queryclient_resultQueue, self.startTime, self.endTime)
            worker.daemon = True
            worker.start()
            self.workers.append(worker)
        self.saver.workers = self.workers
        
        self.queryclient_keyWordQueue.join()
        self.queryclient_resultQueue.join()

class Query(Thread):
    def __init__(self, queryclient_keyWordQueue, queryclient_resultQueue,start_time,end_time):
        Thread.__init__(self)
        self.configDict   = {'AUTH_ACCOUNT': 0,'STRDST_IP': 1,'STRSRC_IP': 2,'_REFERER': 3,'_QUERY_CONTENT': 4,'SRC_IPID': 5,'DST_IPID': 6,'CAPTURE_TIME': 7}
        self.lineEnd      = '_USERAGENT'
        self.user_key     = ['account','uid','name','phone','username','userid']
        self.password_key = ['password','pwd','pass','key']
        self.other_key    = {'safecode':['safepass','code']}
       
        self.lineEndMarkLength  =len(self.lineEnd)
        self.exitRefer          = False
        self.queryContentFlag   = False
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
            LOGGER.info('query Time:{}_{}'.format(queryStart,queryEnd))
            self.queryclient_keyWordQueue.task_done()

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
            '--datatype', 'normal,garbage',
            '--maxcount', '2147483647'
        ]
        if '=' not in key_word:
            dbfilter = '\'' + ' OR '.join(['"' + value + '"' +  ' in _REFERER'  for value in  LOGIN_VALUE]) + '\''
            cmd = cmd + ['--dbfilter',dbfilter]
        req = Popen(". /home/search/search_profile && {}".format(" ".join(cmd)), shell=True, stdout=PIPE)
        for line in req.stdout:
            if 'query finished' in line:
                LOGGER.info('queryCmd:' + ". /home/search/search_profile && {}".format(" ".join(cmd)) + '-----' +line.strip())
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
        
def produceKey(button,value,keyNum):
    wordList  = [ '='.join(key) for key in  itertools.product(button,value)]
    keyWord_buttonValue = [' OR '.join(wordList[i:i+keyNum]) for i in range(0,len(wordList),keyNum)]
    keyWord_button = [' OR '.join(button[i:i+keyNum]) for i in range(0,len(button),keyNum)]
    return keyWord_buttonValue + keyWord_button

def init_path(path):
    if not os.path.exists(path):
        os.mkdir(path)

def queryBatch(keyWords):
    tempFilename   = "_result_password_" + startTime + '_' + endTime 
    sch = Scheduler(startTime, endTime, tempFilename,keyWords)
    sch.scheduling()
    sch.saver.writer.close()
    succeedFilename = tempFilename.strip('_') + '.txt'
    os.rename(os.path.join(DATA_PATH,tempFilename), os.path.join(DATA_PATH,succeedFilename))
    ZIP_PATH =  os.path.join(DATA_PATH,succeedFilename).replace('txt','')
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
    queryBatch(query_buttonValue)
    LOGGER.info('END QUERY BATCH')

    overTime =  datetime.datetime.now()
    ZIP_PATH = DATA_PATH + NowTime.strftime("%Y%m%d%H%M%S") + '_' + overTime.strftime("%Y%m%d%H%M%S") +  '.tgz.tmp'
    zip_result(DATA_PATH,ZIP_PATH)
    #encrypTion(ZIP_PATH)
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
  
    option,args = parser.parse_args()
    startTime   = option.startTime
    endTime     = option.endTime
    areacode    = option.areacode
    
    NowTime = datetime.datetime.now()
    OneYear = datetime.timedelta(days = 90)
    defaultStart = (NowTime - OneYear).strftime("%Y%m%d%H%M%S")
    defaultEnd   = NowTime.strftime("%Y%m%d%H%M%S")
   
    PASSWORD = 'fenghuohuofeng' + NowTime.strftime("%Y%m%d")
    DATA_PATH = './_queryResult_db_'
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