# -*- coding: utf-8 -*-

"""
Created on Sat Nov  2 15:08:13 2019
@author: 
--20191103
v1
   1.keyWords only support QQ
   2.production environment not support xlsx
   3.drop dbfilter use
   4.add output param and ipconfig

--20191111
v2 
   1.add net get
   
@author: IAO_X5149 bly
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
from optparse import OptionParser


from os.path import join
from operator import itemgetter
from itertools import groupby
#from openpyxl import Workbook
#from concurrent import futures
reload(sys)
sys.setdefaultencoding('utf-8')


ITEMS = {
    "PIC": {
        "content": "_QUERY_CONTENT",
        "end": "_QUERY_MATCHTERMS",
        "items": ['PROTYPE','AUTH_ACCOUNT','USERNAME','USERID','GROUPNUM','CAPTURE_TIME',
                  'STRSRC_IP','FILENAME','FROM_ID','TO_ID','_MAINFILE'],
        "key_words": "CONTENTTAGS_TAG_ID:W01AAG00",
    },
    "EMAIL": {
        "content": "_QUERY_CONTENT",
        "end": "_QUERY_MATCHTERMS",
        "items": ['AUTH_ACCOUNT', '_FROM','_FROM_PASS', '_SUBJECT','_QUERY_CONTENT',
                  'ACTION','_RCPTTO','_RCPTTO_PASS','CAPTURE_TIME',
                  'USERNAME','PASSWORD'],
        "key_words": "(波推 OR 胸推 OR 打飞机 OR 女优 OR 高清无码 OR 包夜 OR 胸大OR 乱伦 OR 人兽 OR 人妻 OR 乱仑 OR 卵仑) AND (FROM_PROTYPE:101)"
    }
}
ILLEGAL_CHARACTERS_RE = re.compile(r'[\000-\010]|[\013-\014]|[\016-\037]')

proTypeDict = {'WA_SOURCE_0048':'WX',
               '5710001':'HTTP_POST',
               '5710013':'QQ',
               '5710014':'WX',
               '5710088':'115网盘'
             }


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
            '--server', serverIP,
            '--port', '9870',
            '--querystring', "'" + key_word + "'",
            '--start', startTime,
            '--end', endTime,
            '--contextlen', '1000',
            '--maxcount', '2147483647'
        ]
        LOGGER.info("""
        Start query
        query config:
        start_time:{0}
        end_time:{1}
        keywords:{2}
        """.format(startTime, endTime, key_word.decode('gbk').encode('utf8')))
        if '=' not in key_word:
            dbfilter = '\'' + ' OR '.join(['"' + value + '"' +  ' in _REFERER'  for value in  LOGIN_VALUE]) + '\''
            cmd = cmd + ['--dbfilter',dbfilter]
        _cmd = ". /home/search/search_profile && {0}".format(" ".join(cmd))
        LOGGER.info("cmd: {0}".format(_cmd))
        req = Popen(_cmd, shell=True, stdout=PIPE) 
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
        self.writer = open(os.path.join(netPath,self.save_path), 'a+')
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
        if self.get_FileSize(netPath) > 50:
            succeedFilename = self.save_path.strip('_') + '_' + datetime.datetime.now().strftime("%Y%m%d%H%M%S") + '.txt'
            os.rename(os.path.join(netPath,self.save_path), os.path.join(netPath,succeedFilename))
            ZIP_PATH =  os.path.join(netPath,succeedFilename).replace('.txt','')
            zip_result(os.path.join(netPath,succeedFilename),ZIP_PATH)
            self.writer.close()
            self.writer = open(os.path.join(netPath,self.save_path), 'a+')
    def get_FileSize(self,dataPath):
        filePath = unicode(self.save_path,'utf-8')
        fsize    = os.path.getsize(os.path.join(dataPath,filePath))
        fsize    = fsize/float(1024*1024)
        return round(fsize,2)
##日志文件打印
def init_logger(path, isdebug=False):
    if isdebug:
        logger_level = logging.DEBUG
    else:
        logger_level = logging.INFO
    logger = logging.getLogger('queryclient_logger')
    logger.setLevel(logger_level)
    fh = logging.FileHandler(path)
    fh.setLevel(logging.DEBUG)
    ch = logging.StreamHandler()
    ch.setLevel(logging.DEBUG)
    formatter = logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s')
    fh.setFormatter(formatter)
    ch.setFormatter(formatter)
    logger.addHandler(fh)
    logger.addHandler(ch)
    return logger

##init file path
def init_path(paths):
    for _path in paths:
        if not os.path.exists(_path):
            os.mkdir(_path)

def queryclient(keywords, start_time, end_time, cont_item, end_item, items,
                contextlen='1000',max_count='2147483647'):
    """
    进行全文查询，解析后返回给定字段的块文件的生成器
    *Note*: 中文必须是gbk编码string
    :param keywords: 查询的关键词逻辑语句(str)
    :param start_time: 开始时间(str)
    :param end_time: 结束时间(str)
    :param cont_item: 内容字段(str)。
    :param end_item: 内容结束字段(str)。指的是内容字段下一个字段名
    :param items: 待解析字段(List[str])。
    :param contextlen: 文本长度(str)
    :param max_count: 最大返回条数(str)
    :return: data(dict)。一个全文block的结构化数据
    """
    batch = []
    cont_flag = False
    cont_end_flag = False
    content = ''
    # TODO 异常处理
    # 深圳全文堡垒机上有多重数据源，目前深圳全文堡垒机是用来查询QQ群聊数据的，因此给出的dbfilter参数
    # 对于深圳堡垒机，'DATATYPE="QQGROUPMSG"'
    # 对于wx堡垒机， 'DATATYPE="103001"'
    cmd = [
        '/home/search/sbin/queryclient',
        '--server', serverIP,
        '--port', '9870',
        '--querystring', '"{0}"'.format(keywords),
        '--start', start_time,
        '--end', end_time,
        '--contextlen', contextlen,
        '--maxcount', max_count
    ]
    _cmd = '. /home/search/search_profile && {0}'.format(' '.join(cmd))
    LOGGER.info("cmd: {0}".format(_cmd))
    req = Popen(_cmd, shell=True, stdout=PIPE)
    LOGGER.debug("queryclient pid: {0}".format(req.pid))
    LOGGER.info("""
    Start query
    query config:
        start_time:{0}
        end_time:{1}
        keywords:{2}
        contextlen:{3}
        maxcount:{4}
    """.format(start_time, end_time, keywords.decode('gbk').encode('utf8'),
               contextlen, max_count))

    for line in req.stdout:
#    with open('picSh_oneDay') as f:
#        for line in f:
        #LOGGER.debug("The original line is: {}".format(line))
        if line == '\n' and not cont_flag:
            if batch:
                data = dict(batch)
                yield data
                batch = []
        elif ':' in line:
            k, v = map(lambda x: x.strip(), line.split(':', 1))
            if k == end_item:
                # cont_end_flag = True
                # 多行content
                batch.append((cont_item, content.replace('\t', ' ').replace('\n', ' ')))
                content = ''
                cont_flag = False
                cont_end_flag = False
            if items and k in items:
                if k == cont_item:
                    cont_flag = True
                    content = v
                else:
                    batch.append((k, v))
        else:
            if cont_flag and not cont_end_flag:
                content += line
                




def multi_query(start_time_1, end_time_1,  start_time_2,
                contextlen='300', max_count='2147483647', length=300):
    """
    """

    data_type = 'EMAIL'
    query_config = {
        'keywords': ITEMS[data_type]["key_words"].decode("utf8").encode("gbk"),
        'start_time': start_time_1,
        'end_time': end_time_1,
        'items': ITEMS[data_type]["items"],
        'cont_item': ITEMS[data_type]["content"],
        'end_item': ITEMS[data_type]["end"],
        'contextlen': contextlen,
        'max_count': max_count
    }
    LOGGER.info(
        "Query shehuang Email: \n"
        "\tkeywords: {0}, \n"
        "\tstart_time: {1}, \n"
        "\tend_time: {2}, \n".format(
           query_config['keywords'].decode("gbk").encode("utf8"), 
           start_time_1, end_time_1, 
        ))
    emailDicts = []
    counter = 0
    for data in queryclient(**query_config):
        authAccount = data.get('AUTH_ACCOUNT', None)
        if not authAccount:
            continue
        counter += 1
        emailDicts += [data]
    LOGGER.info("数据量：{0}".format(counter))
    data_type = 'PIC'
    query_config = {
        'keywords': ITEMS[data_type]["key_words"],
        'start_time': start_time_2,
        'end_time': end_time_1,
        'items': ITEMS[data_type]["items"],
        'cont_item': ITEMS[data_type]["content"],
        'end_item': ITEMS[data_type]["end"],
        'contextlen': contextlen,
        'max_count': max_count
    }
                                                 

    picDicts= []

    counter = 0
    for data in queryclient(**query_config):
        authAccount = data.get('AUTH_ACCOUNT', None)
        username = data.get('USERNAME', '')
        userid   = data.get('USERID', '')
        toId     = data.get('TO_ID','')
        fromId   = data.get('FROM_ID','')
        if (not authAccount and  not username and  not userid) or (toId and not fromId):
            continue
        if toId and toId  == username:
            username = fromId
            data['USERNAME'] = fromId
            authAccount = ''
            data['AUTH_ACCOUNT'] = ''
        if toId and toId  == username:
            userid = fromId
            data['USERID'] = fromId
            authAccount = ''
            data['AUTH_ACCOUNT'] = ''
        data['CAPTURE_TIME'] = time_change(data.get('CAPTURE_TIME',''))
        data['PROTYPE'] =  proTypeDict.get(data.get('PROTYPE',''),'其他')
        data['MD5'] = str(authAccount) + ':' + str(username) + ':' + str(userid)
        picDicts += [data]
        counter += 1
    LOGGER.info("数据量：{0}".format(counter))
        #break
    return emailDicts,picDicts

def time_change(inputTime):
    
    timeArray = time.strptime(inputTime,"%Y%m%d%H%M%S")
    return time.strftime("%Y/%m/%d/ %H:%M:%S",timeArray)
    
    

  

def emailExact(dataDict):
    LOGGER.info("start email exact data")

    shEmail = [['认证账号', '发件人账号', '发件人密码', '邮件主题', '邮件内容', '动作', 
                  '收件人账号', '收件人密码', '截获时间', '命中关键词', '标签']]
    key_word = ['波推','胸推','打飞机','口吹','口活','口交','外围','莞式一条龙','AV',
                '女优','高清无码','包夜','现在空','上门','一招一式',
                '一照一视','胸大','中圈','大圈','小圈','全套服务','乱伦','人兽',
                '人妻','乱仑','卵仑' ]

    for data in dataDict:
        info  = [data.get(item, "") for item in ITEMS['EMAIL']['items']]
        info[-3] = time_change(info[-3])
        content = info[4]
        wordHit  = '('+ ')('.join([','.join([key,str(content.count(key))]) 
                   for key in key_word if content.count(key) != 0])+')' 
        if len(wordHit) == 2 or info[5]!='31':
            continue
        tmp = info[:-2] + [wordHit,'传播涉黄视频']
        userName,passWord = info[-2:]
        if not passWord:
            shEmail += [tmp]
            continue
        if userName in tmp[1]:
            tmp[2] = passWord
        if userName in tmp[6]:
            tmp[7] = passWord
        shEmail += [tmp]
    return shEmail


def download_one(image):    
    with open(join(down_path,image['name']),'wb') as f:
        f.write((urllib.urlopen(image['url'])).read())
        
def picExact(picDicts):
    LOGGER.info("start email pic data")
    shPic = [['应用类型', '认证账号', '发送者账号', '发送者ID', '群号', '发送时间',
              '发送IP', '图片名']]
    images = []
    picDicts.sort(key = itemgetter('MD5'))
    
    picDicts_1 = groupby(picDicts,key = itemgetter('MD5'))
    for key,gp in picDicts_1:
        info  = [[g.get(item,'') for item in ITEMS['PIC']['items']] for g in gp]
        if len(info) < 10:continue
        images += [{'name':line[-4],'url':line[-1]} for line in info]
        shPic  += [line[:-3] for line in info]
    LOGGER.info("downloading...")
    for image in images:
        download_one(image)
#    workers = 10
#    with futures.ThreadPoolExecutor(workers) as executor:
#        to_do = [executor.submit(download_one, image) for image in images]    
    return shPic



def zip_result(DATA_PATH,ZIP_PATH):
    LOGGER.info("ZIP DATA...")
    _cmd = "tar -zcvf {0} {1} --remove-files".format(ZIP_PATH,DATA_PATH)
    Popen(_cmd, shell=True).wait()
    
def dataFormat(content):
    try:
        return unicode(content,"utf-8")
    except:
        try:
            return unicode(content,"gbk")
        except:
            return u'很抱歉!转码失败'
        

    
def save(data):
    LOGGER.info("start save data")
#    fileNames = [u'涉黄邮件',u'涉黄图片']
#    workbook = Workbook()
#    path = join(dataPath,areacode + '_shData_' +  defaultEnd[:8]  +  '.xlsx')
#    for i in range(len(fileNames)):
#        booksheet = workbook.create_sheet(fileNames[i])
#        for line in data[i]:
#            line = map(dataFormat,line)
#            line = [ILLEGAL_CHARACTERS_RE.sub(r'',tmp) for tmp in line]
#            booksheet.append(line)
#    workbook.remove(workbook['Sheet'])
#    workbook.save(path)
    with open(join(dataPath,'shehuang_email.txt'),'w+') as f:
        for line in data[0]:
            f.write('\t'.join(line) + '\n')
    
    with open(join(dataPath,'shehuang_pic.txt'),'w+') as f:
        for line in data[1]:
            f.write('\t'.join(line) + '\n')
    

        
def produceKey(button,value,keyNum):
    wordList  = [ '='.join(key) for key in  itertools.product(button,value)]
    keyWord_buttonValue = [' OR '.join(wordList[i:i+keyNum]) for i in range(0,len(wordList),keyNum)]
    keyWord_button = [' OR '.join(button[i:i+keyNum]) for i in range(0,len(button),keyNum)]
    return keyWord_buttonValue + keyWord_button



##quey FULLTEXT 
def queryBatch(keyWords):
    tempFilename   = "_result_password_" + netQueryStart + '_' + defaultEnd 
    sch = Scheduler(netQueryStart, defaultEnd, tempFilename,keyWords)
    sch.scheduling()
    sch.saver.writer.close()
    succeedFilename = tempFilename.strip('_') + '.txt'
    os.rename(os.path.join(netPath,tempFilename), os.path.join(netPath,succeedFilename))
    ZIP_PATH =  os.path.join(netPath,succeedFilename).replace('.txt','.tgz')
    zip_result(os.path.join(netPath,succeedFilename),ZIP_PATH)

##query pic&&Email shehuang
def queryPicEmail():    
    emailDicts,picDicts = multi_query(emailQueryStart, defaultEnd, picQueryStart)
    emailList = emailExact(emailDicts)
    picList   = picExact(picDicts)
    save([emailList,picList])
    LOGGER.info("end Query all wait..")

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
    LOGGER.info('END QUERY BATCH\n START QUERY PIC')
    queryPicEmail()
    
    zip_path = "{0}_{1}_{2}_{3}.tgz_".format(areacode,'queryResult_yellow',defaultStart[:14],defaultEnd[:14])
    zip_result(dataPath,zip_path)
    #encrypTion(zip_path)
    succeedFilename = zip_path.strip('_') 
    os.rename(zip_path,succeedFilename)
    
    
if __name__ == '__main__':
    ##Program description
    usage = 'python query_yellowWish_v4.py  --server[serverIp]  --netDay[queryNetDays]  --emailDay[queryEmailDays]  --picDay[queryPicDays]  --out[outfilePath]  --area[areaCode]'
  
    serverInfo = 'the query addr,default 127.0.0.1'
    netDay     = 'query netData days,default 0.1 days'
    emailDay   = 'query emailData days,default 90 days'
    picDay     = 'query picData days,default 90 days'
    outInfo    = 'Output file directory,default ./'
    areaInfo   = 'area code,default 000000'

    parser = OptionParser(usage)
    parser.add_option('--server',dest = 'serverIp', help = serverInfo,default = '127.0.0.1')
    parser.add_option('--netDay',dest = 'netQueryDay', help = netDay,default = '90')
    parser.add_option('--emailDay',dest = 'emailQueryDay', help = emailDay,default = '90')
    parser.add_option('--picDay',dest = 'picQueryDay', help = picDay,default = '90')
    parser.add_option('--out',dest = 'outfilePath', help = outInfo,default = sys.path[0])
    parser.add_option('--area',dest = 'areaCode', help = areaInfo,default = '000000')
    ##get input Time  parameter
    option,args = parser.parse_args()
    if not os.path.exists(sys.path[0] + '/result'):
        os.mkdir(sys.path[0] + '/result')
    dataPath = option.outfilePath + '/result'
    serverIP = option.serverIp
    areacode = option.areaCode
    netDay   = option.netQueryDay
    emailDay = option.emailQueryDay
    picDay   = option.picQueryDay
    
    down_path = join(dataPath,'pic')
    ##set default Time[ one year]
    NowTime = datetime.datetime.now()
    netQueryStart  = (NowTime - datetime.timedelta(days = float(netDay))).strftime("%Y%m%d%H%M%S")
    emailQueryStart= (NowTime - datetime.timedelta(days = float(emailDay))).strftime("%Y%m%d%H%M%S")
    picQueryStart= (NowTime - datetime.timedelta(days = float(picDay))).strftime("%Y%m%d%H%M%S")

    defaultEnd   = NowTime.strftime("%Y%m%d%H%M%S")
    defaultStart = picQueryStart
   
    PASSWORD = 'fenghuohuofeng' + NowTime.strftime("%Y%m%d")
    netPath = join(dataPath,'queryResult_net_')
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
    init_path([dataPath,join(dataPath,"Logs"),netPath,down_path])
    log_path = join(dataPath, "Logs", "{0}.log".format(NowTime.strftime("%Y%m%d%H%M%S")))
    LOGGER = init_logger(log_path)

    if  len(areacode) !=6: 
        LOGGER.info('areacode error:'+ areaInfo)
        sys.exit(1)
    main()

