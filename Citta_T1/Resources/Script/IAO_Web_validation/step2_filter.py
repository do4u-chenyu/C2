import requests
from bs4 import BeautifulSoup as BS
import re
from collections import namedtuple
from queue import Queue
from threading import Thread
import sys
from requests.packages.urllib3.exceptions import InsecureRequestWarning
from requests.exceptions import ReadTimeout
from requests.models import Response, Request
from util import Logger, pad_url, uni_format, concat, is_valid_web, get_file_name
import time
from os.path import join, splitext, abspath
from argparse import ArgumentParser
import json
import traceback
from functools import reduce
import os
from global_method import default_output_path

global LOG
global RQ

# 禁用安全请求警告
requests.packages.urllib3.disable_warnings(InsecureRequestWarning)

"""
准则： ~~绝对不删除原有数据，只是根据外网结果对其进行打标~~
      现在concat函数里加入过滤选项，如果需要过滤则使用过滤，不需要（数据量很少的情况下）则不过滤

功能如下：
1. 在主页页面搜索ICP备案号（不是主页怎么办？没有主页怎么办？）
2. 保存title、keywords、description
3. 对于爬取失败的url，首先是设置重查次数，然后是记录status_code  

整体结构如下：
1. Scheduler 读取urls文件，对其进行预处理，将url信息（index : url）放入url队列中。
2. Crawler 作为工作线程从url队列获取url，并完成爬取任务，将网站信息web_info放入结果队列中。
3. Saver 从结果队列中获取结果，并保存在本地。
4. Logger 用来记录日志

输入输出文件:
1. 一个全文导出文件，如`./datas/result_admin_ln06.csv`
2. 一个输出文件，如`./datas/_result_admin_ln06.csv`
期间会产生一些中间文件如：
    1. `./tmps/file_1.tsv`: 仅去HOST, REFERER两列的数据
    2. `./tmps/wu.tsv`: 对输入文件进行一些预处理之后的文件，用于与爬虫结果文件关联
    3. `./Logs/{time}.log`： 日志文件
    4. `./Logs/{time}.ret`： 爬虫结果文件
"""

# DONE 中断，失败队列，查找主页 => 中断没必要，现在可以直接运行完；失败队列现在也不做了，失败就直接扔了；查找主页也不做，这个是截屏的任务。


WebInfo = namedtuple("WebInfo", ["url", "icp", "title", "keywords", "description", "name"])


class Scheduler:
    def __init__(self, file_path, encoding="utf8", file_type="csv"):
        """

        :param file_path: 爬虫输入文件。HOST\tREFERER，两个字段
        :param encoding: 文件编码。默认utf8
        :param file_type: 文件类型。csv，tsv
        """
        self.file_type = file_type
        self.file_path = file_path
        self.file_encoding = encoding
        self.url_queue = Queue()
        self.ret_queue = Queue()
        self.seen = set()
        self.urls = set()
        self.spawn_urls()
        self.failed_urls = set()
        self.total = 0
        self.workers = []
        self.saver = Saver(join("./Logs", RQ + "_ret.txt"), self.workers, self.ret_queue)

    def spawn_urls(self):
        if self.file_type == "csv":
            self.get_url_from_csv(sep=",")
        elif self.file_type == "tsv":
            self.get_url_from_csv(sep="\t")
        else:
            pass

    def get_url_from_csv(self, sep):
        """
        读取源文件，向队列中推送任务
        Update 0805
        队列中每个元素都是一个Tuple[host: str, referer: str]
        :param sep:
        :return:
        """
        url_set = set()
        with open(self.file_path, encoding=self.file_encoding, errors="ignore") as f:
            for line in f:
                url_set.add(line.strip('\r\n').split(sep)[0])
        for url in url_set:
            self.urls.add(url)
        self.total = len(self.urls)
        LOG.info("URL总数：{}".format(self.total))

    def scheduling(self):
        LOG.info("启动写入线程")
        self.saver.daemon = True
        self.saver.start()

        LOG.info("启动爬虫线程")
        for x in range(THREAD_NUMS):
            worker = Crawler(self.url_queue, self.saver.ret_queue)
            worker.daemon = True
            worker.start()
            self.workers.append(worker)
        self.saver.workers = self.workers

        LOG.info("URL预处理")
        for url in self.urls:
            self.url_queue.put(
                url
            )

        LOG.info("启动守护线程")
        daemon = Daemon(self.workers, self.saver, self.url_queue, self.ret_queue, len(self.urls))
        daemon.daemon = True
        daemon.start()

        self.url_queue.join()
        self.ret_queue.join()


class Crawler(Thread):
    """爬虫
    职责为验证网站是否`有效`
    1. 能否打开
    2. ~~能打开的话是否是有效网页，而不是类似于“域名无法访问”， “域名没有绑定”之类的提示页面~~
    2. 找input输入框，并提取其name
    """

    def __init__(self, url_queue, ret_queue):
        """
        :param url_queue: url队列，(idx, url)
        :param ret_queue:
        """
        Thread.__init__(self)
        self.headers = {
            "User-Agent": "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_4) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.81 Safari/537.36"}
        self.pattern = {
            "icp": re.compile("ICP.\d+号"),
            "charset": re.compile('charset=" * ([\w\d\-]+)"*'),
        }
        self.purify = lambda s: re.sub("\r|\n|\t", ",", s)
        self.url_queue = url_queue
        self.ret_queue = ret_queue
        self.get_num = 0
        self.done_num = 0
        self.failed_url = set()
        self._exit_code = 0
        self._exit_info = ''
        self._exception = ''
        self._exc_traceback = ''

    def run(self):
        while True:
            url = self.url_queue.get()
            LOG.debug("{}: URL: {}".format(self.name, url))
            self.get_num += 1
            status_code = ""
            detail = "正常"
            try:
                req = self._request(url)
                if req and is_valid_web(req.text):
                    web_info = self._extract(req, url)
                    self.ret_queue.put(
                        web_info
                    )
            except ReadTimeout:
                status_code = -1
                detail = "超时"
                self.failed_url.add((url, status_code, detail))
            except UnicodeDecodeError:
                detail = "网页转码异常"
                self.failed_url.add((url, status_code, detail))
            except Exception as e:
                detail = str(e)
                self._exit_code = 1
                self._exception = e
                self._exc_traceback = ''.join(traceback.format_exception(*sys.exc_info()))
                LOG.debug("[Worker]Worker: {} Traceback: {}.".format(self.name, self._exc_traceback))
                self.failed_url.add((url, status_code, detail))
            finally:
                self.url_queue.task_done()

            LOG.debug("url : {}, 获取数 : {} , 完成数 : {} , detail : {}".
                      format(self.name, url, self.get_num, self.done_num, detail))

    def _request(self, url) -> Response:
        # DONE 处理状态码，超时、重查次数
        """
        :param url:
        :return req:
        """
        retries = 0
        req = None
        while retries < RETRIES:
            if not (url.startswith("http://") or url.startswith("https://")):
                url = "http://" + url
            req = requests.get(url=url,
                               headers=self.headers,
                               verify=False,
                               timeout=TIMEOUT)
            if req.status_code == 200:
                return req
            retries += 1
            LOG.debug("url:{} retries:{} status_code:{}".format(url, retries, req.status_code))
        return req

    def _extract(self, req: Response, url: str) -> WebInfo:
        """
        页面信息提取，提取ICP号与title等，对于中文页面需要转码
        :param url:
        :param req:
        :return: WebInfo
        """
        try:
            html_text = req.content.decode("utf-8")
        except UnicodeDecodeError:
            html_text = req.content.decode("gbk")
        soup = BS(html_text, "lxml")
        try:
            icp = self.purify(self.pattern["icp"].findall(html_text)[0])
        except:
            icp = ""
        try:
            title = self.purify(soup.head.title.text.strip())
        except:
            title = ""
        try:
            keywords = self.purify(soup.head.find("meta", attrs={"name": "keywords"})["content"].strip())
        except:
            keywords = ""
        try:
            description = self.purify(soup.head.find("meta", attrs={"name": "description"})["content"].strip())
        except:
            description = ""
        # names = Tuple[是否有密码框: int, names: List[str]]
        # 考虑input框输入的时候必须含有密码框才有意义
        # 必须提取text框，其他框不要
        try:
            # 密码框只管一个
            pwd = soup.find("input", {"type": "password"})
            text_list = soup.find_all("input", {"type": "text"})
            has_pwds = 1 if pwd else 0
            eles = text_list
            if has_pwds:
                eles.append(pwd)
            names = list(filter(lambda x: x != "", [ele.attrs.get("name", "") for ele in eles]))
            names = [has_pwds, names]
        except:
            names = [0, []]

        names = json.dumps(names)
        web_info = WebInfo(url, icp, title, keywords, description, names)
        self.done_num += 1
        return web_info


class Daemon(Thread):
    """守护线程
    负责报告整个程序的运行情况：
        1. 爬虫线程存活情况
        2. 爬取进度
    """

    def __init__(self, workers, saver, all_queue, val_queue, total_num):
        """

        :param workers:
        :param all_queue:
        :param val_queue:
        :param total_num:
        """
        Thread.__init__(self)
        self.workers = workers
        self.saver = saver
        self.all_queue = all_queue
        self.val_queue = val_queue
        self.total_num = total_num

    def run(self) -> None:
        while True:
            time.sleep(5)
            LOG.info("=" * 20)
            LOG.info(
                "[Daemon]Crawler Status: {}/{}".format(sum([1 for w in self.workers]), len(self.workers)))
            LOG.info("[Daemon]ALL_QUEUE: {}/{}".format(self.all_queue.qsize(), self.total_num))
            LOG.info("[Daemon]VAL_QUEUE: {}".format(self.val_queue.qsize()))
            LOG.info("[Daemon]Saver: {}/{}".format(self.saver.done_sum, self.total_num))
            LOG.info("=" * 20)


class Saver(Thread):
    """写入线程

    """

    def __init__(self, save_path, workers, ret_queue):
        Thread.__init__(self)
        self.ret_queue = ret_queue
        self.workers = workers
        self.rets = []
        self.writer = open(save_path, "w+", encoding="utf-8")
        self.done_sum = 0
        self.last_time = time.time()

    def _save(self, web_info):
        info = "\t".join(web_info._asdict().values()) + "\n"
        self.writer.write(info)
        self.writer.flush()
        self.done_sum += 1

    def run(self):
        while True:
            tmp = self.ret_queue.get()
            if self.done_sum % 100 == 0:
                LOG.info("[Saver]Saver done total : {} runtime : {:.2f}s speed: {:.2f} url/s".format(
                    self.done_sum,
                    time.time() - self.last_time,
                    self.done_sum / (time.time() - self.last_time)
                )
                )
            web_info = tmp
            self._save(web_info)
            self.ret_queue.task_done()


def main(file_path, THREAD_NUMS, TIMEOUT, RETRIES, is_filter_by_word, is_filter_by_input, is_filter_by_country,output_path):
    global LOG
    global RQ
    LOG, RQ = Logger("CrawlerLog", is_debug=is_debug).getlog()
    LOG.info("""
                启动脚本
                输入文件名： {}
                线程数：{}
                超时等待：{}S
                重试次数：{}
                ID：{}
            """.format(file_path, THREAD_NUMS, TIMEOUT, RETRIES, RQ))
    # 创建需要的文件夹
    os.makedirs("./tmp", exist_ok=True)
    os.makedirs("./Logs", exist_ok=True)
    os.makedirs("./datas", exist_ok=True)

    # 文件类型 可选 "tang" or "wu"
    if file_type == "fulltext":
        file_from = "wu"

        dir_path, file_name = get_file_name(file_path)
        # 输入文件
        tag = time.strftime("%Y%m%d%H%M", time.localtime(time.time()))
        uni_format(file_path, file_from=file_from, id=RQ, logger=LOG)
        # 调度器
        sch = Scheduler("./tmp/{}_referers.tsv".format(RQ), "utf8", "tsv")
        sch.scheduling()
        ret_file_path = "./Logs/{}_ret.txt".format(tag)
        LOG.info("开始合并文件")
        result_path = join(output_path, "{}_ret.csv".format(file_name))
        concat(ret_file_path, "./tmp/{}_{}.tsv".format(RQ, file_from), result_path,
               LOG,
               is_filter_by_word, is_filter_by_input, is_filter_by_country)
    elif file_type == "urls":
        sch = Scheduler(file_path, "utf8", "tsv")
        sch.scheduling()
    else:
        LOG.error("Wrong file type: {}, Please choose 'fulltext or 'urls'".format(file_type))
        import sys
        sys.exit(-1)


if __name__ == "__main__":
    parser = ArgumentParser()

    # 输入文件参数
    parser.add_argument("input_path", help="the path of input file ")
    # 输出数据类型
    parser.add_argument("output_path", nargs='*', default="default_path", type=str, help="path of result")
    parser.add_argument("-d", "--debug", action="store_true", dest="is_debug", help="Debug mode.")
    # 输入数据类型
    parser.add_argument("-f", "--file_type", action="store", dest="file_type",
                        default="fulltext", type=str, choices=["fulltext", "urls"],
                        help="Data type, fulltext or urls")
    # 爬虫参数设置
    parser.add_argument("-t", "--thread_nums", action="store", dest="THREAD_NUMS", default=200, type=int,
                        help="Number of thread.")
    parser.add_argument("-m", "--timeout", action="store", dest="TIMEOUT", default=40, type=int,
                        help="Time out")
    parser.add_argument("-r", "--retries", action="store", dest="RETRIES", default=2, type=int,
                        help="Retry times.")
    # 过滤条件
    parser.add_argument("-w", "--word_filter", action="store_true", dest="is_filter_by_word",
                        help="Filter web info via `words_label`.")
    parser.add_argument("-i", "--input_filter", action="store_true", dest="is_filter_by_input",
                        help="Filter web info via `input_num` label.")
    parser.add_argument("-c", "--country_filter", action="store_true", dest="is_filter_by_country",
                        help="Filter web info via `is_chinese` label.")

    args = parser.parse_args()
    file_path = args.input_path
    # 设置默认输出路径
    output_path = args.output_path
    if output_path == 'default_path':
        output_path = abspath(join(file_path, "..", default_output_path(2)))
    os.makedirs(output_path, exist_ok=True)
    file_type = args.file_type
    is_debug = args.is_debug
    THREAD_NUMS = args.THREAD_NUMS
    TIMEOUT = args.TIMEOUT
    RETRIES = args.RETRIES
    is_filter_by_word = args.is_filter_by_word
    is_filter_by_input = args.is_filter_by_input
    is_filter_by_country = args.is_filter_by_country
    for file in os.listdir(file_path):
        if file.endswith("txt") and "queryResult_db" in file:
            file = os.path.join(file_path, file)
            main(file, THREAD_NUMS, TIMEOUT, RETRIES, is_filter_by_word, is_filter_by_input, is_filter_by_country,output_path)
