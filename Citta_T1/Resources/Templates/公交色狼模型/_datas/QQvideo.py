import pandas as pd
import os

'''
查询群聊
get_groupchat(path, 7, time_today, 'qq')
'''
import re
from datetime import datetime, timedelta
import shutil
import subprocess
time_today = datetime.today().strftime('%Y%m%d')
def get_groupchat(file,  diffday, endday, Type):
    '''
    param:searchfile_path,  diffday, endday, Type
    return: path_groupchat, file_groupchat
            query_file:['USERID:微信ID','APPID:微信账号','GROUPID:微信群号','QQNUM:QQ号','GROUPCODE:QQ群号','IPAREAID:地区编码','SENDAPPID:敏感私聊发送者ID','RECEIVERAPPID:敏感私聊接收者ID','STRMESSAGE:敏感私聊接内容']
    '''
    endtime = datetime.strptime(endday+'235959','%Y%m%d%H%M%S')
    starttime = (endtime - timedelta(days=int(diffday), hours=23, minutes=59, seconds=59))
    starttime = starttime.strftime('%Y%m%d%H%M%S')
    endtime = endtime.strftime('%Y%m%d%H%M%S')
    outstring = "bash /DATA/libraries/batch_query_task.sh {starttime} {endtime} {file} {Type} ".format( \
            starttime = starttime, 
            endtime = endtime, 
            file = file,
            Type = Type)
    print('\nStart searhing groupchat ...', '\n', outstring)
    subprocess.call(outstring, shell=True)
    res_path = file + '_' + starttime + '_to_' + endtime + '_' + Type + '.txt'
    shutil.rmtree(re.sub('\.txt', '/', res_path))
    print('The GroupChat path: ' + res_path + '\nsearch GroupChat complete!\n' + '-'*30)
    return res_path

'''
读取群聊文件，并作预处理
'''
def Load_data(Path, apptype):
    '''
    param: groupchat file path
    return: dataframe
    deal: drop_duplicates、dropna、add len(content)
    '''
    df = pd.read_csv(Path, sep='\t', dtype=str, quoting=3)
    df.columns = df.columns.str.lower() #列名转小写
    df.rename({df.columns[0]:'groupid', df.columns[1]:'id'}, axis=1, inplace=True)
#     df = df[~df.content.astype(str).str.contains('^/:+[a-z]+$||jpg')]
    df.drop_duplicates(inplace=True)
    df.dropna(inplace=True)
    df['len_content'] = df.content.apply(lambda x:len(x))
    df['apptype'] = apptype
    df = df.loc[:, ['groupid', 'id', 'capture_time', 'clientip','content', 'ipareaid', 'dotip', 'position', 'len_content', '_query_matchterms', 'apptype']]
    print(Path, '\n', df.shape, '\n涉及群数：' + str(df.iloc[:,0].nunique()) + '\n' + '涉及账号数：' + str(df.iloc[:,1].nunique()) + '\n' + '-'*30)
    return df

'''
根据关键词切词处理并统计
'''
def dictkeyword(path_keyword):
    '''
    param: path
    return: dict
    deal: 获取关键词字典
    '''
    dict_keyword = {}
    with open(path_keyword, 'r') as f:
        for line in f.readlines():
            line = line.rstrip()
            if '# ' in line:
                tmpIndex = line.replace('# ','')
                dict_keyword[tmpIndex] = []
                continue
            dict_keyword[tmpIndex].append(line)
    return dict_keyword

def Keyword_Count(df, KeyColumns, contentCol):
    res = []
    keyword_res = {}
    for value, group in df.groupby(KeyColumns):
        group = group.drop_duplicates(keep='first')
        chatCnt = len(group[contentCol])
        group[contentCol] = group[contentCol].astype(str)
        content = '【' + '】【'.join(group.sort_values('capture_time', ascending=False)[contentCol]) + '】'
#         content_keyword = '【' + '】【'.join(group[group.content.str.contains(dict_to_str(dict_keyword))].sort_values('capture_time', ascending=False)[contentCol]) + '】'
        time_last  = datetime.strftime(datetime.strptime(max(group['capture_time']), '%Y%m%d%H%M%S'), '%Y-%m-%d %H:%M:%S')
        contentAvgLen = int(group[contentCol].str.len().mean())
        ipareaid = group[group.capture_time == group.capture_time.max()].ipareaid.values.astype(str)[0]
        
        dict_keyword = dictkeyword(path_keyword)  #需要更改为关键词所在路径
        for key, values in dict_keyword.items():
            keyword_res[key] = {}
            keyword_res[key]['message'] = ' '.join(sorted([word + ',' + str(content.count(word)) for word in values if word in content], key=lambda x: x.split(',')[1], reverse=True))
            if not keyword_res[key]['message']: keyword_res[key]['allcnt'], keyword_res[key]['diffcnt'] = 0,0; continue
            keyword_res[key]['allcnt'] = sum([int(i.split(',')[1]) for i in keyword_res[key]['message'].split(' ')])
            keyword_res[key]['diffcnt'] = len(keyword_res[key]['message'].split(' '))
#             keyword_res[key]['pre'] = (keyword_res[key]['allcnt'] / chatCnt) if keyword_res[key]['allcnt'] != '' else 0
        res += [[value, time_last, chatCnt, contentAvgLen, ipareaid] + \
                [j for i in [list(v.values()) for k,v in keyword_res.items()] for j in i]]
    res = pd.DataFrame(res)
    res.columns = [KeyColumns, 'time_last', 'chatCnt', 'contentAvgLen', 'ipareaid'] + [f'{k}_{v_k}' for k,v in keyword_res.items() for v_k in list(v.keys())]
#     res['level'] = res.apply(lambda x:judge_leve(x.enable_diffcnt, x.extreme_diffcnt, x.extreme_way_diffcnt), axis=1) #个人极端增加极端级别
#     res['apptype'] = apptype #增加账号类型字段
#     res['time'] = datetime.strftime(start_time, '%Y%m%d') #增加模型运行日期
    return res
	
time_today = datetime.today().strftime('%Y%m%d')

'''
QQ视频群
'''
grpchat_tmp1 = get_groupchat('./keyword_QQvideos', 30, time_today, 'qq')

df1 = Load_data(grpchat_tmp1,1030001)
group_count = df1.groupid.value_counts().to_frame().reset_index().rename({'index':'groupid', 'groupid':'count'}, axis=1).query('count > 3')

account_type1 = 'GROUPCODE'
group_count['data'] = '{}:'.format(account_type1) + group_count.groupid
group_count.loc[:,['data']].to_csv('./tmp1', index=None, sep='\t', header=None)
grpchat_qqvideos = get_groupchat('./tmp1', 30, time_today, 'qq')

df2 = Load_data(grpchat_qqvideos,1030001)
df2.to_csv('./result/result_qqvideos_groupchat.txt', index=None, sep='\t')

os.unlink(grpchat_tmp1)
os.unlink(grpchat_qqvideos)
os.unlink('./tmp1')