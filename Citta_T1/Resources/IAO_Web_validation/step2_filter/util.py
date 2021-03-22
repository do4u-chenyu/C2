import logging
import os.path
from os.path import exists, join
import time
import re
from collections import Counter
import sys
from urllib.parse import urlparse, unquote
from subprocess import PIPE, Popen
import json
from typing import Dict, Tuple
from argparse import ArgumentParser
from operator import itemgetter, attrgetter

LOG_LEVEL_DICT = {
    'debug': logging.DEBUG,
    'info': logging.INFO,
    'warn': logging.WARNING,
    'error': logging.ERROR
}

CHINESE = re.compile(r'[\u4E00-\u9FA5]+')
NO_CHINESE = re.compile(r"[^\u4E00-\u9FA5]+?\s")


class Logger:

    def __init__(self, logger: str, is_debug=False):
        """
        指定保存日志的文件路径，日志级别，以及调用文件
            将日志存入到指定的文件中
        :param logger: 日志名
        """
        # 创建一个logger
        self.logger = logging.getLogger(logger)
        # self.logger.setLevel(LOG_LEVEL_DICT[log_level])
        if is_debug:
            logger_level = logging.DEBUG
        else:
            logger_level = logging.INFO
        # 相对路径，可以换成绝对路径
        self.path = './Logs'
        if not exists(self.path):
            os.mkdir(self.path)
        # 创建一个handler，用于写入日志文件
        self.rq = time.strftime('%Y%m%d%H%M', time.localtime(time.time()))
        self.logger.setLevel(logger_level)
        log_name = join(self.path, self.rq + '.log')
        fh = logging.FileHandler(log_name)
        fh.setLevel(logging.DEBUG)

        # 再创建一个handler，用于输出到控制台
        ch = logging.StreamHandler()
        ch.setLevel(logging.DEBUG)

        # 定义handler的输出格式
        formatter = logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s')
        fh.setFormatter(formatter)
        ch.setFormatter(formatter)

        # 给logger添加handler
        self.logger.addHandler(fh)
        self.logger.addHandler(ch)

    def getlog(self):
        return self.logger, self.rq


def pad_url(url):
    """
    1.url补全http
    2.url填充可能域名
    :param url:
    :return:
    """
    ret = []
    url_struct = url.split('.')
    if len(url_struct) == 2:
        ret.append('http://' + url if 'http' not in url else url)
        ret.append('http://www.' + url if 'http' not in url else url)
    if len(url_struct) == 3:
        ret.append('http://' + url if 'http' not in url else url)
        ret.append('http://www.' + '.'.join(url_struct[1:]) if 'http' not in url else 'www.' + '.'.join(url_struct[1:]))
    return ret


def is_chinese_web(row) -> int:
    """
    是否是中文网站
    1. 是否包含中文
    2. 有login且外文词数不多
    :param row:
    :return:
    """
    fulltext = str(row['title']) + str(row['keywords']) + str(row['description']) + str(row['form_data'])
    if row['title'] == '' and row['keywords'] == '' and row['description'] == '':
        return 1
    elif CHINESE.findall(fulltext):
        return 1
    elif len(NO_CHINESE.findall(fulltext)) < 400:
        return 1
    else:
        return 0


def word_filter(file_path: str) -> {}:
    """
    对爬取结果进行关键词打标，只打标不过滤，关键词的作用需要考究
    关键词文件，以\r\n分割
    :param file_path:    爬虫结果文件
    :return:        打标结果 DataFrame
    """

    def mark(row, words_list: list) -> Dict[str, int]:
        """
        返回的标记为一个字典，字典形如{"keywords1": 1, "keywords1": 2}
        :param words_list:
        :param row:
        :return:
        """
        # 关键词正则，终于统计词频
        p1 = re.compile(r'{}'.format('|'.join([words.strip() for words in words_list])))
        res = []
        res.extend(p1.findall(row))
        res = dict(Counter(res))
        return res if res != {} else {}

    def count_input(row: str) -> int:
        """
        没有密码框的返回0，只有有密码框的时候才计算框的数量
        :param row: '[int(是否有密码框，有则1无则0), List[str](所有框的name，包括text框和password框)]'
        :return:
        """
        if isinstance(row, str):
            pwd_nums, keys = json.loads(row)
            if pwd_nums:
                return len(keys)
            else:
                return 0
        else:
            return 0

    # 读取关键词
    with open('./keywords/neg_keywords.txt', encoding='utf8') as f:
        keywords = f.read()
    neg_words = keywords.split('\n')
    with open('./keywords/pos_keywords.txt', encoding='utf8') as f:
        keywords = f.read()
    pos_words = keywords.split('\n')

    # lxf pandas功能替换
    # file_path各列字段名称 ['referer', 'icp', 'title', 'keywords', 'description', 'names']
    null_file_assert(file_path)
    f = open(file_path, encoding='utf8', errors="ignore")
    # 返回refer为key的字典
    new_row_list = {}
    while 1:
        lines = f.readlines(10000)
        if not lines:
            break
        for line in lines:
            fields_list = line.strip('\r\n').split('\t')
            # 文件应该包含6个列字段
            if len(fields_list) < 6:
                continue
            #  追加[icp_label,neg_words_label,pos_words_label,input_num]列字段
            icp_label_value = '1' if 'ICP' in str(fields_list[1]).upper() else '0'
            fields_list.append(icp_label_value)
            neg_words_label_value = mark(line, neg_words)
            fields_list.append(str(neg_words_label_value))
            pos_words_label_value = mark(line, pos_words)
            fields_list.append(str(pos_words_label_value))
            input_num_value = count_input(fields_list[5])
            fields_list.append(str(input_num_value))
            new_row_list[fields_list[0]] = fields_list
    f.close()
    return new_row_list


def ip_addr(ip):
    cmd = 'curl "http://ip.taobao.com/service/getIpInfo.php?ip={}" -s'.format(ip)
    ret, err = Popen(cmd, stdout=PIPE, shell=True).communicate()
    country = ''
    try:
        country = json.loads(ret.decode())['data']['country']
    except:
        pass
    return country


def pre_filter(recent_hosts: [], col_names: [], logger: Logger = None) -> []:
    """
    爬虫前的预处理，4步
    输入数据是读取的源文件

    1. ip过滤。过滤ip中以115的开头的
    2. host过滤。过滤掉host中包含政府，教育机构后缀的
    3. URL规则规律。过滤掉域名中不包含数字的
    4. urldecode。将密码urldecode一下
    :param col_names:
    :param recent_hosts:
    :param logger:
    :return:
    """

    filter_list = []
    if logger:
        logger.info("=" * 30)
        logger.info("爬取前的数据预处理，输入数据共{}条".format(len(recent_hosts)))
    for row in recent_hosts:
        if 'IPADDR' not in col_names and re.findall('^1156', row[col_names.index('dst_ip_id')]):
            continue
        if re.findall('org|edu|mil|gov|biz', row[col_names.index('HOST')]):
            continue
        if not re.findall('\d', row[col_names.index('HOST')]):
            continue
        row[col_names.index('PASSWORD')] = unquote(row[col_names.index('PASSWORD')])
        filter_list.append(row)
    if logger:
        logger.info("过滤掉ip以1156开头的数据，过滤掉政府、教育、组织机构网站数据,过滤URL中不包含数字的数据,剩余{}条".format(len(filter_list)))
        logger.info("=" * 30)
    return filter_list


def uni_format(file_path: str, file_from: str, id: str, logger: Logger = None) -> None:
    """
    读取源文件，输入index, host, referer 三列，作为爬虫数据的输入文件。
    会生成两个文件在本地：
        1. `{file_from}.tsv`: 对全文原文本文件预处理之后的文件，
        2. `referers.tsv`: 预处理之后REFERER去重生成的文件，给爬虫使用
    :param file_path:
    :param file_from:
    :return:
    """
    p_http = re.compile("(https?|ftp|file)[-A-Za-z0-9+&@#/%?=~_|!:,.;]+[-A-Za-z0-9+&@#/%=~_|]")
    p_no_http = re.compile("[-A-Za-z0-9+&@#/%?=~_|!:,.;]+[-A-Za-z0-9+&@#/%=~_|]")

    def _urlparse(x):
        ret1 = p_http.findall(x)
        ret2 = p_no_http.findall(x)
        if ret1:
            try:
                return urlparse(x).netloc
            except:
                return ""
        elif ret2:
            try:
                x = "http://" + x
                return urlparse(x).netloc
            except:
                return ""
        else:
            return ""

    if file_from == 'wu':
        col = ['AUTH_ACCOUNT', 'dst_ip', 'src_ip', 'REFERER', 'form_data', 'USERNAME', 'PASSWORD', 'code', 'src_ip_id',
               'dst_ip_id', 'CAPTURE_TIME']
    elif file_from == 'tang':
        col = ['AUTH_ACCOUNT', 'CAPTURE_TIME', 'HOST', 'REFERER', 'TITLE', 'IPADDR', 'USERNAME', 'PASSWORD']
    else:
        sys.exit(-1)
    total_count = 0
    filter_rows = []
    with open(file_path, encoding='utf8', errors='ignore') as f:
        for row in f:
            total_count += 1
            if len(col) != len(row.split('\t')):
                continue
            row_dict = dict(zip(col, row.split('\t')))
            if len(row_dict['REFERER']) == 0 or len(row_dict['USERNAME']) == 0:
                continue
            if 'HOST' not in col:
                row = '\t'.join([row.strip('\r\n'), _urlparse(row_dict['REFERER'])])
            if 'code' not in col:
                row = row.stri('\r\n') + '\t'
            # filter_rows 格式：[[],[]]
            filter_rows.append(row.split('\t'))

    if 'HOST' not in col:
        col.append('HOST')
    if 'code' not in col:
        col.append('code')
    if logger:
        logger.info("读取数据{}，剩余{}条".format(file_path, total_count))

    if logger:
        logger.info("过滤掉不包含REFERER字段、USERNAME字段的数据后，剩余{}条".format(len(filter_rows)))

    host_index = col.index('HOST')
    capture_time_index = col.index('CAPTURE_TIME')
    sort_rows = sorted(filter_rows, key=itemgetter(host_index, capture_time_index), reverse=True)
    # 取最近时间的4条
    host_type_count = {}
    recent_hosts = []
    for row in sort_rows:
        if row[host_index] not in host_type_count.keys():
            recent_hosts.append(row)
            host_type_count[row[host_index]] = 1
        else:
            if host_type_count[row[host_index]] < 4:
                recent_hosts.append(row)
                host_type_count[row[host_index]] += 1
            else:
                continue

    if logger:
        logger.info("每个HOST下账号密码只取近期前4条，剩余{}条".format(len(recent_hosts)))
    refer_set = set()
    filter_by_fields = pre_filter(recent_hosts, col, logger)

    web_info_col_names = ['HOST', 'dst_ip', 'src_ip', 'REFERER', 'form_data', 'AUTH_ACCOUNT', 'CAPTURE_TIME',
                          'USERNAME', 'PASSWORD', 'code']
    with open('./tmp/{}_{}.tsv'.format(id, file_from), 'w', encoding='utf8') as f:
        f.write('\t'.join(web_info_col_names) + '\n')
    with open('./tmp/{}_{}.tsv'.format(id, file_from), 'a+', encoding='utf8') as f:
        for row in filter_by_fields:
            refer_set.add(row[col.index('REFERER')])
            web_info_field = []
            for field in web_info_col_names:
                web_info_field.append(row[col.index(field)])
            f.write('\t'.join(web_info_field) + '\n')
    if logger:
        logger.info("生成网站基本信息文件，路径：{},共{}条".format('./tmp/{}_{}.tsv'.format(id, file_from), len(filter_by_fields)))

    with open('./tmp/{}_referers.tsv'.format(id), 'w', encoding='utf8') as f:
        f.writelines('\n'.join(refer_set))
    if logger:
        logger.info("生成referers文件，路径：{},共{}条".format('./tmp/{}_referers.tsv'.format(id), len(refer_set)))


def null_file_assert(file_path):
    size = os.path.getsize(file_path)
    assert size != 0, "{}文件为空".format(file_path)


def concat(res_file_path: str, input_file_path: str, output_file_path: str,
           LOG: Logger = None,
           is_filter_by_word: bool = False,
           is_filter_by_input: bool = False,
           is_filter_by_country: bool = False,
           ) -> None:
    """
    使用爬虫结果文件关联上原始文件，并将结果保存在输出路径中
    过滤逻辑：
        1. 正向关键词逻辑： 用于防止删除了正确的数据。有的页面其可能没有捕获到input框，但是使用浏览器访问的时候是有input框的，这个时候如果关键词带后台，则保留
        2. 反向关键词逻辑： 用于删除数据。凡是命中返现关键词的一律删除
        3. input框逻辑： 用于筛选出包含密码框并且input框数量 > 1（后改为 > 0，没有捕获到密码的网站可以考虑填12345等默认密码） 的网站。如网站爬虫没有捕获input框但是包含正向关键词则保留数据
    :param res_file_path:        爬虫爬取的结果文件
    :param input_file_path:      全文输出文件
    :param output_file_path:     输出文件路径
    :param LOG:                  日志
    :param is_filter_by_word:    是否使用关键词过滤
    :param is_filter_by_input:   是否使用input框过滤
    :param is_filter_by_country: 是否使用`是否为中文网站`这一标签来过滤
    :return:
    """
    # res_file
    if LOG is None:
        LOG, _ = Logger("test").getlog()
    mark_file = word_filter(res_file_path)
    assert len(mark_file) != 0, "文件: {} 过滤后为空！".format(mark_file)
    LOG.info("爬取结果文件共{}条".format(len(mark_file)))
    null_file_assert(input_file_path)
    # 打标结果添加到input_file文件，合并规则，与input_file文件refer相同
    merge_result = set()
    total_count = 0
    field_names = []
    with open(input_file_path, 'r', encoding="utf-8") as f:
        for line in f:
            # 保存字段名称
            total_count += 1
            if total_count == 1:
                field_names.append(line.strip('\r\n'))
                continue
            for key in mark_file.keys():
                # 过滤掉爬取结果为空的行
                if key in line and len(mark_file[key]) != 0:
                    merge_result.add(line.strip('\r\n') + '\t' + '\t'.join(mark_file[key]))
                    break
    field_names.append('referer\ticp\ttitle\tkeywords\tdescription\tnames\ticp_label\tneg_words_label'
                       '\tpos_words_label\tinput_num')
    LOG.info("输入文件共{}条".format(total_count))
    merge_count = len(merge_result)
    assert merge_count != 0, "爬取结果与输入文件合并后为空，请检查 {} 与 {}".format(res_file_path, input_file_path)
    LOG.info("过滤掉爬取结果为空后去重数据{}条".format(total_count - merge_count))
    _, file_name = get_file_name(output_file_path)
    print(file_name, output_file_path)

    # 写入字段名称
    with open("./datas/_{}.csv".format(file_name), 'w', encoding="utf-8") as f:
        f.write('\t'.join(field_names) + '\n')

    # 将集合内容写入文件
    with open("./datas/_{}.csv".format(file_name), 'a+', encoding="utf-8") as f:
        for row in merge_result:
            f.write(row + '\n')

    neg_words_filter_result = []
    if is_filter_by_word:
        field_names.append("neg_words_label")
        for row in merge_result:
            if len(row.split('\t')[-3]) == 0:
                neg_words_filter_result.append(row)
        neg_words_filter_count = len(neg_words_filter_result)
        LOG.info("反向关键词过滤掉{}条数据".format(merge_count - neg_words_filter_count))
    else:
        neg_words_filter_result = merge_result

    pos_words_filter_result = []
    if is_filter_by_input:
        field_names.append("pos_words_label")
        for row in neg_words_filter_result:
            if len(row.split('\t')[-2]) != 0 or int(row.split('\t')[-1]) > 0:
                pos_words_filter_result.append(row)
        pos_words_filter_count = len(pos_words_filter_result)
        LOG.info("通过正向关键词与网站输入框数量过滤掉{}条数据".format(neg_words_filter_count - pos_words_filter_count))
    else:
        pos_words_filter_result = neg_words_filter_result

    foreign_web_filter_result = []
    chinese_web_filter_result = []
    if is_filter_by_country:
        field_list = '\t'.join(field_names).split('\t')
        field_names.append("chinese_web")
        # 中文正则，使用title+keywords+description，剔除全外文网站
        # 该方法存在一种特殊情况，即当title，keywords、description都为空时，此时会判定为外文网站
        for row in pos_words_filter_result:
            row_fields = dict(zip(field_list, row.split('\t')))
            chinese_web = is_chinese_web(row_fields)
            new_row = row + '\t' + str(chinese_web)
            if chinese_web:
                chinese_web_filter_result.append(new_row)
            else:
                foreign_web_filter_result.append(new_row)
        # 写入字段名称
        with open("./tmp/{}_foreign".format(file_name), 'w', encoding="utf-8") as f:
            f.write('\t'.join(field_names) + '\n')

        with open("./tmp/{}_foreign".format(file_name), 'a+', encoding="utf-8") as f:
            for row in foreign_web_filter_result:
                f.write(row + '\n')
        LOG.info("外文网站过滤掉{}条数据".format(len(pos_words_filter_result) - len(foreign_web_filter_result)))
    else:
        chinese_web_filter_result = pos_words_filter_result
    # 写入字段名称
    field_names.append('label\tremarks\tcapturetime\tlocation')
    with open(output_file_path, 'w', encoding="utf-8") as f:
        f.write('\t'.join(field_names) + '\n')
    with open(output_file_path, 'a+', encoding="utf-8") as f:
        for row in chinese_web_filter_result:
            # 0918 新增四列数据,便于取证打标 ['label', 'remarks', 'capturetime', 'location']
            f.write(row + '\t' * 4 + '\n')
    LOG.info("最终{}条数据".format(len(chinese_web_filter_result)))

    # host去重
    hosts_set = set()
    for row_content in chinese_web_filter_result:
        hosts_set.add(row_content.split('\t')[0])
    with open("./datas/{}_hosts.csv".format(file_name), 'w', encoding="utf-8") as f:
        for host in hosts_set:
            f.write(host + '\n')


def purify(line: str) -> Dict[str, str]:
    """
    过滤
    1. 首先取得form_data字段，不要_QUERY_MATCHTERMS
    2. 凡是form_data字段里带"?", "&"的基本都是网页url，统统不要
    3. value太长的，基本都是md5后的值，也不要
    :param line:
    :return:
    """
    sep = ';'
    data = line.split(';_QUERY_MATCHTERMS')[0].strip("...") \
        .replace('; ', sep).replace('...', sep).replace('##', sep)
    eles = data.split(';')
    eles = filter(lambda x: ('?' not in x) and ('&' not in x) and ('=' in x), eles)
    return dict([tuple(ele.split('=', 1)) for ele in eles])


def is_valid_web(text: str, logger=None) -> bool:
    """
    验证页面有效性
    :param text: 网页内容，
    :param logger: logger
    :return: bool
    """
    pattern = re.compile(
        r"(无法访问|无法找到该页|该网页无法正常运作|找不到文件|无法加载控制器|页面错误|未知错误|页面未找到|请联系接入商|域名已过期|"
        r"没有找到站点|Not Found|404错误|<html><head></head><body></body></html>|访问受限|尚未绑定|page note found|"
        r"HTTP 错误)")
    ret = pattern.findall(text)
    if ret:
        if logger:
            logger.debug("无效网址，Detail：{}".format(ret[0]))
        return False
    else:
        return True


def get_file_name(file_path: str, with_suffix=False) -> Tuple[str, str]:
    """
    file_path like E:\ProgramData\Anaconda3\lib\ntpath.py
    or E://ProgramData//Anaconda3//lib//ntpath.py
    => E:/ProgramData/Anaconda3/lib/ntpath.py
    """
    sep = "/"
    file_path = file_path.replace("\\", sep).replace("//", sep)
    right_sep_index = file_path.rfind(sep)
    dir_path = file_path[:right_sep_index]
    file_name = file_path[right_sep_index + 1:]
    if not with_suffix:
        file_name = file_name[:file_name.rfind(".")]
    return dir_path, file_name


if __name__ == '__main__':
    parser = ArgumentParser()
    parser.add_argument("-p", "--path", action="store", dest="path", help="Path of input file.")
    # 过滤条件
    parser.add_argument("-w", "--word_filter", action="store_true", dest="is_filter_by_word",
                        help="Filter web info via word label.")
    parser.add_argument("-i", "--input_filter", action="store_true", dest="is_filter_by_input",
                        help="Filter web info via input label.")
    args = parser.parse_args()
    path = args.path
    is_filter_by_word = args.is_filter_by_word
    is_filter_by_input = args.is_filter_by_input

    # concat(path, r'D:\softanzhuanglxf\4quzheng\IAO_Web_validation\2\tmp\202103191128_wu.tsv',
    #        r"D:\softanzhuanglxf\4quzheng\IAO_Web_validation\2\datas\南充5113002_ret.csv",
    #        is_filter_by_word=is_filter_by_word,
    #        is_filter_by_input=is_filter_by_input,
    #        is_filter_by_country=True)
    uni_format(r"D:\softanzhuanglxf\4quzheng\IAO_Web_validation\2\datas\南充511300.txt", 'wu','89878j')
