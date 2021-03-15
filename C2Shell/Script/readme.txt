《升级包制作》：
1、需要打包的脚本文件位置:C2Shell项目Script目录：
   rollback.bat 回滚脚本
   setup.bat    更新C2.exe脚本
2、将rollback.bat、setup.bat、C2.exe形成zip，zip命名规范software-版本号-日期.zip，例如software-2.1.3-20210310.zip
3、创建新升级功能的说明文档:zip命名规范software-版本号-日期.zip.info，例如software-2.1.3-20210310.zip.info,文件主要内容：
  版本号 zip大小 新增功能说明
  例如: 2.1.3	11M	1、新增数据大屏展示；2、新增xmind展示功能；3、新增XXXXXX 


  
  
《发布过程》:
1、zip压缩包及info说明文件存放到服务器(10.1.126.4)路径： 
  /usr/local/phx/apache-tomcat-8.5.38/webapps/C2Software/software
2、修改网页中显示的升级包信息及说明文件信息：
   /usr/local/phx/apache-tomcat-8.5.38/webapps/C2Software/index.html
    
	若原始数据包版本1.1.3，内容为：
  =======================================================
  <html>
  <head>
    <meta charset="utf-8">
    <title>Citta update package download</title>
  </head>
  <body>
    <h1>Citta update package download</h1> 
    <a href="./software/software-1.1.3-20210310.zip">software-1.1.3-20210310.zip</a><br/>
    <a href="./software/software-1.1.3-20210310.zip.info">software-1.1.3-20210310.zip.info</a><br/>
  </body>
  </html>
 ======================================================
 若新升级数据包版本2.1.3，内容为：
 =======================================================
  <html>
  <head>
    <meta charset="utf-8">
    <title>Citta update package download</title>
  </head>
  <body>
    <h1>Citta update package download</h1> 
    <a href="./software/software-2.1.3-20210310.zip">software-2.1.3-20210310.zip</a><br/>
    <a href="./software/software-2.1.3-20210310.zip.info">software-2.1.3-20210310.zip.info</a><br/>
  </body>
  </html>
 ======================================================