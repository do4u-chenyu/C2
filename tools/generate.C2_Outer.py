#! env/python.exe
from os.path import split, splitext, getctime, join, exists, isfile, abspath
from os import chdir, getcwd, path
import os
import shutil
from subprocess import Popen, PIPE
import re
import sys
from typing import List
from subprocess import Popen
import ctypes, sys
import subprocess
import tempfile
from pathlib import Path


def try_remove(path):
    try:
        os.remove(path)
    except:
        pass

def read_paths(file_path: str) -> List[str]:
    with open(file_path) as f:
        ret = []
        for path in f.readlines():
            ret.append(path.strip())
        return ret

def ex_cmd(cmd: str, cwd=None):
    print(cmd)
    ret = Popen(cmd, cwd=cwd, shell=True, stdout=PIPE)
    for line in iter(ret.stdout.readline,b''):
        strLine=str(line.decode("gbk")).rstrip()
        print(strLine)
    ret.stdout.close()
    ret.wait()
    return "",""

def compile(vs_path: str) -> None:
    sln_path = os.path.abspath(os.path.dirname(os.getcwd()))
    compile_cmd = r"Common7\Tools\VsDevCmd.bat & chdir /d {} & devenv C_T1.sln /Rebuild C2_Outer /Project C2Plugins\FullTextGrammarAssistant\FullTextAssistantPlugin.csproj & devenv C_T1.sln /Rebuild C2_Outer /Project C2Plugins\Game2048Plugin\Game2048Plugin.csproj & devenv C_T1.sln /Rebuild C2_Outer /Project C2Plugins\KnowledgeBase\KnowledgeBase.csproj & devenv C_T1.sln /Rebuild C2_Outer /Project C2Plugins\MD5Plugin\MD5Plugin.csproj & devenv C_T1.sln /Rebuild C2_Outer /Project C2Plugins\QQSpiderPlugin\QQSpiderPlugin.csproj & devenv C_T1.sln /Rebuild C2_Outer /Project C2Plugins\RookieKnowledgePlugin\RookieKnowledgePlugin.csproj & devenv C_T1.sln /Rebuild C2_Outer /Project C2Main\C2.csproj & devenv C_T1.sln /Rebuild C2_Outer /Project UserInstallSet\UserInstallSet.csproj & chdir /d {} & CALL Common7\IDE\CommonExtensions\Microsoft\VSI\DisableOutOfProcBuild\DisableOutOfProcBuild.exe & chdir /d {} & devenv C_T1.sln /Rebuild C2_Outer /Project IAO解决方案\IAO解决方案.vdproj".format(sln_path,vs_path,sln_path)
    ex_cmd(compile_cmd, vs_path)

def main(file_path: str) -> None:
    if not exists(file_path):
        print("找不到文件：{}".format(abspath(file_path)))
        sys.exit(-1)
    vs_paths = read_paths(file_path)
    for path in vs_paths:
        if exists(path):
            compile(path)
            return
    print("没有找到vs编译环境, 请将vs目录添加到tools/git-hook-scripts/vs_path")

def C2_uninstall():
    #uninstall_cmd = "wmic product where Name=\"IAO解决方案\" call uninstall /nointeractive"
    tmp=(os.popen("wmic product where Name=\"IAO解决方案\" get IdentifyingNumber|awk 'NR==2{print}'")).read()
    uninstall_cmd = "msiexec /q /x " + tmp; 
    print(uninstall_cmd)
    if os.system(uninstall_cmd) == 0:
        return 0;
    else:
        return -1;


def C2_install():
    product_path = os.path.abspath(os.path.dirname(os.getcwd())) + "\IAO解决方案\C2_Outer"
    install_cmd = r"chdir /d {} & msiexec /i IAO解决方案.msi /qr".format(product_path)
    print(install_cmd)
    if os.system(install_cmd) == 0:
        return 0;
    else:
        return -1;

def beauty_product():
    fullText  = r"C:\Program Files\FiberHome\IAO解决方案\plugins\FullTextGrammarAssistant.dll"
    Knowlede  = r"C:\Program Files\FiberHome\IAO解决方案\plugins\KnowledgeBase.dll"
    tmpfd, tempfilename = tempfile.mkstemp()
    tmpdir = os.path.split(tempfilename)[0]
    tmpRedisASK = os.path.join(tmpdir,"tmpRedisASK\\tmpRedisASK.xml")
    my_file = Path(tmpRedisASK)
    if my_file.is_file():
        try_remove(tmpRedisASK)
    try_remove(fullText)
    try_remove(Knowlede)
    beauty_path = "C:\Program Files (x86)\Inno Setup 5"
    beauty_cmd = "chdir /d {} & compil32 /cc \"".format(beauty_path) + os.path.abspath(os.path.dirname(os.getcwd())) + "\C2打包程序\gen_beauty_outer_package.iss\""
    print(beauty_cmd)
    if os.system(beauty_cmd) == 0:
        return 0;
    else:
        return -1;

if __name__ == '__main__':
    file_path = "git-hook-scripts/vs_path"
    print(abspath(file_path))
    main(file_path)
    tmp = len(os.popen("wmic product where Name=\"IAO解决方案\"").read())
    if tmp == 4:
        if C2_install() == 0:
            print("新版IAO解决方案安装成功")
            if beauty_product() == 0:
               print("美化版安装包生成成功，位于 " + os.path.abspath(os.path.dirname(os.getcwd())) + "\C2打包程序\output")
            else:
               print("美化版安装包生成失败")
        else:
            print("新版IAO解决方案安装失败")
    else:
        if C2_uninstall() == 0:
            del_cmd = "del /f /s /q \"C:\Program Files\FiberHome\IAO解决方案\""
            if os.system(del_cmd) == 0:
                print("旧版IAO解决方案卸载成功")
            else:
                print("旧版IAO解决方案卸载失败")
            if C2_install() == 0:
                print("新版IAO解决方案安装成功")
                if beauty_product() == 0:
                    print("美化版安装包生成成功，位于 " + os.path.abspath(os.path.dirname(os.getcwd())) + "\C2打包程序\output")
                else:
                    print("美化版安装包生成失败")
            else:
                print("新版IAO解决方案安装失败,无法生成美化版安装包.")
        else:
            print("旧版IAO解决方案卸载失败")