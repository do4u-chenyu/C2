# -*- coding: utf-8 -*-
import requests


# client_id 为官网获取的AK， client_secret 为官网获取的SK
def getKey():
    c = requests.Session()
    url = 'https://aip.baidubce.com/oauth/2.0/token?grant_type=client_credentials&client_id=wWNhaMTTsmVwuKHl7hboZCvq&client_secret=NoNjtTLbGCUM9ePVQAWdSa13HcdkMS4A'
    headers = {'Content-Type': 'application/json; charset=UTF-8'}
    c.headers = headers
    c.keep_aliver = False
    c.verify = False
    content = c.post(url, headers=headers)
    return content.text
