from argparse import ArgumentParser
from os import path, listdir
import os.path
import shutil


def is_image_file(filename):
    return any(filename.endswith(extension) for extension in ['.png', '.jpg', '.jpeg', '.PNG', '.JPG', '.JPEG'])


def modify_host(host):
    return host.replace(":", "冒号") + ".png"


if __name__ == '__main__':
    parser = ArgumentParser()
    parser.add_argument('csv_input')
    parser.add_argument('png_input')
    args = parser.parse_args()
    csv_path = args.csv_input
    png_input = args.png_input

    image_filenames = {x: path.join(png_input, x) for x in listdir(png_input) if is_image_file(x)}
    with open(csv_path, 'r', encoding='utf8')as f:
        for line in f:
            row = line.strip('\r\n').split('\t')
            if len(row) < 1 or row[0] == "":
                continue
            else:
                png_folder = path.join(path.dirname(csv_path),'05最终结果', row[0].strip('.csv'))
                os.makedirs(png_folder, exist_ok=True)
                host = modify_host(row[1])
                if host in image_filenames.keys():
                    shutil.copyfile(image_filenames[host], path.join(png_folder, host))
