import os
from global_method import ex_cmd
from argparse import ArgumentParser
if __name__ == '__main__':
    parser = ArgumentParser()
    parser.add_argument('input')
    parser.add_argument('output', nargs='*', default='default_path')
    parser.add_argument("-d", "--debug", action="store_true", dest="debug",
                        help="debug mode")
    parser.add_argument("-t", "--strict", action="store_true", dest="strict",
                        help="strict filter mode")
    parser.add_argument("-w", "--filter_by_neg_word", action="store_true", dest="filter_by_neg_word",
                        help="filter by negative word")
    parser.add_argument("-p", "--path", action="store", dest="path", default=os.path.abspath(os.path.join(os.path.dirname(__file__),  os.path.pardir, "Python3.7.6", "python.exe")),
                        help="path of python interruptor")
    args = parser.parse_args()
    debug = args.debug
    strict = args.strict
    filter_by_neg_word = args.filter_by_neg_word
    path = args.input
    output_path = args.output
    for file in os.listdir(path):
        if file.endswith("txt") and "queryResult_db" in file:
            file = os.path.join(path, file)
            cmd = "python icp_crawler.py -p {} -i".format(file)
            if debug:
                cmd += " -d"
            if strict:
                cmd += " -c"
            if filter_by_neg_word:
                cmd += " -w"
            if output_path != 'default_path':
                cmd += " -o {0}".format(output_path)
            ex_cmd(cmd)
