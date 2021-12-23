#coding:utf-8
import sys
import os
from os import chdir, mkdir
from glob import glob
import zipfile
import time
import shutil


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


def rename_subfolders(path,name):       
    os.chdir(path)
    if os.path.exists(name):     
        add_name= name+'.bak'
        if os.path.exists(add_name):
            new_name= add_name+'.bak'
            if os.path.exists(new_name):
                shutil.rmtree(name, ignore_errors=True)
                shutil.rmtree(add_name, ignore_errors=True)
                shutil.rmtree(new_name, ignore_errors=True)
                mkdir(name)
            else:
                os.rename(add_name,new_name)
                os.rename(name,add_name)
                mkdir(name)
        else:
            os.rename(name,add_name)
            mkdir(name)
    else:
        mkdir(name)


def del_zipfile(path):
    ls = os.listdir(path)
    for i in ls:
        if (i[-3:])=='zip':
            c_path = os.path.join(path, i)
            if os.path.isdir(c_path):
                del_zipfile(c_path)
                print(c_path)
            else:
                os.remove(c_path)
                print(c_path)


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


if __name__ == "__main__":
    src_path = r"D:\work\C2F"
    dst_path = r"C:\FiberHomeIAOModelDocument\IAO\业务视图"
    for root, _, fnames in os.walk(src_path):
        for fname in fnames:  # sorted函数把遍历的文件按文件名排序
            fpath = os.path.join(root, fname)
            shutil.copy(fpath, dst_path)  # 完成文件拷贝
            print(fname + " has been copied!")
    chdir(dst_path)
    c2_list = glob("*.c2")
    for c2f in c2_list:
        file = resource_path(os.path.join("D:\work\C2F",c2f))
        print(file)
        rename_c2(file)


    c2zip_list = glob("*.zip")
    for c2zip in c2zip_list:
        print(c2zip)
        fz = zipfile.ZipFile(os.path.abspath(c2zip), 'r')
        for file in fz.namelist():
            fz.extract(file, dst_path) 
        zipname = c2zip.split('.')[0]
        rename_subfolders(dst_path,zipname)
        path = os.path.join(dst_path, zipname)
        #fz.extractall(path)
        change_name(path)


    #del_zipfile(dst_path)
    time.sleep(3)