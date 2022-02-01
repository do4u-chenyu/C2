#coding:gbk
import os
import sys
import glob
import shutil
import zipfile
import datetime

#������Դ�ļ�Ŀ¼����·��
def resource_path(relative_path):
    if getattr(sys, 'frozen', False): #�Ƿ�Bundle Resource
        base_path = sys._MEIPASS
    else:
        base_path = os.path.abspath(".")
    return os.path.join(base_path, relative_path)
       
def without_extension(path):
    return os.path.basename(path.split('.')[0])

if __name__ == "__main__":
    print('ս���ֲ����ڰ�װ��...' + '\r\n')
    now_string = datetime.datetime.now().strftime('%Y%m%d%H%M%S')
    
    src_path = resource_path("c2f")
    dst_path = r"C:\FiberHomeIAOModelDocument\IAO\ս���ֲ�"
    bak_path = r"C:\FiberHomeIAOModelDocument\IAO\��������" + "\\" + now_string
    
    print('��ʼ��Ŀ¼...' + '\r\n')
    shutil.rmtree(bak_path, ignore_errors=True)      # ����Ŀ¼��Ӧ���ظ�   
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
        path_to = os.paht.join(dst_path, without_extension(c2))
        os.makedirs(path_to, exists_ok=True)
        fzip = zipfile.ZipFile(c2)
        fzip.extractall(path_to)
     
    print('ս���ֲᰲװ�ɹ���������C2���������C2������Ч')
    os.system("pause")
    