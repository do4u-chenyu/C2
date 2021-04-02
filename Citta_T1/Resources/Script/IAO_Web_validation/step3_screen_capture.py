from argparse import ArgumentParser
from os import path,makedirs
import os.path
from global_method import default_output_path
from util import Logger
import time
from subprocess import Popen, PIPE
import datetime


def pad_url(url):
    """
    1.url补全http
    2.url填充可能域名
    :param url:
    :return:
    """
    if ("http://" in url) or ("https://" in url):
        return url
    return "http://" + url


def modify_host(host):
    return host.replace(":", "冒号")


def screenshot(file_path, output_path):
    urls = set()
    with open(file_path, 'r', encoding='utf8') as f:
        for line in f:
            if len(line.split('\t')) != 2:
                continue
            urls.add(line.strip().split('\t')[1])
    for host in urls:
        version_value = 9
        while version_value > 6:
            if execute_screenshot(output_path, host, "IE" + str(version_value)):
                break
            version_value -= 1


def merge_hosts(host_path):
    host_list = [x for x in os.listdir(host_path) if x.endswith('hosts.csv')]
    total_host = path.join(host_path, "total_hosts.csv")
    total_file = open(total_host, 'w', encoding='utf8')
    for file in host_list:
        with open(path.join(host_path, file), 'r', encoding='utf8') as f:
            for line in f:
                total_file.write("{}\t{}".format(file, line))
    total_file.close()
    return total_host


def execute_screenshot(output_path, host, ie_version):
    try:
        # driver_path = path.join(path.abspath(path.dirname(__file__)), "phantomjs.exe")
        # js_path = path.join(path.abspath(path.dirname(__file__)), "screenshot.js")

        driver_path = path.join(path.abspath(path.dirname(__file__)), "TrifleJS.exe")
        js_path = path.join(path.abspath(path.dirname(__file__)), "screenshot.js")
        png_file = path.join(output_path, modify_host(host) + ".png")
        cmd = '{0} {1} {2} {3} --emulate={4}'.format(driver_path, js_path, pad_url(host), png_file, ie_version)
        print(cmd)
        start = datetime.datetime.now()
        process = Popen(cmd, shell=True, stdout=PIPE, stderr=PIPE)
        timeout = 12
        while process.poll() is None:
            time.sleep(0.2)
            now = datetime.datetime.now()
            if (now - start).seconds > timeout:
                process.kill()
                os.system('taskkill /f /im TrifleJS.exe')
                return False
        return True
    except:
        return False


if __name__ == '__main__':
    parser = ArgumentParser()
    parser.add_argument('input')
    parser.add_argument('output', nargs='*', default='default_path')
    args = parser.parse_args()
    input_path = args.input
    output_path = args.output
    LOG, RQ = Logger("ScreenshotLog", False).getlog()
    # ----创建输出路径及失败存储文件
    if args.output == 'default_path':
        current_time = time.strftime('%Y%m%d%H%M%S', time.localtime())
        output_path = path.join(path.dirname(input_path), default_output_path(3), current_time)

    makedirs(output_path, exist_ok=True)
    if not output_path.endswith("\\"):
        output_path += "\\"
    # -----创建输出路径
    try:
        total_host = merge_hosts(input_path)
        screenshot(total_host, output_path)

    except:
        LOG.info("Quit.")
    finally:
        LOG.info('Done.')
