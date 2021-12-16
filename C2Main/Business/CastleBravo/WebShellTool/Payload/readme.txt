作者：lxf
时间：2021/12/14

(1)后信息各功能模块实际payload存储位置为：

  境外服务器：103.43.17.9
  存储路径：
            1) D:\phpstudy_pro\WWW\wk
            2) D:\myjob\phpstudy\PHPTutorial\WWW\wk
            该服务器安装了两个phpstudy，为了保险，两个目录都放了payload文件
            当前实际使用第一个
  各payload文件对应后信息功能模块说明：  
            1)cmd.gif  系统信息/进程列表/定时任务
            2)conf.gif mysql探针
            3)db_dict  K令爆破使用的字典
            4)sysinfo.gif  系统信息
(2) payload文件修改方式
    以修改mysql探针的conf.gif文件为例：
	1) 解压代码到当前路径下unzip_result.txt
	   D:\work\SG\phpStudy\PHPTutorial\php\php-5.4.45\php.exe unzip.php conf.gif
	2) 修改unzip_result.txt代码
	3）重新压缩代码到conf.gif
	   D:\work\SG\phpStudy\PHPTutorial\php\php-5.4.45\php.exe gzip.php unzip_result.txt conf.gif
    