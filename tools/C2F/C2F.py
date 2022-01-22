#coding:utf-8
import sys
import os
from os import chdir, mkdir
from glob import glob
import zipfile
import time
import shutil
from os import path
# from ctypes import *

# #PSAPI.DLL
# psapi = windll.psapi
# #Kernel32.DLL
# kernel = windll.kernel32

# def EnumProcesses():
    # arr = c_ulong * 256
    # lpidProcess= arr()
    # cb = sizeof(lpidProcess)
    # cbNeeded = c_ulong()
    # hModule = c_ulong()
    # count = c_ulong()
    # modname = c_buffer(1024)
    # PROCESS_QUERY_INFORMATION = 0x0400
    # PROCESS_VM_READ = 0x0010
    
    # #Call Enumprocesses to get hold of process id's
    # psapi.EnumProcesses(byref(lpidProcess),
                        # cb,
                        # byref(cbNeeded))
    
    # #Number of processes returned
    # nReturned = int(cbNeeded.value/sizeof(c_ulong()))

    # pidProcess = [i for i in lpidProcess][:nReturned]
    
    # for pid in pidProcess:
        
        # #Get handle to the process based on PID
        # hProcess = kernel.OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ,
                                      # False, pid)
        # if hProcess:
            # psapi.EnumProcessModules(hProcess, byref(hModule), sizeof(hModule), byref(count))
            # psapi.GetModuleBaseNameA(hProcess, hModule.value, modname, sizeof(modname))
            # if str(modname.value, encoding="GBK") == 'C2.exe':
                # return 1
            # for i in range(modname._length_):
                # modname[i]=  0
            # kernel.CloseHandle(hProcess)

#生成资源文件目录访问路径
def resource_path(relative_path):
    if getattr(sys, 'frozen', False): #是否Bundle Resource
        base_path = sys._MEIPASS
    else:
        base_path = os.path.abspath(".")
    return os.path.join(base_path, relative_path)


def rename_c2(c2_file):
    c2_filename = os.path.basename(c2_file)
    c2_filepath = os.path.dirname(c2_file)
    portion = os.path.splitext(c2_filename)
    # 如果后缀是.txt
    if portion[1] == ".c2": 
        # 重新组合文件名和后缀名  
        c2_newname = portion[0] + ".zip"  
        os.rename(c2_filename,c2_newname)


def backups_C2F(src_path,c2f_name):
    os.chdir(r"C:\C2F_back")
    if os.path.exists(c2f_name):
        new_name= c2f_name+'.bak'
        if os.path.exists(new_name):
            shutil.rmtree(new_name, ignore_errors=True)
            os.rename(c2f_name,new_name)
        else:
            os.rename(c2f_name,new_name)
        shutil.move(os.path.join(src_path,c2f_name),r"C:\C2F_back")
    else:
        shutil.move(os.path.join(src_path,c2f_name),r"C:\C2F_back")


def rename_subfolders(path,name):       
    os.chdir(path)
    if os.path.exists(name):
        add_name= name+'.bak'
        os.rename(name,add_name)
        mkdir(name)
        backups_C2F(path,add_name)
    else:
        mkdir(name)


def change_name(filename):
    os.chdir(filename)
    for i in os.listdir("."):
        try:
            test_name=i.encode("cp437")
            test_name=test_name.decode("gbk")#将文件名转为gbk中文编码
            os.rename(i,test_name)#重命名
            i=test_name
        except:
            pass           
        if os.path.isdir(i):#如果解压后的是一个文件夹
            change_name(i)
    os.chdir('..')


def del_zipfile(path):
    ls = os.listdir(path)
    for i in ls:
        if (i[-3:])=='zip':
            c_path = os.path.join(path, i)
            try:
                os.unlink(c_path)
            except:
                pass


if __name__ == "__main__":
    print('战术手册正在安装中...' + '\r\n')
    src_path = resource_path("c2f")
    dst_path = r"C:\FiberHomeIAOModelDocument\IAO\业务视图"
    
    if not path.exists(dst_path):
        os.makedirs(dst_path)

    # if EnumProcesses() == 1:
        # print('C2.exe正在运行，请关闭此运行程序')
        # os.system("pause")
    # else:
    chdir(r"C:\\")
    if not path.exists("C2F_back"):
        os.mkdir("C2F_back")

    #src_path = r"D:\work\C2F"

    for root, _, fnames in os.walk(src_path):
        for fname in fnames:  # sorted函数把遍历的文件按文件名排序
            if '.c2' in fname:
                fpath = os.path.join(root, fname)
                shutil.copy(fpath, dst_path)  # 完成文件拷贝

    chdir(dst_path)
    for filename in os.listdir("."):
        if '.zip' in filename:
            os.remove(os.path.join(dst_path,filename))

    c2_list = glob("*.c2")
    for c2f in c2_list:
        rename_c2(os.path.join(dst_path,c2f))

    c2zip_list = glob("*.zip")
    for c2zip in c2zip_list:
        fz = zipfile.ZipFile(os.path.abspath(c2zip), 'r')
        zipname = c2zip.split('.')[0]
        rename_subfolders(dst_path,zipname)
        path = os.path.join(dst_path, zipname)
        fz.extractall(path)
        change_name(path)
        
    print(r"旧文档备份到 C:\C2F_back 完毕, 有需要自行前往" + '\r\n')
    print('删除临时文件' + '\r\n')
    del_zipfile(dst_path)
    print('战术手册安装成功，请重启C2，务必重启C2才能生效')
    os.system("pause")
    