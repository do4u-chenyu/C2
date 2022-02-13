#! env/python.exe
import os
import sys


def try_unlink(path):
    try:
        os.unlink(path)
    except:
        pass

def C2F_compress():
    driver, other = os.path.splitdrive(os.getcwd())
    c2f_path = os.path.join(driver, "\\work", "C2F")
    z7_path  = os.path.join(driver, "\\work", "C2", "tools", "zip", '7z.exe')
    out_path = os.path.join(driver, "\\work", "C2", "tools", "C2F", "build", "zssc.exe")
    print("删除旧数据: {0}".format(out_path))
    try_unlink(out_path)
    print("压缩C2F到 : {0}".format(out_path))
    # zip\7z.exe a -mx9  -sfx C2F\build\zssc.exe C:\work\C2F\*.c2
    z7_cmd = "{0} a -mx9 -sfx {1} {2}\*.c2".format(z7_path, out_path, c2f_path)
    return os.system(z7_cmd)
    
def C2F_install():
    spec_path = os.path.join(os.getcwd(), "C2F")
    install_cmd = r'chdir /d {} & pyinstaller -F C2F.spec --upx-dir upx394w'.format(spec_path)
    #install_cmd = r'chdir /d {} & pyinstaller -F C2F.spec'.format(spec_path)
    print(install_cmd)
    return os.system(install_cmd)


if __name__ == '__main__':
    
    
    if C2F_compress() == 0 and C2F_install() == 0:
        print(r"C2F安装包成功生成到 " + os.path.abspath(r'C2F\dist') + " 文件夹下")
    else:
        print("C2F安装包生成失败")
    