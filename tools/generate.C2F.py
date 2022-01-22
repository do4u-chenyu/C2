#! env/python.exe
from os.path import join
from os import chdir,getcwd, path
import os
import sys
import datetime
from glob import glob


def C2F_install(c2f_path):
    install_cmd = r"chdir /d {} & pyinstaller -F C2F.spec".format(c2f_path)
    print(install_cmd)
    if os.system(install_cmd) == 0:
        return 0;
    else:
        return -1;


if __name__ == '__main__':
    py_path = os.path.join(os.getcwd(), "C2F")
    if C2F_install(py_path) == 0:
        print(r"C2F安装包成功生成到 " + os.path.abspath(r'..\..\..\..\work\C2\tools\C2F\dist') + " 文件夹下")
    else:
        print("C2F安装包生成失败")
    