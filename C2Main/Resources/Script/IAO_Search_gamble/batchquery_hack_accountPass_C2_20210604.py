# -*- coding: utf-8 -*-
from Queue import Queue
from threading import Thread
from subprocess import Popen, PIPE
import time
import urllib
import re
import sys
import datetime
import os
import itertools
import logging
from optparse import OptionParser
import urllib2
import json
from urllib import unquote

reload(sys)
sys.setdefaultencoding('utf-8')


class Airport:
    def __init__(self, data_path, startTime, endTime, LOGGER):
        self.data_path = data_path
        self.startTime = startTime
        self.endTime = endTime
        self.LOGGER = LOGGER
        self.all_items = ['AUTH_ACCOUNT', 'AUTH_TYPE', 'CAPTURE_TIME', 'STRSRC_IP', 'SRC_PORT', 'STRDST_IP', 'DST_PORT',
                          '_HOST', '_RELATIVEURL', '_REFERER']
        self.model_key_dict = {
            "bt": {
                "col": self.all_items + ['USERNAME', 'PASSWORD', '_COOKIE', '_MAINFILE', '_QUERY_CONTENT', 'keyWords',
                                         'Path', 'Phone', 'sites_path', 'serverType', 'distribution', 'memSize',
                                         'backup_path', 'new_sites_path'],
                "key": ["DST_PORT:8888 AND login AND username AND password"]
            },
            "hack": {
                "col": self.all_items + ['USERNAME', 'PASSWORD', '_MAINFILE', '_QUERY_CONTENT', '_QUERY_MATCHTERMS',
                                         'keyWords'],
                "key": ["eval AND _POST", "assert AND _POST", "base64_decode AND _POST",
                        "Ba”.”SE6”.”4_dEc”.”OdE OR @ev”.”al", "Response.Write OR Response.End OR _USERAGENT:antSword",
                        "@ini_set “display_errors”,”0″", "_HOST:www.mtqyz.com", "_HOST:www.hebeilvteng.com",
                        "_HOST:www.33ddos.com", "_HOST:www.33ddos.cn", "_HOST:www.33ddos.org",
                        "_HOST:www.33ddos.cc OR _HOST:www.33ddos.net OR _HOST:v1.dr-yun.org OR _HOST:v2.dr-yun.org OR _HOST:v3.dr-yun.org OR _HOST:www.360zs.cn OR _HOST:www2.360zs.cn",
                        "_HOST:www.999yingjia.com"]
            },
            "ddos": {
                "col": self.all_items + ['USERNAME', 'PASSWORD', 'keyWords', 'post_last_line'],
                "key": [
                    "register.php AND username AND email AND password AND password_r AND checkcode AND tos AND register",
                    " login.php AND username AND password AND checkcode AND login",
                    " ajax.php  AND reateorder AND tradeno AND gid AND allprice AND price AND qq AND type AND number AND paypass AND coupon AND phone AND email",
                    " activation.php AND value",
                    " ajax AND login.php AND register AND username AND password AND rpassword AND scode AND email AND question AND answer",
                    " ajax AND login.php AND login AND username AND password",
                    " ajax.php createorder AND tradeno AND gid AND allprice AND price AND qq AND type AND number AND paypass AND coupon AND phone AND email",
                    " user AND code.php AND code AND jihuo",
                    " home AND login.php AND username AND email AND qq AND scode AND password AND password2 AND geetest_challenge AND geetest_validate AND geetest_seccode AND agree AND register",
                    " home AND login.php AND username AND password AND Login", " home AND code.php AND code AND jihuo",
                    " ajax.php AND create AND out_trade_no AND gid AND money AND rel AND type",
                    " Register AND user_name AND user_pass AND email_code AND token",
                    " Login AND user_name AND user_pass AND code AND token",
                    " Attack AND ip AND port AND type AND time",
                    " api.php AND username AND password AND host AND port AND time AND method"]
            },
            "apk": {
                "col": self.all_items + ['USERNAME', 'PASSWORD', '_COOKIE', '_MAINFILE', '_TITLE', '_TEXT', 'keyWords'],
                "key": ["_RELATIVEURL:apk", " apk",
                        " api.tw06.xlmc.sec.miui.com PackageName apk /api/ad/fetch/download",
                        " adfilter.imtt.qq.com TURL=http apk"]
            },
            "xss": {
                "col": self.all_items + ['USERNAME', 'PASSWORD', '_COOKIE', '_MAINFILE', '_TITLE', '_TEXT', 'keyWords',
                                         'post_last_line'],
                "key": ["_RELATIVEURL:do=register&act=submit AND key AND user AND email AND phone AND pwd AND pwd2",
                        "_RELATIVEURL:do=login&act=submit AND user AND pwd",
                        "_RELATIVEURL:do=project&act=create_submit AND token AND title AND description",
                        "_RELATIVEURL:do=project&act=setcode_submit AND token AND id AND ty AND setkey_1_keepsession AND modules AND setkey_15_info AND code",
                        "_RELATIVEURL:do=project&act=delcontent&r AND id AND token",
                        "_RELATIVEURL:index/user/doregister.html AND __token__ AND invitecode AND username AND email AND password AND password2",
                        "_RELATIVEURL:index/user/dologin.html AND username AND password"]
            },
            "sf": {
                "col": self.all_items + ['USERNAME', 'PASSWORD', 'Pay_Callbackurl'],
                "key": ["pay_callbackurl"]
            },
            "vps": {
                "col": self.all_items + ['USERNAME', 'PASSWORD', '_MAINFILE', '_QUERY_CONTENT', 'keyWords',
                                         'post_last_line'],
                "key": ["_HOST:sunnet365.com", "_HOST:chm666.com", "_HOST:www.zhekou5.com", "_HOST:www.tiebavps.com", "_HOST:www.lantuvps.com", "_HOST:www.cbvpa.com", "_HOST:7sensen.com", "_HOST:www.idc789.com", "_HOST:e8088.com", "_HOST:scvps.cn", "_HOST:yh168.com", "_HOST:plaidc.com", "_HOST:maini168.com", "_HOST:wanbianyun.com", "_HOST:09vps.com", "_HOST:hunbovps.com", "_HOST:lsjvps.com", "_HOST:zhimaruanjian.com", "_HOST:xiziyun.cn", "_HOST:taiyangruanjian.com", "_HOST:idcbest.com", "_HOST:zu029.com", "_HOST:qgvps.com", "_HOST:988vps.com" "_HOST:jwzjs.com", "_HOST:aahl.cn", "_HOST:nuobin.com", "_HOST:0086soft.com", "_HOST:029wz.cn", "_HOST:0455xx.cn", "_HOST:0592wap.com", "_HOST:0731js.com", "_HOST:07yue.com", "_HOST:0dxf.com", "_HOST:0host.cn", "_HOST:10000.wang", "_HOST:15-8.com", "_HOST:1600.top", "_HOST:18635690887.236.vip", "_HOST:2009o.com", "_HOST:222.cc", "_HOST:263vps.com", "_HOST:30vps.com", "_HOST:361d.com", "_HOST:400.cx", "_HOST:465vps.com", "_HOST:48010.com", "_HOST:5009.cn", "_HOST:50m.xyz", "_HOST:580ck.com", "_HOST:58me.cn", "_HOST:598yun.com", "_HOST:5gwl.net", "_HOST:5jwl.com", "_HOST:5jwl.net", "_HOST:6bei.cn", "_HOST:74dns.com", "_HOST:75dns.com", "_HOST:77744.cn", "_HOST:800ip.itaoyun.com", "_HOST:888shuiwu.cn", "_HOST:9180188.com", "_HOST:91vps.com", "_HOST:93cloud.cn", "_HOST:9475.cn", "_HOST:9vps.com", "_HOST:a.hooniy.com", "_HOST:agentdemo.west263.com", "_HOST:aowutech.com", "_HOST:aqk1.axc3529.com", "_HOST:baihaow.cn", "_HOST:baikewa.com", "_HOST:bashangbaxia.com", "_HOST:beijiacloud.com", "_HOST:bjiis.cn", "_HOST:bolead.com", "_HOST:bumou.cn", "_HOST:caisanyun.com", "_HOST:cbvps.com", "_HOST:cchxh.wennakeji.com", "_HOST:cdhrkj.net", "_HOST:chanpin.5zai.com", "_HOST:chenxunyun.com", "_HOST:chinafu.com", "_HOST:chitutu.net", "_HOST:ckuai.com", "_HOST:cloud.bxs114.com", "_HOST:cloud.jinyaoyunwei.com", "_HOST:cloud.jvai5g.com", "_HOST:cloud.lanisky.cn", "_HOST:861.cn", "_HOST:cnkuai.com", "_HOST:control.sudu.cn", "_HOST:crnitc.com", "_HOST:csjue.com", "_HOST:cztweb.cn", "_HOST:dghuiyan.com", "_HOST:diyavps.com", "_HOST:dlbyit.cn", "_HOST:dns.tianbaosoft.com", "_HOST:dns021.com", "_HOST:dongnet.net", "_HOST:dovesm.com", "_HOST:dywk.net", "_HOST:elline.cn", "_HOST:fenghost.la", "_HOST:fowufo.com", "_HOST:ftpniy.com", "_HOST:guajibao.club", "_HOST:guajibao.cn", "_HOST:gwdip.com", "_HOST:gysite.com", "_HOST:gyzhstar.net", "_HOST:haizhao.net", "_HOST:hbsnjzj.com", "_HOST:hefei.host", "_HOST:hooniy.com.cn", "_HOST:hooniy.net.cn", "_HOST:host.themepark.com.cn", "_HOST:host.xj114.net", "_HOST:host.yaxinet.com", "_HOST:host263.com", "_HOST:ht.0718idc.com", "_HOST:huazhisoft.com", "_HOST:huhayun.com", "_HOST:huxinzhuji.com", "_HOST:hx99.cn", "_HOST:i.vpssj.net", "_HOST:i.whalehost.cn", "_HOST:i5yf.com", "_HOST:idc.023nic.com.cn", "_HOST:idc.4435.cn", "_HOST:idc.50xx.cn", "_HOST:idc.8366.cn", "_HOST:idc.90data.com", "_HOST:idc.aikuirui.com", "_HOST:idc.anzhixin.com", "_HOST:idc.cdxp.cn", "_HOST:idc.chinanet.asia", "_HOST:idc.dns192.com", "_HOST:idc.haoming.wang", "_HOST:idc.huyunkeji.cn", "_HOST:idc.lations.cn", "_HOST:idc.leidianyun.cn", "_HOST:idc.leiue.com", "_HOST:idc.orangeapp.cn", "_HOST:idc.p222000.cn", "_HOST:idc.phpwc.cn", "_HOST:idc.qixidc.cn", "_HOST:idc.tsnb.xyz", "_HOST:idc.tuiedu.com", "_HOST:idc.weiewl.com", "_HOST:idc.ynqunju.com", "_HOST:idc.yqd518.com", "_HOST:idc.yunlng.com", "_HOST:idc.zhirui.net", "_HOST:idc121.com", "_HOST:idcgo.net", "_HOST:idckc.com", "_HOST:iitt.cc", "_HOST:intvps.com", "_HOST:itguozi.cn", "_HOST:jiaoliu.xin", "_HOST:juli86.pc51.com", "_HOST:junchengidc.com", "_HOST:jw.nbw114.com", "_HOST:jzvps.net", "_HOST:kbtsj.org", "_HOST:kefucn.com", "_HOST:kmidc.com.cn", "_HOST:kuaiyunidc.cn", "_HOST:kujz.cn", "_HOST:kx.net.cn", "_HOST:kzmb.wovps.com", "_HOST:lc.bdyvps.com", "_HOST:lc.ygvps.com", "_HOST:leidianvps.com", "_HOST:lewan100.com", "_HOST:leyundns.cn", "_HOST:longtengkj.cn", "_HOST:lr163.com", "_HOST:lvtuyun.com", "_HOST:lxxynet.cn", "_HOST:mayivps.com", "_HOST:mingxintong.com", "_HOST:mx-idc.com", "_HOST:my.locvps.net", "_HOST:niuhulian.com", "_HOST:ntqsjj.com", "_HOST:product.yuwangjiehe.com", "_HOST:qimaiit.cn", "_HOST:quanyue.net", "_HOST:qusou.3007.cn", "_HOST:ruoxinet.com", "_HOST:s.kin168.top", "_HOST:sanxia114.com", "_HOST:shengxuezhiyuan.com", "_HOST:shweb360.com", "_HOST:sudu.cn", "_HOST:shusen.cc", "_HOST:sumdns.cn", "_HOST:szwz.com.cn", "_HOST:tiandi123.com", "_HOST:tiankeyun.com", "_HOST:uedcs.com", "_HOST:urkeji.com", "_HOST:v.fafa9.com", "_HOST:vip.niuqikeji.com", "_HOST:vip.qqcm.net", "_HOST:vps.17s.cn", "_HOST:vps.cn.com", "_HOST:vps.duoip.cn", "_HOST:vps.iiskj.com", "_HOST:vps.ipv4.top", "_HOST:wealthsat.com", "_HOST:web.cdyht.com", "_HOST:web.ooo", "_HOST:web.veisun.net", "_HOST:west.top", "_HOST:west.youxia.org", "_HOST:west169.cn", "_HOST:west800.com", "_HOST:winisp.com", "_HOST:wlsun.com", "_HOST:wovps.com", "_HOST:woyw.com", "_HOST:wufan.net", "_HOST:ww.httu.cn", "_HOST:1417vps.com", "_HOST:168vps.cn", "_HOST:187.net.cn", "_HOST:189vps.com", "_HOST:18inter.com", "_HOST:258.hk", "_HOST:50zx.com", "_HOST:518vps.com", "_HOST:657e.com", "_HOST:668vps.com", "_HOST:74yun.com", "_HOST:91space.com", "_HOST:99bgp.com", "_HOST:adslvps.com", "_HOST:akuyun.cn", "_HOST:aw7.cn", "_HOST:ayundns.cn", "_HOST:bacaiyun.com", "_HOST:bnnd.net", "_HOST:boluoyun.com", "_HOST:cdnnode.com", "_HOST:d5hl.com", "_HOST:dafayun.cn", "_HOST:dedeidc.com", "_HOST:dnssu.com", "_HOST:enic.co", "_HOST:forisp.com", "_HOST:haihuizhuji.com", "_HOST:heixia.net", "_HOST:hubeidc.com", "_HOST:hubeishuju.com", "_HOST:hxhost.net", "_HOST:idc-gz.com", "_HOST:idc86.cn", "_HOST:idcbaidu.com", "_HOST:idcbs.com", "_HOST:isyidc.com", "_HOST:jinghost.com", "_HOST:jixiangnet.com", "_HOST:jsatkj.com", "_HOST:kaierwei.cn", "_HOST:lanmicloud.com", "_HOST:lqvps.com", "_HOST:maihui5.com", "_HOST:newercom.com", "_HOST:openzy.com", "_HOST:pppoevps.com", "_HOST:saiaosi.net", "_HOST:shenggaocloud.com", "_HOST:south6.cn", "_HOST:tfkc.cn", "_HOST:vpspptp.com", "_HOST:west68.cn", "_HOST:xibei.cc", "_HOST:youhuaruanjian.com", "_HOST:yswlcm.com.cn", "_HOST:yujiaidc.com", "_HOST:yy286.net", "_HOST:yyyvps.cn", "_HOST:zancms.com", "_HOST:zhijunet.com", "_HOST:zhvps.com", "_HOST:zzhidc.com", "_HOST:wz713.com", "_HOST:xibushuju.com", "_HOST:xiguaip.com", "_HOST:xihw.cn", "_HOST:xinmaihulian.com", "_HOST:xishuidc.com", "_HOST:xnhl.net", "_HOST:xpkj.net", "_HOST:xridc.zzcmol.com", "_HOST:xyun.lzxbwl.com", "_HOST:y.centrun.com", "_HOST:ycnet88.cn", "_HOST:ygvps.com", "_HOST:ym.78idc.cn", "_HOST:ymjq.cn", "_HOST:yuminews.com", "_HOST:yun.deyinshiye.com", "_HOST:yun.whyjzy.com", "_HOST:yun.zzzg.cc", "_HOST:yunhk.net", "_HOST:yunlifang.cn", "_HOST:yunlingidc.com", "_HOST:yuntuzhiwang.com", "_HOST:yxen.net", "_HOST:zhou7.com", "_HOST:zhuji.029wangzhan.com", "_HOST:zzyzwl.cn", "_HOST:360vps.cn", "_HOST:miandns.com", "_HOST:xunheng.com", "_HOST:jidcy.cn", "_HOST:54ak.com", "_HOST:186yun.com", "_HOST:asiyun.cn", "_HOST:duoduovps.cn", "_HOST:150cn.com", "_HOST:39.100.66.108", "_HOST:fwvps.com", "_HOST:aspcms.com", "_HOST:91soyun.com", "_HOST:vpsadm.yyyip.cn", "_HOST:lqvps.com", "_HOST:yqserver.com", "_HOST:263vps.com", "_HOST:dorlor.com", "_HOST:61vps.cn", "_HOST:007vps.com", "_HOST:keenyun.cn", "_HOST:pldip.cn", "_HOST:jzvps.net"]
            },
            "qg": {
                "col": self.all_items + ['USERNAME', 'PASSWORD', '_MAINFILE', '_QUERY_CONTENT', '_QUERY_MATCHTERMS',
                                         'p_num', 'keyWords'],
                "key": ["contactNum AND contactName", "contactphone AND contactname", "contactPhone AND contactName",
                        "contact_phone AND contact_name", "contactMobile AND contactName",
                        "ContactMobile AND ContactName", "contactPhoneList AND contactName", "mobile AND name",
                        "mobile AND contactName", "number AND name", "phone AND name", "telNums AND lastName",
                        "phoneNumbers AND contactName", "phoneList AND contactName", "/api/uploads/api data",
                        "cee_mobile AND cee_name", "tel AND name", "address AND body AND date",
                        "address AND body AND smsTime", "mobile AND content AND send_time",
                        "mobile AND sms_body AND sms_time", "other AND body AND time AND phone",
                        "peer_number AND content AND sms_time AND user_mobile", "phone AND content AND date",
                        "phone AND content AND dialtime", "phone AND messageContent AND date",
                        "phone AND messageContent AND messageDate", "phone AND text AND date"]
            }
        }

    def queryclient(self, keyWords, queryType):
        batch = []
        cont_flag = False
        cont_end_flag = True
        content = ''
        end_item = '_QUERY_MATCHTERMS'

        cmd = [
            '/home/search/sbin/queryclient',
            '--server', '127.0.0.1',
            '--port', '9870',
            '--querystring', '"{0}"'.format(keyWords),
            '--start', self.startTime,
            '--end', self.endTime,
            '--contextlen', '10000',
            '--maxcount', '1000000'
        ]
        req = Popen(". /home/search/search_profile && {0}".format(" ".join(cmd)), shell=True, stdout=PIPE)
        print
        ". /home/search/search_profile && {0}".format(" ".join(cmd))
        self.LOGGER.info('QUERYTIME:{0}_{1} {2} ...wait...'.format(self.startTime, self.endTime, keyWords))

        for line in req.stdout:
            line = line.replace('\x1a', '')
            if line == '\n' and cont_flag and cont_end_flag:
                cont_flag = False
                if batch != []:
                    data = dict(batch)
                    content = data.get('_QUERY_CONTENT')
                    data['_QUERY_CONTENT'] = re.sub('[\n&; \x1a]', '', content)
                    yield data
                    batch = []
            elif ':' in line and (cont_end_flag or '_QUERY_MATCHTERMS' in line):
                k, v = map(lambda x: x.strip(), line.split(':', 1))
                if k == end_item:
                    # cont_end_flag = True
                    batch.append(('_QUERY_CONTENT', content))
                    content = ''
                    cont_end_flag = True
                elif k == '_QUERY_CONTENT':
                    cont_flag = True
                    cont_end_flag = False
                    content = v + ';'
                else:
                    batch.append((k, v))
            else:
                if not cont_end_flag:
                    content += line + ';'

    def re_verify(self, text):
        # p = Phone()
        try:
            # 手机号
            userphone = re.finditer(r"\D(13\d|14[5|7]|15\d|166|17[3|6|7]|18\d)\d{8}\D", text)
            # 验证
            if userphone:
                s = set()
                for m in userphone:
                    phone = m.group()[1:-1]
                    # if p.find(phone):
                    s = s | set([phone])
                return len(s)
            else:
                return 0
        except:
            return 0

    def get_phone(self, info):
        tel = ""
        if info and 'username' in info:
            r = re.findall('username":"([^"]+)"}', info)
            a = "".join(r)
            if len(a) == 11:
                tel = a
        return tel

    def ext_form_msg(self, content, mimetypes):
        """
        提取上传的form表单的变量名
        @param content:报文数据
        @param res_dic:结果字典
        @return:关键词列表
        """

        # 争对三种常见的POST提交数据方式，分别进行处
        # if 'application/x-www-form-urlencoded' in mimetypes:
        last_line = content.replace('&&', '&').strip('...')
        # 判断是不是json格式
        print
        "last_line-->", last_line
        if last_line.split('=')[0] == 'data' or last_line.split('=')[0] == 'submit_params':
            dic_keys = json.loads(last_line.split('=')[1])
            # print 11111,dic_keys
            return dic_keys.get('pay_callbackurl', '')
        else:
            kv = [x.split('=') for x in last_line.split('&')]
            # print 22222,kv
            for x in kv:
                if x[0] == 'pay_callbackurl':
                    return x[1]

    def ext_mainfile(self, file_url):
        try:
            req = urllib2.Request(file_url)
            response = urllib2.urlopen(req, timeout=2)
            content = response.read().decode()
            pload = content.split('\n')[-1].replace('\t', '').replace('\r', '').replace('\n', '').strip()
            pload = urllib2.unquote(pload)
            if len(pload) > 0:
                return pload.strip()

        except Exception, e:
            return ''

    def same_start_end(self, s):
        n = len(s)  # 整个字符串长度
        j = 0  # 前缀匹配指向
        i = 1  # 后缀匹配指向
        result_list = [0] * n
        while i < n:
            if j == 0 and s[j] != s[i]:  # 比较不相等并且此时比较的已经是第一个字符了
                result_list[i] = 0  # 值为０
                i += 1  # 向后移动
            elif s[j] != s[i] and j != 0:  # 比较不相等,将j值设置为ｊ前一位的result_list中的值，为了在之前匹配到的子串中找到最长相同前后缀
                j = result_list[j - 1]
            elif s[j] == s[i]:  # 相等则继续比较
                result_list[i] = j + 1
                j = j + 1
                i = i + 1
        return result_list

    def kmp(self, s, p):
        """kmp算法,s是字符串，p是模式字符串，返回值为匹配到的第一个字符串的第一个字符的索引，没匹配到返回-1"""
        s_length = len(s)
        p_length = len(p)
        l = []
        i = 0  # 指向s
        j = 0  # 指向p
        next = self.same_start_end(p)
        while i < s_length:
            if s[i] == p[j]:  # 对应字符相同
                i += 1
                j += 1
                if j >= p_length:  # 完全匹配
                    return i - p_length
            elif s[i] != p[j]:  # 不相同
                if j == 0:  # 与模式比较的是模式的第一个字符
                    i += 1
                else:  # 取模式当前字符之前最长相同前后缀的前缀的后一个字符继续比较
                    j = next[j]
        if i == s_length:  # 没有找到完全匹配的子串
            return -1

    def deal(self, data, model):
        if model == "bt":
            if data['_RELATIVEURL'] != "/login":
                return {}
            content = data.get('_QUERY_CONTENT', '').replace("\r", "")
            cookie = data.get('_COOKIE', '')

            if "username=" in content and "password=" in content:
                a = re.compile(r'login...username=(.*?)password=(.*?)code=', re.S)
                result = a.findall(content)
                if len(result) > 0 and len(list(result[0])) == 2:
                    data['USERNAME'] = list(result[0])[0]
                    data['PASSWORD'] = list(result[0])[1]

            cookie_dict = {}
            for c in cookie.split(";"):
                c_list = c.strip().split("=")
                if len(c_list) != 2:
                    continue
                key = c_list[0]
                value = c_list[1]
                if key not in cookie_dict:
                    cookie_dict[key] = value

            ls = ["Path", "sites_path", "serverType", "distribution", "memSize", "backup_path"]
            for j in ls:
                data[j] = cookie_dict.get(j, "")
            data['new_sites_path'] = "-100"

            if data['sites_path']:
                data["sites_path"] = data["Path"].replace(data["sites_path"], "")
            # print(type(data))
            # print("length:",len(data))
            # print(data)
            # print(data['sites_path'])

            p = '/'
            s = data['sites_path']
            t = s
            l = []
            flag = 0
            while s:
                if self.kmp(s, p) == -1:
                    break
                else:
                    n = self.kmp(s, p)
                    flag += n + len(p)
                    # print(s)
                    s = s[n + len(p):]
                    l.append(flag - len(p))

            if (len(l) == 1):
                data['new_sites_path'] = t
            if (len(l) >= 2):
                data['new_sites_path'] = t[0:l[1]]

            if ("." not in data['new_sites_path']):
                data['new_sites_path'] = "404"

            zh_pattern = re.compile(u'[\u4e00-\u9fa5]+')
            data['new_sites_path'] = data['new_sites_path'].decode(encoding='utf-8')
            match = zh_pattern.search(data['new_sites_path'])
            if match:
                data['new_sites_path'] = "404"

            if data['new_sites_path'] == "404":
                return {}

            if '/' in data['new_sites_path']:
                data['new_sites_path'] = data["new_sites_path"].replace('/', "")

            data["Phone"] = self.get_phone(cookie_dict.get("bt_user_info", ""))

        if model == "sf":
            pay_callbackurl = self.ext_form_msg(data.get('_QUERY_CONTENT',''), data.get('_MIMETYPES',''))
            if pay_callbackurl:
                data['Pay_Callbackurl'] = pay_callbackurl
        if model == "qg":
            Flist = ['hujing-public.oss-cn-beijing.aliyuncs.com', 'img-weimao.oss-cn-shanghai.aliyuncs.com', 'wap.js.10086.cn', 'oss.suning.com', 'ossup.suning.com', 'm.client.10010.com', 'c.pcs.baidu.com', 'pcs.baidu.com', 'c.tieba.baidu.com', 'www.spider.58.com', 'dms-sales.baonengmotor.com']
            if (data.get('_HOST', '') not in Flist) and ('13' or '145' or'147' or '15' or '166' or '173' or '176' or '177' or '18' in data.get('_QUERY_CONTENT', '')):
                tmp = data.get('_QUERY_CONTENT', '')
                p_num = self.re_verify(tmp)
                if p_num >= 2:
                    data['p_num'] = str(p_num)
        if model == "vps" or model == "ddos" or model == "xss":
            post_last_line= self.ext_mainfile(data.get('_MAINFILE',''))
            if post_last_line:
                data['post_last_line'] = post_last_line

        return data

    def run(self, model):
        kwl = self.model_key_dict[model]["key"]
        items = self.model_key_dict[model]["col"]
        out_file = model + '_out.txt'

        with open(os.path.join(self.data_path, out_file), 'a+') as f:
            f.write('\t'.join(items) + '\n')
            for KEY_WORDS in kwl:
                if model == "vps":
                    KEY_WORDS = KEY_WORDS + " AND (_TEXT:u_name OR _TEXT:username OR _TEXT:userid OR _TEXT:adminusername OR _TEXT:login OR _TEXT:txtname OR _TEXT:uname OR _TEXT:swapname OR _TEXT:email OR _TEXT:loginname OR _TEXT:user_name OR _TEXT:usrname OR _TEXT:phone OR _TEXT:identification OR _TEXT:login_user OR _TEXT:user) AND (_TEXT:u_password OR _TEXT:userpass OR _TEXT:password OR _TEXT:adminuserpwd OR _TEXT:txtpass OR _TEXT:passwd OR _TEXT:ps OR _TEXT:swappass OR _TEXT:loginpwd OR _TEXT:user_pass OR _TEXT:usrpass OR _TEXT:login_pass OR _TEXT:pass)"
                try:
                    for data in self.queryclient(KEY_WORDS, model):
                        data['keyWords'] = KEY_WORDS
                        data = self.deal(data, model)
                        if not data:
                            continue
                        if model == "qg" and data.get("p_num", '').replace("\r", "").replace("\t", "") == '':
                            continue
                        f.write('\t'.join(
                            [data.get(item, '').replace("\r", "").replace("\t", "") for item in items]) + '\n')
                except Exception, e:
                    self.LOGGER.info('QUERY_ERROR-{0}'.format(e))

                ##日志文件打印


def init_logger(logname, filename, logger_level=logging.INFO):
    logger = logging.getLogger(logname)
    logger.setLevel(logger_level)
    fh = logging.FileHandler(filename)
    fh.setLevel(logging.DEBUG)
    ch = logging.StreamHandler()
    ch.setLevel(logging.DEBUG)
    formatter = logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s')
    fh.setFormatter(formatter)
    ch.setFormatter(formatter)
    logger.addHandler(fh)
    logger.addHandler(ch)
    return logger


def zip_result(DATA_PATH, ZIP_PATH, LOGGER):
    pipe = Popen(['tar', '-zcvf', ZIP_PATH, DATA_PATH[2:], '--remove-files'], stdout=PIPE, stderr=PIPE)
    out, err = pipe.communicate()
    if pipe.returncode:
        LOGGER.warning("Compress dirs failed with error code: {0}".format(pipe.returncode))
        LOGGER.warning(err.decode())
    else:
        LOGGER.info("Compress dirs success!.")


def init_path(path):
    if not os.path.exists(path):
        os.mkdir(path)


def main():
    for model in model_list:
        DATA_PATH = './_queryResult_' + model + '_'
        init_path(DATA_PATH)
        LOGGER = init_logger('queryclient_logger', os.path.join(DATA_PATH, 'running.log'))
        LOGGER.info('START ' + model + ' QUERY BATCH....')
        ap = Airport(DATA_PATH, startTime, endTime, LOGGER)
        ap.run(model)
        LOGGER.info('END ' + model + ' QUERY BATCH')

        overTime = datetime.datetime.now()
        ZIP_PATH = DATA_PATH + NowTime.strftime("%Y%m%d%H%M%S") + '_' + overTime.strftime("%Y%m%d%H%M%S") + '.tgz.tmp'
        zip_result(DATA_PATH, ZIP_PATH, LOGGER)
        ZIP_SUCCEED = areacode + ZIP_PATH[2:].replace('.tmp', '')
        os.rename(ZIP_PATH, ZIP_SUCCEED)


if __name__ == '__main__':

    usage = 'python bathquery_db_password.py --start [start_time] --end [end_time] --areacode [areacode] --model [modelType]'
    dataformat = '<time>: yyyyMMddhhmmss eg:20180901000000'
    areaformat = '<areacode> xxxxxx eg:530000'
    modelformat = '<modelType> eg:bt'

    parser = OptionParser(usage)
    parser.add_option('--start', dest='startTime', help=dataformat)
    parser.add_option('--end', dest='endTime', help=dataformat)
    parser.add_option('--areacode', dest='areacode', help=areaformat)
    parser.add_option('--model', dest='modelType', help=modelformat)
    option, args = parser.parse_args()

    modelType = option.modelType
    startTime = option.startTime
    endTime = option.endTime
    areacode = option.areacode
    NowTime = datetime.datetime.now()
    OneYear = datetime.timedelta(days=90)
    defaultStart = (NowTime - OneYear).strftime("%Y%m%d%H%M%S")
    defaultEnd = NowTime.strftime("%Y%m%d%H%M%S")

    if (startTime is None and endTime is None) or len(startTime) != 14 or len(endTime) != 14:
        startTime = defaultStart
        endTime = defaultEnd
    if areacode is None or len(areacode) != 6:
        areacode = '000000'

    model_list = ['bt', 'hack', 'ddos', 'apk', 'xss', 'sf', 'vps', 'qg']
    if modelType is None or modelType not in model_list:
        pass
    else:
        model_list = [modelType]

    main()
