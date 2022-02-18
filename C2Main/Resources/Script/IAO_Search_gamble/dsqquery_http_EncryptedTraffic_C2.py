# -*- coding: utf-8 -*-
import sys
import datetime
import time
import os
from subprocess import Popen, PIPE
from os import listdir, mkdir, makedirs
from os.path import join, isfile, basename, isdir, getsize
import imp
from struct import unpack
import zlib
import re

def handle_file_path(current_path):
    isSeqFile = lambda filePath: isfile(filePath) and filePath[-4:] == '.seq'
    fileList = []
    for dir in os.listdir(current_path):
        dir_path = join(current_path, dir)
        if isSeqFile(dir_path):
            fileList.append(dir_path)
        elif isdir(dir_path):
            fileList.extend(handle_file_path(dir_path))
    return fileList

def convert_content(srcBuffer):
    desBuffer = {}
    endFlag = len(srcBuffer) - 1
    curInd = 0
    while curInd < endFlag:
        curInd += 2
        keyLen = unpack('B', srcBuffer[curInd - 1])[0]

        key = srcBuffer[curInd:curInd + keyLen]
        curInd += keyLen + 4

        valueLen = unpack('I', srcBuffer[curInd - 4:curInd])[0]
        curInd += valueLen
        desBuffer[key] = srcBuffer[curInd - valueLen:curInd]
    return desBuffer

def process_rule(fieldDict):
    mainFile = fieldDict['_MAINFILE'] if '_MAINFILE' in fieldDict else ''
    isHttp = True if mainFile.endswith('.http') else False
    if not isHttp or not mainFile:
        return 0

    _HOST = fieldDict['_HOST'] if '_HOST' in fieldDict else ''
    capturetime = fieldDict['CAPTURE_TIME'] if 'CAPTURE_TIME' in fieldDict else ''
    _RELATIVEURL = fieldDict['_RELATIVEURL'] if '_RELATIVEURL' in fieldDict else ''
    _TEXT = fieldDict['_TEXT'] if '_TEXT' in fieldDict else ''
    _USERAGENT = fieldDict['_USERAGENT'] if '_USERAGENT' in fieldDict else ''
    AUTH_ACCOUNT = fieldDict['AUTH_ACCOUNT'] if 'AUTH_ACCOUNT' in fieldDict else ''
    STRSRC_IP = fieldDict['STRSRC_IP'] if 'STRSRC_IP' in fieldDict else ''
    STRDST_IP = fieldDict['STRDST_IP'] if 'STRDST_IP' in fieldDict else ''
    _REFERER = fieldDict['_REFERER'] if '_REFERER' in fieldDict else ''

    if ( _RELATIVEURL.endswith("asp") or _RELATIVEURL.endswith("jsp") or _RELATIVEURL.endswith("php")) and re.search("^[0-9a-zA-Z+/ ]{1000,25000}={0,2}$",_TEXT):
        otherStr = '\t'.join([_HOST, mainFile, capturetime, _RELATIVEURL, _USERAGENT, AUTH_ACCOUNT, STRSRC_IP, STRDST_IP, _REFERER])
        return otherStr
    return 0

def slaver_one_file(oneSeq, curNumCount):
    if not isfile(oneSeq):
        raise IOError(oneSeq + " has been removed before handling it")
        sys.stderr.write(datetime.datetime.now().strftime("%Y%m%d%H%M%S") + " - LOGGER WARNING: " + oneSeq + " has been removed before handling it" + "\n")
    with open(oneSeq, 'rb') as F:
        while True:
            ans = F.read(5)
            if ans == '\xff\xff\xff\xff\xff' or ans == '\x00\x00\x00\x00\x00':
                break
            try:
                contentSize, state = unpack('IB', ans)
            except Exception, e:
                sys.stderr.write(datetime.datetime.now().strftime("%Y%m%d%H%M%S") +" - LOGGER WARNING: can\'t unpack the file\'s contentSize; except: "+ str(e) + "\n")

            oriContent = F.read(contentSize)
            retContent = oriContent
            if state == 0:
                # means the content has been compressed
                try:
                    convContent = zlib.decompress(oriContent)
                    retContent = convert_content(convContent)
                except Exception, e:
                    sys.stderr.write(datetime.datetime.now().strftime("%Y%m%d%H%M%S") + " - LOGGER WARNING: can\'t zlib decopress the content; except: " + str(e) + "\n")
                    break

            try:
                if not isinstance(retContent, dict):
                    continue
                content_str = process_rule(retContent)
                if content_str == 0:
                    continue

                if curNumCount >= 100000:
                    break
                curNumCount += 1
                sys.stdout.write(content_str+ "\n")
                sys.stdout.flush()
            except Exception, e:
                sys.stderr.write(datetime.datetime.now().strftime("%Y%m%d%H%M%S") + " - LOGGER WARNING: process rule failed with error: {0}".format(e) + "\n")
    return curNumCount

def producer_run(oneProFiles):
    curNumCount = 0
    header = ["_HOST", "mainFile", "capturetime", "_RELATIVEURL", "_USERAGENT", "AUTH_ACCOUNT", "STRSRC_IP", "STRDST_IP", "_REFERER"]
    #path = os.path.join(DATA_PATH, 'dsq_result_' + datetime.datetime.now().strftime("%Y%m%d") + '.txt')
    #with open(path, 'w') as f:
    sys.stdout.write("\t".join(header) + "\n")
    sys.stdout.flush()
    for oneFile in oneProFiles:
        sys.stderr.write(datetime.datetime.now().strftime("%Y%m%d%H%M%S") + ' - LOGGER INFO: current handling file: ' + oneFile + "\n")
        try:
            curNumCount = slaver_one_file(oneFile, curNumCount)
            if curNumCount >= 100000:
                break
        except Exception, e:
            sys.stderr.write(datetime.datetime.now().strftime("%Y%m%d%H%M%S") + " - LOGGER WARNING: " + str(e) + "\n")
    sys.stderr.write(datetime.datetime.now().strftime("%Y%m%d%H%M%S") + " - All results: " + str(curNumCount) + "\n")

def main():
    sys.stderr.write(datetime.datetime.now().strftime("%Y%m%d%H%M%S") + ' - LOGGER INFO: START DSQ BATCH....' + '\n')
    #按时间降序排列获取当前机器上所有seq文件列表
    fileList = handle_file_path(rootTreeNode)
    fileList.sort(reverse=True)
    #开始producer
    queryStart = datetime.datetime.now().strftime("%Y%m%d%H%M%S")
    producer_run(fileList)
    queryEnd = datetime.datetime.now().strftime("%Y%m%d%H%M%S")
    sys.stderr.write(datetime.datetime.now().strftime("%Y%m%d%H%M%S") + ' - LOGGER INFO: query Time: {0}-{1}'.format(queryStart, queryEnd) + '\n')
    sys.stderr.write(datetime.datetime.now().strftime("%Y%m%d%H%M%S") + ' - LOGGER INFO: END DSQ BATCH....' + "\n")

if __name__ == '__main__':
    rootTreeNode = '/fsindex/sequence/general/'
    main()