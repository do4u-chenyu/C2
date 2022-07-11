# -*- coding: utf-8 -*-
"""
2022.06.22
Modify by HZH
基本需求实现
2022.06.30
Modify by HZH
生成.net文件并加密
2022.07.04
Modify by HZH
修改.net文件的路径
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

class WaiGua:
    def __init__(self,data_path,startTime,endTime,all_items):
        self.data_path = data_path
        self.startTime = startTime
        self.endTime   = endTime
        self.all_items = all_items

    def queryclient(self,keyWords,queryOut):
        batch = []
        cont_flag = False
        cont_end_flag = True
        content = ''
        end_item = '_QUERY_MATCHTERMS'
        with open(os.path.join(self.data_path, 'fullfile.txt'), 'a+') as f:
            cmd = [
                '/home/search/sbin/queryclient',
                '--server', '127.0.0.1',
                '--port', '9870',
                '--querystring', '"{0}"'.format(keyWords),
                '--start', self.startTime,
                '--end', self.endTime,
                '--contextlen', '2000',
                '--maxcount', '1000000'
            ]
            req = Popen(". /home/search/search_profile && {0}".format(" ".join(cmd)), shell=True, stdout=PIPE)
            print ". /home/search/search_profile && {0}".format(" ".join(cmd))
            LOGGER.info('QUERYTIME:{0}_{1}\n wait...'.format(self.startTime,self.endTime)) 
            for line in req.stdout:
                f.write(line)
                line = line.replace('\x1a', '')
                if line == '\n' and cont_flag and cont_end_flag:
                    cont_flag = False
                    if batch != []:
                        data = dict(batch)
                        content = data.get('_QUERY_CONTENT')
                        if queryOut == "wpwg_user.txt":
                            data['DLMC'] = data.get('_HOST').replace(".cccpan.com", "").replace(".uepan.com", "")
                            data['USER_PWD'] = self.get_user_pwd(content)
                        else:
                            relative_url = data.get('_RELATIVEURL')
                            data['USER_PWD'] = ''
                            data['ADMIN_PWD'] = self.get_admin_pwd(content)
                            data['DLMC'] = self.get_dlmc(relative_url)
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


    def get_user_pwd(self, content):
        user_pwd = ''
        for part in re.split(r'\.\.\.',content):
            if "teqtbz=" not in part:
                continue
            for line in re.split('[\r\n&;]+',part):
                try:
                    [key, value] = line.strip().split("=")
                    if len(value.strip()) < 1:
                        continue
                    if "teqtbz" in key:
                        user_pwd = value
                except:
                    pass
        return user_pwd


    def get_admin_pwd(self, content):
        admin_pwd = ""
        for part in re.split(r'\.\.\.',content):
            if "glmm" not in part:
                continue
            for line in re.split('[\r\n&;]+',part):
                try:
                    [key, value] = line.strip().split("=")
                    if len(value.strip()) < 1:
                        continue
                    if key == "glmm":
                        admin_pwd = value
                except:
                    pass
        return admin_pwd


    def get_dlmc(self, relative_url):
        dlmc = ""
        for part in re.split('&',relative_url):
            if "dlmc" not in part:
                continue
            try:
                [key, value] = part.strip().split("=")
                if len(value.strip()) < 1:
                    continue
                if "dlmc" in key:
                    dlmc = value
            except:
                pass
        return dlmc


    def run_query(self, KEY_WORDS, out_file):
        if out_file == "wpwg_admin.txt":
            self.all_items += ['ADMIN_PWD']
        with open(os.path.join(self.data_path, out_file), 'a+') as f:
            f.write('\t'.join(ALL_ITEMS) + '\n')
            try:
                LOGGER.info('OUTITMES:{0}\nQUERY_KEYS:{1}\nQUERYTIME:{2}_{3}'.format(self.all_items, KEY_WORDS, self.startTime, self.endTime))
            except Exception, e:
                LOGGER.info('QUERY_ERROR-{0}'.format(e))
            for data in self.queryclient(KEY_WORDS,out_file):
                if (not data.get('DLMC', '')) or (not data.get('_REFERER', '')):
                    continue
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


def macth_admin(DATA_PATH):
    admin_dict = {}
    result_list = []
    with open(os.path.join(DATA_PATH, 'wpwg_admin.txt'), 'r') as f:
        lines = f.readlines()
        index = lines[0].split("\t").index("DLMC")
        for line in lines[1:]:
            line = line.strip('\n').split("\t")
            if line[index] not in admin_dict.keys():
                admin_dict[line[index]] = []
            if line[index + 2] not in admin_dict[line[index]]:
                admin_dict[line[index]].append(line[index + 2])
    if admin_dict == {}:
        return result_list
    wpwg_out = []
    wpwg_user = []
    with open(os.path.join(DATA_PATH, 'wpwg_out.txt'), 'a+') as f:
        with open(os.path.join(DATA_PATH, 'wpwg_user.txt'), 'r') as fs:
            lines = fs.readlines()
            header_list = lines[0].split("\t")
            index = header_list.index("DLMC")
            f.write(lines[0].strip('\n').replace("DLMC\t", "") + "\t" + "ADMIN_PWD" + "\n")
            wpwg_user.append(header_list[index - 1] + "\t" + header_list[index + 1])
            wpwg_out.append(header_list[index - 1] + "\t" + header_list[index + 1].strip('\n') + "\t" + "ADMIN_PWD" + "\n")
            for line in lines[1:]:
                line_list = line.strip('\n').split("\t")
                wpwg_user.append(line_list[index - 1] + "\t" + line_list[index + 1] + "\n")
                if line_list[index] not in admin_dict.keys():
                    continue
                for pwd in admin_dict[line_list[index]]:
                    line_list.remove(line_list[index])
                    f.write("\t".join(line_list) + "\t" + pwd + "\n")
                    wpwg_out.append(line_list[index - 1] + "\t" + line_list[index] + "\t" + pwd + "\n")
    writer_outer_result(wpwg_out, 'wpwg_out.txt')
    writer_outer_result(wpwg_user, 'wpwg_user.txt')


def writer_outer_result(result_list, result_file):
    with open(os.path.join(OUT_PATH, result_file), 'a+') as f:
        for result in result_list:
            f.write(result)

def encrypTion(path):
    with open(path,'rb') as f:
        data = f.read()
    data = data[0:3] + bytes(PASSWORD) + data[3:]
    with open(path,'wb') as f:
        f.write(data)


def zip_result(DATA_PATH, ZIP_PATH):
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
    LOGGER.info('START WAIGUA QUERY BATCH....')
    key_word_user = '(cccpan.com OR uepan.com) AND login AND teqtbz='
    key_word_admin = '(cccpan.com OR uepan.com) AND glmm='
    out_user_file = 'wpwg_user.txt'
    out_admin_file = 'wpwg_admin.txt'
    ap = WaiGua(DATA_PATH, startTime, endTime, ALL_ITEMS)
    ap.run_query(key_word_user, out_user_file)
    ap.run_query(key_word_admin, out_admin_file)
    macth_admin(DATA_PATH)
    LOGGER.info('END WAIGUA QUERY BATCH')

    ZIP_PATH = DATA_PATH + startTime + '_' + endTime + '.tgz.tmp'
    zip_result(DATA_PATH,ZIP_PATH)
    ZIP_SUCCEED = areacode +  ZIP_PATH[2:].replace('.tmp','')
    os.rename(ZIP_PATH,ZIP_SUCCEED)

    NET_PATH = OUT_PATH + startTime + '_' + endTime + '.tgz.tmp'
    zip_result(OUT_PATH,NET_PATH)
    encrypTion(NET_PATH)
    os.rename(NET_PATH, NET_PATH[2:].replace('.tmp',''))



if __name__ == '__main__':
    usage = 'python batchquery_wpwg_accountPass_C2_20220622.py --start [start_time] --end [end_time] --areacode [areacode]'
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

    PASSWORD = 'fenghuohuofeng' + NowTime.strftime("%Y%m%d")
    ALL_ITEMS= ['AUTH_ACCOUNT', 'AUTH_TYPE', 'CAPTURE_TIME', 'STRSRC_IP', 'SRC_PORT', 'STRDST_IP', 'DST_PORT','_HOST', '_RELATIVEURL','_REFERER', 'DLMC', 'USER_PWD']
    DATA_PATH = './_queryResult_wpwg_'
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

    OUT_PATH = './outer_queryResult_wpwg_'
    init_path(OUT_PATH)
    main()
