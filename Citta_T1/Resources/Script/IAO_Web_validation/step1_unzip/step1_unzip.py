from glob import glob
from argparse import ArgumentParser
from os import chdir, mkdir, rename, walk, getcwd
from os.path import splitext, exists, join, abspath
from global_method import *


def decrypt(file_path):
    with open(file_path, "rb") as f:
        data = bytearray(f.read())
    with open("_{}".format(file_path), "wb") as f:
        ret = b"\x1f\x8b\x08\x00" + data[22:]
        f.write(ret)


if __name__ == '__main__':
    parser = ArgumentParser()
    parser.add_argument('input')
    parser.add_argument('output', nargs='*', default='default_path')
    args = parser.parse_args()
    input_path = args.input
    output_path = args.output
    abs_root_path = abspath(input_path)
    # 设置解密结果默认输出路径
    if args.output == 'default_path':
        output_path = join(input_path, default_output_path(1))

    chdir(abs_root_path)
    tgz_list = glob("*.tgz")
    for tgz in tgz_list:
        decrypt(tgz)
    db_tgz_list = glob("_*.tgz")
    for _tgz in db_tgz_list:
        # 输出路径加上数据包名称，便于多地市数据包一起解密
        city_output_path = join(output_path, _tgz)
        # _分割
        num = _tgz[1:].split("_")[0]
        _tgz_name = splitext(_tgz)[0]
        if not exists(_tgz_name):
            mkdir(_tgz_name)
        unzip_tgz(_tgz, _tgz_name)

        # 重命名
        dot_files_path = join(_tgz_name, "_queryResult_db_")
        chdir(dot_files_path)
        tgz_list = glob("result_password*")
        for tgz in tgz_list:
            unzip_tgz(tgz, city_output_path)
        chdir(abs_root_path)
