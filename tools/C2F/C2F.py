#coding:gbk
import os
import sys
import glob
import shutil
import zipfile
import datetime

from ctypes import *

psapi = windll.psapi
kernel = windll.kernel32

def EnumProcesses():
    lpidProcess = (c_ulong * 256)()
    cbNeeded = c_ulong() 
    psapi.EnumProcesses(byref(lpidProcess), sizeof(lpidProcess), byref(cbNeeded))
    nReturned = int(cbNeeded.value/sizeof(c_ulong()))
    pidProcess = [i for i in lpidProcess][:nReturned]
    
    p_list = []
    mod_name = c_buffer(1024)
    hModule, count = c_ulong(), c_ulong()
    for pid in pidProcess:
        hProcess = kernel.OpenProcess(0x0400 | 0x0010, False, pid)
        if hProcess:
            psapi.EnumProcessModules(hProcess, byref(hModule), sizeof(hModule), byref(count))
            psapi.GetModuleBaseNameA(hProcess, hModule.value, mod_name, sizeof(mod_name))
            p_list.append(str(mod_name.value, encoding = "GBK"))
            for i in range(mod_name._length_):
                mod_name[i]= 0
            kernel.CloseHandle(hProcess)
    return p_list

def decode_cp437_gbk(dir_path):
    for i in os.listdir(dir_path):
        try:
            gbk = i.encode("cp437").decode("gbk")
            src = os.path.join(dir_path, i)
            dst = os.path.join(dir_path, gbk)
            os.rename(src, dst)
        except:
            pass
        if os.path.isdir(dst):
            decode_cp437_gbk(dst)

def self_extract(sfx_path):
    return os.system("cd /d {0} && {1}".format(os.path.dirname(sfx_path), sfx_path))
    
#������Դ�ļ�Ŀ¼����·��
def resource_path(relative_path):
    if getattr(sys, 'frozen', False): #�Ƿ� Bundle Resource
        base_path = sys._MEIPASS
    else:
        base_path = os.path.abspath(".")
    return os.path.join(base_path, relative_path)
       
def without_extension(path):
    return os.path.basename(path.split('.')[0])

def roll_rmtree(path, ignore_path):
    backuplist = glob.glob(os.path.join(path + r'\202*'))
    if ignore_path in backuplist:
        backuplist.remove(ignore_path)
    backuplist.sort()
    if (len(backuplist) >= 5):            # ɾ������5����������ɵ�
        shutil.rmtree(backuplist[0], ignore_errors=True)   


def check_c2_running():
    pList = EnumProcesses()
    pList = [i.lower() for i in pList]
    if 'c2.exe' in pList:
        print('����ʦ������սװ��(C2.exe)-����������, ���ȹر�C2�ٰ�װս���ֲ�\r\n')
        print('\r\n')
        print('����ʦ������սװ��(C2.exe)-����������, ���ȹر�C2�ٰ�װս���ֲ�\r\n')
        print('\r\n')
        print('����ʦ������սװ��(C2.exe)-����������, ���ȹر�C2�ٰ�װս���ֲ�\r\n')
        os.system("pause")
        sys.exit()
    

def install():
    print('ս���ֲ����ڰ�װ��...' + '\r\n')
    now_string = datetime.datetime.now().strftime('%Y%m%d%H%M%S')
    src_path = resource_path("c2f")
    sfx_path = os.path.join(src_path, 'zssc.exe')
    #src_path = "C:\\work\\C2F"    # ������
    dst_path = r"C:\FiberHomeIAOModelDocument\IAO\ս���ֲ�"
    bak_path = r"C:\FiberHomeIAOModelDocument\IAO\��������"
    
    print('��ѹ����ʱ�ļ� : {0} \r\n'.format(sfx_path))
    self_extract(sfx_path)
    
    print('��ʼ��Ŀ¼...' + '\r\n')
    bak_path = bak_path + "\\" + now_string
    shutil.rmtree(bak_path, ignore_errors=True)
    os.makedirs(dst_path, exist_ok=True)
    os.makedirs(bak_path, exist_ok=True)
    
    # ���ݾ�����
    shutil.copytree(dst_path, bak_path, dirs_exist_ok=True)
    print("���ݾ��ĵ��� {0} ���, ����Ҫ����ǰ��\r\n".format(bak_path))
       
    # ɾ����ʱ�ļ�
    shutil.rmtree(dst_path, ignore_errors=True)
    os.makedirs(dst_path, exist_ok=True)
    
    # ��ѹ��
    print('���ڽ�ѹ��...\r\n')
    c2list = glob.glob(src_path + r"\*.c2")
    for c2 in c2list:
        path_to = os.path.join(dst_path, without_extension(c2))
        os.makedirs(path_to, exist_ok=True)
        fzip = zipfile.ZipFile(c2)
        fzip.extractall(path_to)
        decode_cp437_gbk(path_to)          # ���zipfile���ѹ�������������
        print('��װ���:\t��{0}��{1}'.format(os.path.basename(c2), os.linesep))
        
    # roll����ɾ������ı���
    print('ɾ�����ڱ���...\r\n')
    roll_rmtree(os.path.dirname(bak_path), bak_path)
        
    print('ս���ֲᰲװ�ɹ���������C2���������C2������Ч\r\n')

if __name__ == "__main__":

    check_c2_running()
    try:
        install()    
    except:
        print('ս���ֲᰲװ���ִ��󣬽�ͼ���ۺ�Ⱥ��ü���֧��\r\n')
    os.system("pause")
    