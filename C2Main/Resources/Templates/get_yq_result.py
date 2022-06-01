import urllib.request
import urllib.parse
import json
import os
import time
import argparse


def get_yq_result(data_list):
    id = data_list[1]
    result_path = data_list[3]
    dict = json.dumps({"rule_id": id})
    url = "http://47.94.39.209:22222/api/yq/get_message"
    headers = {"Content-Type": 'application/json'}
    post_data = bytes(dict, 'utf8')
    req = urllib.request.Request(url, data=post_data, headers=headers)
    response = urllib.request.urlopen(req).read().decode('utf-8')
    result = json.loads(response)
    if result.get("status", "fail") != "success" or result.get("data", "") == "":
        return data_list
    col_list = ["taskurl", "url", "title", "datasourcetype", "userid", "nickname", "postfloor", "revertcount",
              "heat", "mainareacode", "netimgurl", "imgurl", "posttime", "domain", "domainname", "forumname",
              "context", "isforward", "forwardnum", "likenum", "commentnum", "fannum", "followernum", "blognum",
              "readnum", "verify", "address", "addresscode", "imagepath", "domaintype", "classifyid", "classifyscore",
              "commentsign", "sensitivity", "areacode", "tagarea", "signcode", "eventcode", "tagevent"]
    info_list = result["data"].strip("\n").split("\n")
    data_list[1] = data_list[1] + str(len(info_list))
    for info in info_list:
        result_list = []
        col_info = json.loads(info)["articleinfo"]
        for col in col_list:
            if col == "datasourcetype" and col_info.get(col, "") != "":
                col_info[col] = source_dict[col_info[col]]
            elif col == "classifyid" and col_info.get(col, "") != "":
                col_info[col] = classify_dict[col_info[col][0]]
            result_list.append(str(col_info.get(col, "")))
        write_result(result_list, result_path)
    return data_list


def write_result(result_list, path):
    with open(path, "a", encoding='utf8', errors='ignore') as f:
        f.write("\t".join(result_list) + '\n')


def write_info(info_list, path):
    with open(path, "w", encoding='utf8', errors='ignore') as f:
        f.write("\n".join(info_list))


def read_file(path):
    result_list = []
    if not os.path.exists(path):
        return result_list
    with open(path, "r", encoding='utf8', errors='ignore') as f:
        result_list = f.readlines()
    return result_list


def main():
    header_list = ["采集任务url", "文章url", "文章标题", "数据源标识", "用户userid", "发表人昵称", "发表楼层", "回复数",
                     "点赞数、热度值", "主线地区", "图片实际网页地址", "图片短串", "发表时间", "网站域名", "网站名称",
                     "板块名称", "文章正文", "是否转发", "转发数", "点赞数", "评论数", "粉丝数", "关注者数", "发表文章数",
                     "阅读数", "是否为官方认证", "注册地址", "注册地地区编号", "头像链接", "境内外标识", "文章所属分类",
                     "文章所属分类得分", "正负面标识", "文章敏感度", "是否为噪音标识", "文章命中地区编码", "文章命中地区",
                     "行业情感正负面", "行业id", "行业说明"]

    # rule_id_list = ["26165356897133", "2616535689712"]

    for i in range(0, 12):
        content_list = read_file(file_path)
        update_content = []
        for content in content_list:
            data_list = content.strip("\n").split("\t")
            if len(data_list) != 4:
                update_content.append(content)
                continue
            #if i == 0:
            #    write_result(header_list, data_list[3])
            content = "\t".join(get_yq_result(data_list))
            update_content.append(content)
        write_info(update_content, file_path)
        time.sleep(60)


if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument('--f', type=str, default=None)
    args = parser.parse_args()
    # file_path = "C:\\FiberHomeIAOModelDocument\\IAO\\侦察兵\\舆情侦察兵\\微博任务0526_1653568971\\261653568970_info.txt"
    file_path = args.f
    source_dict = {"1": "新闻", "2": "报刊", "4": "新闻APP", "8": "Facebook", "16": "论坛", "32": "Twitter",
                   "64": "贴吧", "256": "博客", "65536": "视频", "1048576": "微博", "268435456": "微信",
                   "16384": "今日头条", "32768": "知乎", "131072": "短视频"}
    classify_dict = {
        "0X00000000": "未知分类", "0X00000001": "旅游", "0X00000002": "游戏", "0X00000003": "人物", "0X00000004": "体育",
        "0X00000005": "音乐", "0X00000006": "影视", "0X00000007": "软件", "0X00000008": "文学", "0X00000009": "美食",
        "0X0000000A": "健康", "0X0000000B": "医药", "0X0000000C": "商铺", "0X0000000D": "财经", "0X0000000E": "汽车",
        "0X0000000F": "房产", "0X00000010": "动漫", "0X00000011": "教育学校、学科、考试，培训等", "0X00000012": "科技",
        "0X00000013": "军事", "0X00000014": "天气", "0X00000015": "情感", "0X00000016": "广告", "0X00000017": "群体聚集",
        "0X00000018": "自然灾害", "0X00000019": "交通事故", "0X0000001A": "暴力执法", "0X0000001B": "刑事犯罪",
        "0X0000001C": "求职招聘", "0X0000001D": "食品安全", "0X0000001E": "环境污染", "0X0000001F": "疾病疫情",
        "0X00000020": "金融安全", "0X00000021": "敏感政治", "0X00000022": "贪腐", "0X00000023": "非法组织",
        "0X00000024": "反动言论", "0X00000025": "先进事迹", "0X00000026": "领导人活动", "0X00000027": "政策方针",
        "0X00000028": "心灵鸡汤", "0X00000029": "其他社会类", "0X0000002A": "其它政治类", "0x0000002B": "色情广告/推广",
        "0x0000002C": "色情从业", "0x0000002D": "色情文学", "0x0000002E": "广告子类营销类", "0x0000002F": "幽默搞笑",
        "0x00000030": "敏感政治中六四", "0x00000031": "敏感政治中领导人", "0x00000032": "敏感政治中意识形态",
        "0x00000033": "色情资源", "0x00000034": "违法犯罪之网络黑产", "0x00000035": "违法犯罪之毒品",
        "0x00000037": "娱乐明星", "0x00000038": "星座", "0x00000039": "亲子", "0x0000003A": "女性", "0x0000003B": "招聘广告",
        "0x0000003C": "宗教", "0x0000003D": "文化", "0x0000003E": "环球", "0x0000003F": "宠物", "0x00000040": "互联网",
        "0x00000041": "数码", "0x00000042": "手机", "0x00000043": "软件", "0x00000044": "网络安全", "0x00000045": "科学",
        "0x00000046": "智能化", "0x00000047": "股票", "0x00000048": "期货", "0x00000049": "理财", "0x0000004A": "互联网金融",
        "0x0000004B": "外汇", "0x0000004C": "投资", "0x0000004D": "基金", "0x0000004E": "宏观经济", "0x0000004F": "债券",
        "0x00000050": "银行", "0x00000051": "保险", "0x00000052": "电影", "0x00000053": "电视剧", "0x00000054": "综艺节目",
        "0x00000055": "戏剧", "0x00000056": "篮球", "0x00000057": "足球", "0x00000058": "乒乓球", "0x00000059": "羽毛球",
        "0x0000005A": "排球", "0x0000005B": "游泳", "0x0000005C": "健身", "0x0000005D": "高尔夫", "0x0000005E": "田径",
        "0x0000005F": "历史", "0x00000036": "违法犯罪之赌博", "0X0000FFFF": "其他"}
    try:
        main()
    except Exception as ex:
        print(ex)

