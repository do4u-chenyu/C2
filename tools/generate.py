#! env/python.exe
from os.path import split, splitext, getctime, join, exists, isfile, abspath
from os import chdir, getcwd, path
import os
from subprocess import Popen, PIPE
import re
import sys
from typing import List
from subprocess import Popen

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
    compile_cmd = r"Common7\Tools\VsDevCmd.bat & chdir /d {} & devenv C_T1.sln /Rebuild Release /Project C2Plugins\FullTextGrammarAssistant\FullTextAssistantPlugin.csproj & devenv C_T1.sln /Rebuild Release /Project C2Plugins\Game2048Plugin\Game2048Plugin.csproj & devenv C_T1.sln /Rebuild Release /Project C2Plugins\KnowledgeBase\KnowledgeBase.csproj & devenv C_T1.sln /Rebuild Release /Project C2Plugins\MD5Plugin\MD5Plugin.csproj & devenv C_T1.sln /Rebuild Release /Project C2Plugins\QQSpiderPlugin\QQSpiderPlugin.csproj & devenv C_T1.sln /Rebuild Release /Project C2Plugins\RookieKnowledgePlugin\RookieKnowledgePlugin.csproj & devenv C_T1.sln /Rebuild Release /Project C2Main\C2.csproj & devenv C_T1.sln /Rebuild Release /Project C2Shell\C2Shell.csproj & devenv C_T1.sln /Rebuild Release /Project C2Tests\C2UnitTests.csproj & devenv C_T1.sln /Rebuild Release /Project UserInstallSet\UserInstallSet.csproj & chdir /d {} & CALL Common7\IDE\CommonExtensions\Microsoft\VSI\DisableOutOfProcBuild\DisableOutOfProcBuild.exe & chdir /d {} & devenv C_T1.sln /Rebuild Release /Project IAO解决方案\IAO解决方案.vdproj".format(sln_path,vs_path,sln_path)
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

if __name__ == '__main__':
    file_path = "git-hook-scripts/vs_path"
    print(abspath(file_path))
    main(file_path)