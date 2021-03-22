# -*- coding: utf-8 -*-
import os
from os import listdir
import requests
import json
import re
import urllib.parse
import base64
import shutil
from argparse import ArgumentParser
import urllib3
import token_ocr

urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)
global c


def group_figure(png_path: str):
    # -----------------------将识别赌博图片保存到result_figure文件
    path1 = './/result_figure//'
    with open(r'./tmpResult/result.txt', 'r+') as file:
        for png in file:
            shutil.copy(png_path + png.strip(), path1 + png.strip())
    # -----------------------将不识别赌博图片保存到rawFigure文件
    path2 = './/failfigure//'
    with open(r'./tmpResult/fail.txt', 'r+') as file:
        for png in file:
            shutil.copy(png_path + png.strip(), path2 + png.strip())
    # -----------------------将噪音图片保存到rawFigure文件
    path3 = './/noiseFigure//'
    with open(r'./tmpResult/noise.txt', 'r+') as file:
        for png in file:
            shutil.copy(png_path + png.strip(), path3 + png.strip())


if __name__ == '__main__':
    parser = ArgumentParser()
    parser.add_argument("-p", "--path", action="store", dest="path", required=True, type=str,
                        help="Data path.")
    args = parser.parse_args()
    path = args.path
    file_list = listdir(path)

    png_names = []
    for f_name in file_list:
        if f_name[-3:] == 'png':
            png_names.append(f_name)
    f_number = [xs for xs in range(len(png_names))]

    # --------------------------获取秘钥-----------------
    key_list = token_ocr.getKey().split(',')
    keyValue = key_list[3].split(':')[1]
    # ----------------------图片转码-----------------
    if not os.path.exists(r"./tmpResult/"):
        os.makedirs(r"./tmpResult/")
    f = open(r"./tmpResult/base64g.txt", 'wb+')
    for png_name in png_names:
        png_dir = os.path.join(path, png_name)
        with open(png_dir, "rb") as file:
            base64_data = base64.b64encode(file.read())
            f.write(base64_data + b'\r\n')
    f.close()
    # -------------正向关键词-----------------
    keywords = ""
    negwords = ""
    with open('./keywords/posKeys.txt', 'r') as f:
        for line in f:
            keywords += line.strip()
    # -------------反向关键词-----------------
    with open('./keywords/negKeys.txt', 'r') as f:
        for line in f:
            negwords += line.strip()
    pattern1 = re.compile(keywords)
    pattern2 = re.compile(negwords)
    # --------------------------------------------------
    with open(r"./tmpResult/base64g.txt", 'r') as f:  # 打开文件
        lines = f.readlines()
    ocr_content_file = open(r"./tmpResult/ocrContent.txt", 'w', encoding='utf-8')
    result_file = open(r"./tmpResult/result.txt", 'w', encoding='utf-8')
    noise_file = open(r"./tmpResult/noise.txt", 'w', encoding='utf-8')
    fail_file = open(r"./tmpResult/fail.txt", 'w', encoding='utf-8')
    for di in f_number:
        # ------------------读取文件----------------
        # 读取所有行
        first_line = lines[di]
        hf = {'image': first_line.strip()}
        body = urllib.parse.urlencode(hf)
        c = requests.Session()
        url = 'https://aip.baidubce.com/rest/2.0/ocr/v1/general_basic?access_token=' + keyValue
        headers = {
            'Content-Type': 'application/x-www-form-urlencoded'}
        c.headers = headers
        c.keep_aliver = False
        c.verify = False
        nk = 0

        while nk < 3:
            try:
                req = c.post(url, headers=headers, data=body)
                print("processing pic: {}/{}".format(di + 1, len(f_number)))
                contentDict = json.loads(req.text)

                word_list = contentDict['words_result']
                feature_dic = {}
                a10 = str()
                for word_dict in word_list:
                    a10 = a10 + word_dict['words'] + ','
                feature_dic[png_names[di]] = a10
                ocr_content_file.write(png_names[di] + '识别内容:  ' + a10 + '\n')
                # -------------------------正则表达式进行文本匹配
                match1 = pattern1.match(a10)
                match2 = pattern2.match(a10)
                if match1 and not match2:
                    result_file.write(png_names[di] + '\n')
                else:
                    noise_file.write(png_names[di] + '\n')
                break
            except:
                nk = nk + 1
                if nk == 2:
                    fail_file.write(png_names[di] + '\n')
    ocr_content_file.close()
    result_file.close()
    noise_file.close()
    fail_file.close()



    group_figure(path)
