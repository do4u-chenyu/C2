# -*- encoding: utf-8 -*-
# @File : AutoUpdate.py
# @Author : 安替
# @Time : 2022/7/28 15:43
# @Software : PyCharm
# Python版本：3.6.3

from json import loads
import paramiko
import datetime
import xml.etree.ElementTree as ET
import logging
import os
import argparse
logging.basicConfig(level=logging.INFO, format='%(asctime)s - %(name)s - %(levelname)s - %(message)s')
logger = logging.getLogger('AutoUpdate.py')

"""

 1.文件更新
     C2版本号更新
         gen_beauty_inner_package.iss 
         gen_beauty_outer_package.iss  
         gen_beauty_package.iss
         AssemblyInfo.cs
         update.xml
     战术手册版本号更新
         update.xml
         
 2.上传文件到Linux
     C2外网单机版
     C2内网单机版
     C2内网服务版
     战术手册
     update.xml
     
 3.使用注意事项
     3.1 文件更新读取的路径为绝对路径，如果文件路径有改动，请在config.json文件及时更新对应路径
     3.2 美化后的 外网/内网C2 单机版exe 放在原来位置(D:\work\C2\C2打包程序\output)不要动，会读取路径，上传到Linux服务器对应目录(/data/C2/C2_exe)
     3.3 原则上默认当天打包，当天上传，上传日期与打包日期一致，如果不一致，则无法上传
     3.4 内网服务版的路径存在 D:\work\C2\C2打包程序\output
 
 4.更新选项
     4.1 更新C2外网单机版和内网单机版
     4.2 更新战术手册
     4.3 更新C2内网服务版
     4.4 多项更新采用 选择对应命令参数(暂时没写)
 5. 使用说明
     5.1 更新C2单机版命令：  python AutoUpdate.py -o 1
     5.2 更新服务版命令：    python AutoUpdate.py -o 2
     5.3 更新战术手册命令：  python AutoUpdate.py -o 3
"""

def alter_iss(file, old_str, new_str):
    """
    替换文件中的字符串
    :param file:文件名
    :param old_str:旧字符串
    :param new_str:新字符串
    :return:
    """
    file_data = ""
    with open(file, "r", errors='ignore') as f:
        for line in f:
            if old_str in line:
                line = line.replace(old_str, new_str)
            file_data += line
    with open(file, "w") as f:
        f.write(file_data)


def alter_cs(file, old_str, new_str):
    """
    替换文件中的字符串
    :param file:文件名
    :param old_str:旧字符串
    :param new_str:新字符串
    :return:
    """
    file_data = ""
    with open(file, "r", encoding='utf-8') as f:  #errors='ignore'
        for line in f:
            if old_str in line:
                line = line.replace(old_str, new_str)
            file_data += line
    with open(file, "w", encoding='utf-8') as f:
        f.write(file_data)


def alter_xml(xml_path,option):
    tree = ET.parse(xml_path)
    root = tree.getroot()
    for node in root.findall('Soft'):
        if option == 1: #单机版
            node.find('Verson').text = config['new_version']
            node.find('DownLoadC2Outer').text = "https://113.31.114.239:53376/C2/单兵作战(外网版)_{}.exe".format(datetime.datetime.now().strftime("%Y%m%d%H%M%S")[4:8])
            node.find('DownLoadC2Inner').text = "https://113.31.114.239:53376/C2/单兵作战(内网版)_{}.exe".format(datetime.datetime.now().strftime("%Y%m%d%H%M%S")[4:8])
        elif option == 2: #服务版
            node.find('Verson').text = config['new_version']
            node.find('DownLoadC2Service').text = "https://113.31.114.239:53376/C2/单兵作战(内网服务版)_{}.exe".format(datetime.datetime.now().strftime("%Y%m%d%H%M%S")[4:8])
        elif option == 3: #战术手册
            node.find('ManulVersion').text = datetime.datetime.now().strftime("%Y%m%d%H%M%S")[0:8]
            node.find('DownLoadC2F').text = "https://113.31.114.239:53376/C2/战术手册_{}.exe".format(datetime.datetime.now().strftime("%Y%m%d%H%M%S")[4:8])
    tree.write(config['update_xml_path'], encoding='utf-8')


def upload_copy(filespath, root, local_dir, remote_dir, sftp):
    logger.info('开始上传文件:{}'.format(filespath))
    local_file = os.path.join(root, filespath)
    a = local_file.replace(local_dir, '').replace('\\', '/').lstrip('/')
    remote_file = os.path.join(remote_dir, a).replace("\\", "/")
    sftp.put(local_file, remote_file)

def upload(local_dir, remote_dir, option):
    """
    Parameters
    ----------
    local_dir : C2本地上传文件目录
    remote_dir : 上传目标地址
    Returns
    -------
    """
    try:
        t = paramiko.Transport((config["hostname"], config['port']))
        t.connect(username=config["username"], password=config["password"])
        sftp = paramiko.SFTPClient.from_transport(t)
        logger.info('开始上传文件:{}'.format(datetime.datetime.now()))
        for root, dirs, files in os.walk(local_dir):
            for filespath in files:
                if option == 1:
                    if ("单兵作战(内网版)_"+ datetime.datetime.now().strftime("%Y%m%d%H%M%S")[4:8] in filespath or "单兵作战(外网版)_"+ datetime.datetime.now().strftime("%Y%m%d%H%M%S")[4:8] in filespath or 'update.xml' in filespath):
                        upload_copy(filespath, root, local_dir, remote_dir, sftp)
                if option == 2:
                    if("单兵作战(内网服务版)_" + datetime.datetime.now().strftime("%Y%m%d%H%M%S")[4:8] in filespath or 'update.xml' in filespath):
                        upload_copy(filespath, root, local_dir, remote_dir, sftp)
                if option == 3:
                    if ("战术手册_" + datetime.datetime.now().strftime("%Y%m%d%H%M%S")[4:8] in filespath or 'update.xml' in filespath):
                        upload_copy(filespath, root, local_dir, remote_dir, sftp)
        logger.info('文件上传完成:{}'.format(datetime.datetime.now()))
        t.close()
    except Exception as e:
        print(e)


def main(args):

    global config
    with open('./config.json', 'r', encoding='utf_8_sig') as f:
        config = loads(f.read())

    if args.option == 1:
        """
            Returns
            -------
            更新C2单机版外网和内网
        """
        path_list = os.listdir(config["gen_beauty_path"])
        for filename in path_list:
            if os.path.splitext(filename)[1] == ".iss" and "gen_beauty" in os.path.splitext(filename)[0]:
                alter_iss(config["gen_beauty_path"] + "\\" + filename, config["old_version"], config["new_version"])
                logger.info("{}|C2版本号由{}更新为{}".format(filename, config["old_version"], config["new_version"]))

        alter_cs(config["AssemblyInfo_path"], config["old_version"], config["new_version"])
        logger.info("{}|C2版本号由{}更新为{}".format("AssemblyInfo.cs", config["old_version"], config["new_version"]))

        alter_xml(config['update_xml_path'], 1)
        logger.info("{}|C2版本号由{}更新为{}".format("update.xml", config['old_version'], config['new_version']))

        upload(config['local_C2_output'], config['remote_upload_dir'], 1)
        upload("./", config['remote_upload_dir'], 1)

    if args.option == 2:
        """
            Returns
            -------
            更新C2内网服务版
        """
        alter_xml(config['update_xml_path'], 2)
        logger.info("{}|C2版本号由{}更新为{}".format("update.xml", config['old_version'], config['new_version']))

        upload(config['local_C2_output'], config['remote_upload_dir'], 2)
        upload("./", config['remote_upload_dir'], 2)

    if args.option == 3:
        """
            Returns
            -------
            更新战术手册
        """
        alter_xml(config['update_xml_path'], 3)
        logger.info("{}|战术手册版本号(ManulVersion)更新为:{}".format("update.xml", datetime.datetime.now().strftime("%Y%m%d%H%M%S")[0:8]))
        upload(config['local_manau_path'], config['remote_upload_dir'], 3)
        upload("./", config['remote_upload_dir'], 3)

if __name__ == "__main__":
    my_arg = argparse.ArgumentParser('My argument parser')
    my_arg.add_argument('--option', '-o', default=1, type=int, help='选择更新哪一块代码')
    args = my_arg.parse_args()
    main(args)
