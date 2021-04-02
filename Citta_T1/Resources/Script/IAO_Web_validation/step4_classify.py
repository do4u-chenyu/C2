# -*- coding: utf-8 -*-
from os import listdir, path
import requests
import json
import re
import urllib.parse
import base64
import shutil
from argparse import ArgumentParser
import urllib3
from global_method import *

urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)
result_pngs = []
noise_pngs = []
fail_pngs = []


def group_figure(png_path: str, output_path: str):
    # -----------------------将识别赌博图片保存到result_figure文件
    path1 = path.join(output_path, 'resultfigure')
    os.makedirs(path1, exist_ok=True)

    for png in result_pngs:
        shutil.copy(path.join(png_path, png), path.join(path1, png))
    # -----------------------将不识别赌博图片保存到rawFigure文件
    path2 = path.join(output_path, 'failfigure')
    os.makedirs(path2, exist_ok=True)

    for png in fail_pngs:
        shutil.copy(path.join(png_path, png), path.join(path2, png))
    # -----------------------将噪音图片保存到rawFigure文件
    path3 = path.join(output_path, 'noiseFigure')
    os.makedirs(path3, exist_ok=True)

    for png in noise_pngs:
        shutil.copy(path.join(png_path, png), path.join(path3, png))

def getKey():
    http_session = requests.Session()
    url = 'https://aip.baidubce.com/oauth/2.0/token?grant_type=client_credentials&client_id=wWNhaMTTsmVwuKHl7hboZCvq&client_secret=NoNjtTLbGCUM9ePVQAWdSa13HcdkMS4A'
    headers = {'Content-Type': 'application/json; charset=UTF-8'}
    http_session.headers = headers
    http_session.keep_aliver = False
    http_session.verify = False
    content = http_session.post(url, headers=headers)
    return content.text


if __name__ == '__main__':
    parser = ArgumentParser()
    parser.add_argument('input')
    parser.add_argument('output', nargs='*', default='default_path')
    args = parser.parse_args()
    input_path = args.input
    output_path = args.output

    file_list = listdir(input_path)
    # 设置解密结果默认输出路径
    if args.output == 'default_path':
        output_path = path.join(input_path, "..", default_output_path(4))
    tmpResult_path = path.join(output_path, 'tmpResult')

    png_names = []
    for f_name in file_list:
        if f_name[-3:] == 'png':
            png_names.append(f_name)
    f_number = [xs for xs in range(len(png_names))]

    # --------------------------获取秘钥-----------------
    key_list = getKey().split(',')
    keyValue = key_list[3].split(':')[1]
    # ----------------------图片转码-----------------
    os.makedirs(tmpResult_path, exist_ok=True)
    base64g_path = path.join(tmpResult_path, "base64g.txt")
    f = open(base64g_path, 'wb+')
    for png_name in png_names:
        png_dir = os.path.join(input_path, png_name)
        with open(png_dir, "rb") as file:
            base64_data = base64.b64encode(file.read())
            f.write(base64_data + b'\r\n')
    f.close()
    pattern1 = re.compile(Pos_keywords)
    pattern2 = re.compile(Neg_keywords)
    # --------------------------------------------------
    with open(base64g_path, 'r') as f:  # 打开文件
        lines = f.readlines()
    ocr_content_file = open(path.join(tmpResult_path, 'ocrContent.txt'), 'w', encoding='utf-8')
    for di in f_number:
        try:
            first_line = lines[di]
            hf = {'image': first_line.strip()}
            body = urllib.parse.urlencode(hf)
            http_session = requests.Session()
            url = 'https://aip.baidubce.com/rest/2.0/ocr/v1/general_basic?access_token=' + keyValue
            headers = {
                'Content-Type': 'application/x-www-form-urlencoded'}
            http_session.headers = headers
            http_session.keep_aliver = False
            http_session.verify = False
            nk = 0
        except:
            continue
        while nk < 3:
            try:
                req = http_session.post(url, headers=headers, data=body)
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
                    result_pngs.append(png_names[di])
                else:
                    noise_pngs.append(png_names[di])
                break
            except:
                nk = nk + 1
                if nk == 2:
                    fail_pngs.append(png_names[di])
    ocr_content_file.close()
    group_figure(input_path, output_path)
