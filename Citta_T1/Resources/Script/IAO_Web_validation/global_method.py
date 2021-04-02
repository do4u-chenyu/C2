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
    # progress.wait()
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


# 分来脚本关键词
Pos_keywords = '.*登入.*|.*管理.*|.*用戶.*|.*Password.*|.*password.*|.*Username.*|.*username.*|.*赛车.*|.*飞艇.*|.*女优.*|.*美女.*|.*影院.*|.*直播.*|.*影音.*|.*经销.*|.*电商.*|.*营销.*|.*彩票.*|.*购彩.*|.*大三巴.*|.*新葡京.*|.*好运金.*|.*体育投注.*|.*高水位.*|.*返水.*|.*bet.*|.*老虎机.*|.*博彩.*|.*彩金.*|.*周周彩金.*|.*彩世界.*|.*投无上限.*|.*德州麻将.*|.*快乐彩.*|.*时时彩.*|.*澳门.*|.*彩民.*|.*首次存送.*|.*棋牌.*|.*真人.*|.*澳门威尼斯人.*|.*葡京.*|.*用户登录.*|.*账号.*|.*密码.*|.*验证码.*|.*登录.*|.*分彩.*|.*用户名.*|.*管理系统.*|.*彩世界.*|.*红包.*|.*捕鱼.*|.*电子游艺.*|.*快3.*|.*六合彩.*|.*开奖号码.*|.*游戏玩家.*|.*存款.*|.*取款.*|.*彩金.*|.*VIP专属群.*|.*灭庄.*|.*后台.*|.*金花.*|.*代充.*|.*红利.*|.*代理登录.*|.*大发.*|.*开奖.*|.*楼凤.*|.*演员.*|.*日本.*'
Neg_keywords = '.*仅供在备案成功前调试网站.*|.*无法访问此网站.*|.*运行Windows网络检查.*|.*Managing.*Tomcat.*|.*Apache Tomcat.*|.*云供应链.*'
