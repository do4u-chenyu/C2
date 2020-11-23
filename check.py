from os.path import split, splitext, getctime, join, exists, isfile
from os import chdir, getcwd
from subprocess import Popen, PIPE
import re
import sys

from subprocess import Popen


def ex_cmd(cmd: str, cwd):
    print(cmd)
    ret = Popen(cmd, cwd=cwd, shell=True, stdout=PIPE).communicate()
    return ret

def re_findall(pat: re.Pattern, raw: str) -> str:
    rst = pat.findall(raw)
    return rst[0] if len(rst) != 0 else ""
         


def compile():
    #  ; devenv Citta_T1.sln /Build '开发调试'' /Project Citta_T1\C2.csproj /Out check.log
    # init_env_cmd = r"E:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\Tools\VsDevCmd.bat"
    # rst, err = ex_cmd(init_env_cmd)
    # if err is not None:
    #     print(err)
    #     return
    sln_path = getcwd()
    vs_path = r"E:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise"
    compile_cmd = r"Common7\Tools\VsDevCmd.bat & chdir /d {} & devenv Citta_T1.sln /Build 开发调试 /Project Citta_T1\C2.csproj /Out check.log".format(sln_path)
    info, err = ex_cmd(compile_cmd, vs_path)
    info = info.decode("gbk")
    pat_start_compile = re.compile(r"已启动生成")
    pat_succ_num = re.compile(r"生成: 成功 \d 个")
    pat_fail_num = re.compile(r"失败 \d 个")
    pat_new_num = re.compile(r"最新 \d 个")
    pat_skip_num = re.compile(r"跳过 \d 个")
    if err is None:
        print(info)
        succ_num = re.findall(pat_succ_num, info)
        fail_num = re.findall(pat_fail_num, info)
        new_num = re.findall(pat_new_num, info)
        skip_num = re.findall(pat_skip_num, info)
        if succ_num == "0" or fail_num != "0":
            sys.exit(-1)
        sys.exit(0)
    sys.exit(-2)


if __name__ == '__main__':
    compile()