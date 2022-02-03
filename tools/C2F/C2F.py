#coding:gbk
import os
import sys
import glob
import shutil
import zipfile
import datetime

def change_name(dir_path):
    os.chdir(dir_path)
    for i in os.listdir("."):
        try:
            gbk_name = i.encode("cp437").decode("gbk")  #将文件名转为gbk中文编码
            os.rename(i, gbk_name)#重命名
        except:
            pass           
        if os.path.isdir(gbk_name): # 递归
            change_name(gbk_name)
    os.chdir('..')

#生成资源文件目录访问路径
def resource_path(relative_path):
    if getattr(sys, 'frozen', False): #是否Bundle Resource
        base_path = sys._MEIPASS
    else:
        base_path = os.path.abspath(".")
    return os.path.join(base_path, relative_path)
       
def without_extension(path):
    return os.path.basename(path.split('.')[0])
    
def install():
    print('战术手册正在安装中...' + '\r\n')
    now_string = datetime.datetime.now().strftime('%Y%m%d%H%M')
    
    src_path = resource_path("c2f")
    dst_path = r"C:\FiberHomeIAOModelDocument\IAO\战术手册"
    bak_path = r"C:\FiberHomeIAOModelDocument\IAO\备份数据" + "\\" + now_string
    
    print('初始化目录...' + '\r\n')
    shutil.rmtree(bak_path, ignore_errors=True)      # 备份目录不应该重复   
    os.makedirs(dst_path, exist_ok=True)
    os.makedirs(bak_path, exist_ok=True)
    
    # 备份旧数据
    shutil.copytree(dst_path, bak_path, dirs_exist_ok=True)
    print("备份旧文档到 {0} 完毕, 有需要自行前往\r\n".format(bak_path))
       
    # 删除临时文件
    shutil.rmtree(dst_path, ignore_errors=True)
    os.makedirs(dst_path, exist_ok=True)
    
    # 解压缩
    print('正在解压缩...\r\n')
    c2list = glob.glob(src_path + r"\*.c2")
    for c2 in c2list:
        path_to = os.path.join(dst_path, without_extension(c2))
        os.makedirs(path_to, exist_ok=True)
        fzip = zipfile.ZipFile(c2)
        fzip.extractall(path_to)
        change_name(path_to)        # 解决zipfile库解压中文乱码的问题,这个函数实现的极其丑陋
        
    print('战术手册安装成功，请重启C2，务必重启C2才能生效\r\n')

if __name__ == "__main__":
    try:
        install()    
    except:
        print('战术手册安装出现错误，截图发售后群获得技术支持\r\n')
        
    os.system("pause")
    