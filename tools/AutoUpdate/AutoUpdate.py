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
logging.basicConfig(level=logging.INFO, format='%(asctime)s - %(name)s - %(levelname)s - %(message)s')
logger = logging.getLogger('AutoUpdate.py')
"""
 1.文件更新
     版本号更新
         gen_beauty_inner_package.iss 
         gen_beauty_outer_package.iss  
         gen_beauty_package.iss
         AssemblyInfo.cs
         update.xml
             C2和战术手册版本号
 2.上传文件到Linux
     C2外网单机版
     C2内网单机版
     C2内网服务版
     战术手册
     update.xml
 3.使用注意事项
     
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


def alter_xml(xml_path):
    tree = ET.parse(xml_path)
    root = tree.getroot()
    for node in root.findall('Soft'):
        node.find('Verson').text = config['new_version']
        node.find('ManulVersion').text = datetime.datetime.now().strftime("%Y%m%d%H%M%S")[0:8]
        node.find('DownLoadC2Outer').text = "https://113.31.114.239:53376/C2/单兵作战(外网版)_{}.exe".format(datetime.datetime.now().strftime("%Y%m%d%H%M%S")[4:8])
        node.find('DownLoadC2Inner').text = "https://113.31.114.239:53376/C2/单兵作战(内网版)_{}.exe".format(datetime.datetime.now().strftime("%Y%m%d%H%M%S")[4:8])
        node.find('DownLoadC2F').text = "https://113.31.114.239:53376/C2/战术手册_{}.exe".format(datetime.datetime.now().strftime("%Y%m%d%H%M%S")[4:8])
        node.find('DownLoadC2Service').text = "https://113.31.114.239:53376/C2/单兵作战(内网服务版)_{}.exe".format(datetime.datetime.now().strftime("%Y%m%d%H%M%S")[4:8])
    tree.write(config['update_xml_path'], encoding='utf-8')


def upload(local_dir,remote_dir):
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
        logger.info('文件开始上传:{}'.format(datetime.datetime.now()))
        for root, dirs, files in os.walk(local_dir):
            for filespath in files:
                if(datetime.datetime.now().strftime("%Y%m%d%H%M%S")[4:8] in filespath or 'update.xml' in filespath):
                    logger.info('开始上传文件:{}'.format(filespath))
                    local_file = os.path.join(root, filespath)
                    a = local_file.replace(local_dir, '').replace('\\', '/').lstrip('/')
                    remote_file = os.path.join(remote_dir, a).replace("\\", "/")
                    sftp.put(local_file, remote_file)
        logger.info('文件上传完成:{}'.format(datetime.datetime.now()))
        t.close()
    except Exception as e:
        print(e)


def main():
    global config
    with open('./config.json', 'r', encoding='utf_8_sig') as f:
        config = loads(f.read())

    path_list = os.listdir(config["gen_beauty_path"])
    for filename in path_list:
        if os.path.splitext(filename)[1] == ".iss" and "gen_beauty" in os.path.splitext(filename)[0]:
            alter_iss(config["gen_beauty_path"] + "\\" + filename, config["old_version"], config["new_version"])
            logger.info("{}|版本号由{}更新为{}".format(filename, config["old_version"], config["new_version"]))

    alter_cs(config["AssemblyInfo_path"], config["old_version"], config["new_version"])
    logger.info("{}|版本号由{}更新为{}".format("AssemblyInfo.cs", config["old_version"], config["new_version"]))

    alter_xml(config['update_xml_path'])
    logger.info("{}|更新完成".format("update.xml"))

    upload(config['local_C2_output'], config['remote_upload_dir'])
    upload("./", config['remote_upload_dir'])

if __name__ == "__main__":
    main()
