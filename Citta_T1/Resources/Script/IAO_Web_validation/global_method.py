from subprocess import Popen, PIPE
import os.path
from copy import deepcopy
import tarfile

bin_env = os.path.abspath(os.path.join(os.path.dirname(__file__), "bin\\")) + ";"
sys32_env = r"C:\Windows\system32;"
GLOBAL_ENV = os.environ.copy()
GLOBAL_ENV["PATH"] = GLOBAL_ENV["PATH"].replace(bin_env, "").replace(sys32_env, "")
org_env = deepcopy(GLOBAL_ENV)
no32_env = deepcopy(GLOBAL_ENV)

if sys32_env not in org_env["PATH"]:
    org_env["PATH"] = sys32_env + org_env["PATH"]
if bin_env not in no32_env["PATH"]:
    no32_env["PATH"] = bin_env + no32_env["PATH"]


def ex_cmd(cmd: str, py_path=None, use_system_path: bool = False) -> None:
    print(cmd)
    print("当前工作路径" + os.getcwd())
    if "python" in cmd and py_path:
        cmd = cmd.replace("python", py_path)
    _env = org_env if use_system_path else no32_env
    progress = Popen(cmd, shell=True, env=_env, stdout=PIPE, stderr=PIPE)
    progress.communicate()
    progress.wait()
    # print("状态{}，标准输出{}，标准错误{}".format(status, out.decode(), err.decode()))


# 解压tgz文件
def unzip_tgz(src: str, aim: str):
    tar_obj = tarfile.open(src, "r:gz")
    for tarinfo in tar_obj:
        tar_obj.extract(tarinfo.name, aim)
    tar_obj.close()


# 设置默认输出路径
def default_output_path(step: int):
    switch = {1: step1(),
              2: step2(),
              3: step3(),
              4: step4(),
              5: step5()}
    return switch.get(step)


def step1():
    return "01解密"


def step2():
    return "02过滤"


def step3():
    return "03截图"


def step4():
    return "04分类"


def step5():
    return "05最终结果"
