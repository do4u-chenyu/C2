using System.Windows.Forms;
namespace C2.Utils
{
    class HelpUtil
    {
        public static string C2HelpInfo = "分析师单兵作战装备,是历史分析工具的沉淀集合,旨在提升分析师的单兵和小团队作战能力,在独立环境下能够以一当十,快速展开各类型分析实战攻坚任务;其轻量精干携带方便,并覆了历史战例中各种成功经验,统一融合了四大专项服务入口(网站侦察兵,APK大眼睛,HI部AI服务,知识库)、52个小工具、17种WA社工ST工具和24个经典线索类技战法,包括但不限于:一系列网络安全工具、常见WS类Payload和Trojan样本、基站和WIFI查询、临战信息收集工具、加密解密工具、不可逆HASH撞库和彩虹表工具、数据转换工具、文本分析工具、互联网取证固证模块和二进制分析模块";
        public static string AvgOperatorHelpInfo = "取平均值:计算选择字段的平均值.";
        public static string MinOperatorHelpInfo = "取最小值:提取选择字段的最小值,查看相关数据信息.";
        public static string MaxOperatorHelpInfo = "取最大值:提取选择字段的最大值,查看相关数据信息.";
        public static string CollideOperatorHelpInfo = "碰撞算子:对两个数据表的选择字段进行取交集,输出两表都存在的数据.";
        public static string UnionOperatorHelpInfo  = "取并集:对两个数据表的选择字段进行合并.";
        public static string DifferOperatorHelpInfo = "取差集:比较查找左表存在而右表不存在的数据行.";
        public static string RandomOperatorHelpInfo = "随机采样:随机输出n条数据.";
        public static string FilterOperatorHelpInfo = "条件筛选:根据数值筛选条件设置查看符合条件的所在行数据.";
        public static string FreqOperatorHelpInfo = "频率算子:统计选择字段出现次数.";
        public static string SortOperatorHelpInfo = "排序算子:根据选择字段进行排序,支持数据去重.";
        public static string GroupOperatorHelpInfo  = "分组算子:根据选择字段对文本进行分组展示.";
        public static string RelateOperatorHelpInfo = "关联算子:根据选择的关联条件将两个数据表进行连接,默认左连接.";
        public static string CustomOperator1HelpInfo = "AI实践:灵活配置算子,用于各种探索和展示;一元算子,支持一个输入数据源.";
        public static string PythonOperatorHelpInfo  = "Py算子:调用自定义的第三方Python脚本完成运算.";
        public static string KeyWordOperatorHelpInfo = "关键词过滤:根据输入的关键词,对数据进行基础的关键词命中或去噪处理.";
        public static string DataFormatOperatorHelpInfo = "数据标准化:对数据进行输出列选择,顺序调整,列项重命名处理.";
        public static string CustomOperator2HelpInfo = "多源算子:灵活配置算子,用于各种探索和展示;二元算子,支持两个输入数据源.";
        public static string PreprocessingOperatorHelpInfo = "数据预处理:对数据进行去广告、去图片等操作.";
        public static string AnalysisOperator1HelpInfo = "主体分析:对数据主体进行信息抽取等操作.";
        public static string AnalysisOperator2HelpInfo = "关联分析:数据与其他数据源关联后分析，包括关键词分析等.";
        public static string UndoButtonHelpInfo = "撤销按钮:撤销当前操作,目前支持添加,删除,重命名,移动,关系添加,关系删除6种操作的撤销";
        public static string RedoButtonHelpInfo = "恢复按钮:恢复上一步的撤销操作,目前支持添加,删除,重命名,移动,关系添加,关系删除6种操作的恢复";
        public static string FormatOperatorHelpInfo = "一键排版:智能调整元素版面位置";
        public static string SaveModelButtonHelpInfo = "保存";
        public static string SaveAllButtonHelpInfo = "保存所有";
        public static string ImportModelHelpInfo = "导入本地数据文件,支持bcp,txt,csv,xls四种格式";
        public static string RemarkPictureBoxHelpInfo = "编写备注信息";
        public static string ZoomUpPictureBoxHelpInfo = "放大屏幕,支持三级缩放";
        public static string ZoomDownPictureBoxHelpInfo = "缩小屏幕,支持三级缩放";
        public static string MovePictureBoxHelpInfo = "拖动当前视野框";
        public static string FramePictureBoxHelpInfo = "框选屏幕中算子进行整体拖动";
        public static string MoreButtonHelpInfo = "首选项:配置程序的各项参数";

        public static string AttachmentWidgetHelpInfo = "附件:【{0}】支持多个附件和多种类型文档";
        public static string VedioWidgetHelpInfo = "视频:【{0}】 使用Windows自带播放器";
        public static string ChartWidgetHelpInfo = "图表:柱状图,折线图,饼图,环形图,雷达图等";
        public static string OperatorWidgetHelpInfo = "单算子:内置多种算子,支持Python脚本";
        public static string ModelWidgetHelpInfo = "多维运算:【{0}】支持各种算子的组合,支持Python脚本";
        public static string ResultWidgetHelpInfo = "运算结果:算子和多维运算后生成的结果,支持预览";
        public static string DataSourceWidgetHelpInfo = "数据源【{0}】:支持添加多个数据源,支持多种类型文档";
        public static string ExportImageHelpInfo = "导出图片:支持导出成多种格式的图片";

        public static string ReviewToolStripMenuItemInfo = "首页状态下无法预览数据，请点击左侧面板打开一个战术手册后再试";
        public static string DbInfoIsEmptyInfo = "输入信息不能为空，请检查输入后再次尝试添加信息";
        public static string DbServerInfoIsEmptyInfo = "需要一个有效的IP，请检查输入后再次尝试添加信息";
        public static string DbCannotBeConnectedInfo = "连接数据库发生错误，请检查信息后再次尝试连接";
        public static string DbQueryFailInfo = "数据查询失败，请检查信息后再次尝试连接"; 
        public static string CastStringToIntFailedInfo = "仅支持整数输入，请检查后重新输入";
        public static string DbConnectSucceeded = "连接成功";
        public static string DbConnectFailed = "连接失败";
        public static string DatabaseItemIsNull = "当前并未选择连接，请选择连接后重试";
        public static string SQLOpExecuteSucceeded = "SQL算子运算完毕";
        public static string SQLOpExecuteFailed = "SQL算子运算失败，请打开SQL算子检查语句后重试";
        public static string UnsupportedDatabase = "不支持的数据库类型";
        public static string TableFilterHelpInfo = "快速筛选:支持表名,列名,业务常用字段";
        public static string InvalidMaxNum = "提取条数输入有误，请输入整数类型，不支持浮点数、非数字符号等";

        public static string ApkToolFormHelpInfo = "对Apk进行反编译并获取Apk的图标，安装名称，包名，入口函数名和文件大小，解析结果右键导出xls";
        public static string VisualizationFormHelpInfo = "数据可视化，从数据中生成组织架构图、社交关系图和词云图，需要IE浏览器版本大于8";
        public static string WifiLocationFormHelpInfo = "根据WIFI热点的MAC或基站号进行定位，获取经纬度，覆盖范围和详细地址，需要网络";
        public static string BankToolFormHelpInfo = "根据银行卡号获取银行卡的卡种，开户行和其他信息，需要网络";
        public static string GPSTransformFormHelpInfo = "经纬度坐标系转换(百度、火星和WGS);计算两经纬度坐标之间的距离";
        public static string TimeAndIPTransformFormHelpInfo = "IP和整形IP之间的转换，绝对时间和真实时间之间的转换";
        public static string BigAPKFormHelpInfo = "APK检测系统";
        public static string FraudFormHelpInfo = "诈骗模拟器";
        public static string PostAndGetFormHelpInfo = "POST工具,向目标发送构造的HTTP/HTTPS报文,支持POST,GET,HEAD,PUT";
        public static string GoldEyesFormHelpInfo = "火眼金睛相关接口";
        public static string DownloadToolFormHelpInfo = "短视频、图像、邮件等下载提取工具";

        public static string OCRFormHelpInfo = "OCR图片检测文字";
        public static string NERFormHelpInfo = "将输入句子中的人名、地点、机构识别并定位";
        public static string ASRFormHelpInfo = "将语音文件转换为文本信息";
        public static string QRCodeFormHelpInfo = "对二维码进行定位与解析";
        public static string FaceAgeGenderFormHelpInfo = "识别人脸的年龄和性别";
        public static string InfoExtractionFormHelpInfo = "对微信号，手机号，网址，微信ID，QQ号，身份证号，姓名，微信群名，微信群号，IP地址，QQ群名，APP名，银行卡号、支付宝、邮箱、短地址、MAC地址等多个字段进行抽取";
        public static string LanguageDetectFormHelpInfo = "判断输入文本的语言种类";
        public static string DrugTextRecognitionFormHelpInfo = "判断输入语句是否涉赌";
        public static string PoliticsTextRecognitionFormHelpInfo = "判断输入语句是否涉政";
        public static string PornRecognitionFormHelpInfo = "判断输入图片是否涉黄";
        public static string TrackRecognitionFormHelpInfo = "对导航截图、登机牌、火车票、快递单、常规聊天截图、朋友圈截图等内容进行识别";
        public static string RedPocketRecognitionFormHelpInfo = "判断输入图像是否为红包截图和转账截图";
        public static string QRCodeRecognitionFormHelpInfo = "判断图片中是否存在二维码";
        public static string BankCardRecognitionFormHelpInfo = "判断图像中是否含有银行卡";
        public static string CardRecognitionFormHelpInfo = "对银行卡、营业执照、驾驶证、毕业证、房产证、身份证、结婚证、护照、居住证等九大证件类图像进行识别";
        public static string RedHeaderRecognitionFormHelpInfo = "对政府机关抄送的红头文件进行识别";
        public static string GunDetectionFormHelpInfo = "检测输入图像中是否含有枪支，并输出位置信息";
        public static string TerrorismDetectionFormHelpInfo = "判断输入图片是否包含社恐信息";
        public static string TibetanDetectionFormHelpInfo = "检测输入图片是否含有藏僧、藏旗，并输出具体位置信息";
        public static string FaceExpressionFormHelpInfo = "包含生气、失望、惊恐、害怕、开心、沮丧、惊喜、自然等表情";
        public static string FaceRecognizerFormHelpInfo = "对人脸进行编码，输出512维人脸编码";
        public static string FaceDetectorFormHelpInfo = "检测图片中是否包含人脸，并返回人脸位置、关键点信息";
        public static string FaceBeautyFormHelpInfo = "颜值打分(0-5分)";

        public static string CrackerFormHelpInfo = "基于ssh、rtd、mysql等协议的弱K令素描";
        public static string PwdGeneratorHelpInfo = "SG生成器，输入目标信息，猜测可能使用的数字字符组合";
        public static string WebScanHelpInfo = "Web目录素描，敏感文件素描和前后台入口素描";
        public static string RobotsScanHelpInfo = "对目标网站的几个通用特征进行素描";
        public static string WebShellHelpInfo = "盗洞专项配套工具集";
        public static string BinaryHelpInfo = "二进制逆向、分析和解密专项";
        public static string IntruderHelpInfo = "为大马模型定制化的登陆破门锤发包工具";
        public static string VPNHelpoInfo = "VPN和翻墙流量专项配套工具集";

        public static string DBFormHelpInfo = "涉赌专项分析";
        public static string SQFormHelpInfo = "涉枪专项分析";
        public static string SHFormHelpInfo = "涉黄专项分析";
        public static string DDFormHelpInfo = "盗洞专项分析";

        public static string XiseBackdoorHelpInfo = "肉鸡黑吃黑专项分析";
        public static string NetworkAssetsHelpInfo  = "购置境外网络资产专项分析";

        public static string BinaryStringsInfo = "二进制分析";
        public static string BinaryStringsDesc = "从二进制(exe,dll,.so,.lib,.a, ...)中提取文本,IP,域名,手机号,用户名...";

        public static string XiseDecryptInfo = "Xise流量解密";
        public static string XiseDecryptDesc = "破解Xise后门加密流量,配套黑吃黑战法";
        
        public static string BehinderDecryptInfo = "冰蝎流量解密";
        public static string BehinderDecryptDesc = "破解三代冰蝎(Behinder)的加密流量报文(AES128)";

        public static string BehinderEncryptInfo = "冰蝎流量加密";
        public static string BehinderEncryptDesc = "根据密码生成20/40位冰蝎加密特征字符串";

        public static string BaiduLBSDecryptInfo = "百度LBS报文解密(施工中)";
        public static string BaiduLBSDecryptDesc = "百度LBS报文解密,配套IP定位";

        public static string GaodeLBSDecryptInfo = "高德LBS报文解密(施工中)";
        public static string GaodeLBSDecryptDesc = "高德LBS报文解密,配套IP定位";

        public static string FileNotFoundHelpInfo = "文件不存在";
        public static string InvalidScaleHelpInfo = "输入的缩放比有误。缩放比仅支持大于0且小于20的整数";
        public static string InvalidLatitude = "输入纬度有误，纬度需为一个大于-90且小于90的浮点数";
        public static string InvalidLongitude = "输入经度有误，经度需为一个大于-180且小于180的浮点数";
        public static string InvalidCount = "输入权重有误，权重需为一个大于0的浮点数";
        public static string InvalidDimension = "经纬度维度不一致，请确保输入数据每行均有经纬度且被正确分割";
        public static string InvalidInputMapInfo = "输入数据有误，文件内容为空或经度、纬度、权重列格式有误";
        public static string InvalidDataType = "数据格式有误，目前内部数据仅支持文本数据和excel数据，外部数据仅支持oracle, Hive, postgres库";

        public static string InvalidPythonENV0 = "未配置python虚拟机，在‘【右上角】-【首选项】-【python引擎】’中配置。";
        public static string InvalidPythonENV1 = "选择一个运行Python脚本的虚拟机";
        public static string InvalidPythonENV2 = "未配置python虚拟机，点击确定跳转至虚拟机配置界面。";
        public static string MLFormInfo = "MD5加盐速查表";
        public static string MLFormDesc = "根据输入的MD5,Salt,用户名和明文密码,推算出所采用的加盐模式";
        public static string MD5CrackerInfo     = "常规-MD5-批爆";
        public static string MD5SaltCrackerInfo = "带盐-MD5-单爆";
        public static string MD5CrackerDesc = "不带盐MD5批量爆(针对宝塔特殊优化),成功率大于80%";
        public static string MD5SaltCrackerDesc = "带盐MD5单个爆,成功率大于73%";

        #region IAO基站批量查询
        public static string BaseStationHelpInfo = @"单次输入格式：4600051162c01(2G/3G) 或 46001590a8089407(4G) 或 37b900018bd0(电信2G)
批量查询格式：多个基站号码间用换行分隔，最大支持5000条";
        public static string BankToolHelpInfo = @"单次输入格式：6210857100018896476
批量查询格式：多个银行卡号间用换行分隔，最大支持5000条";
        public static string GPSTransformHelpInfo = @"单次输入格式：31.14 118.22，经纬度间采用空格分隔
批量查询格式：多个坐标间用换行分隔，最大支持5000条";
        public static string GPSDistanceHelpInfo = @"单次输入格式：31.14 118.22 21.14 108.22,经纬度间采用空格分隔
批量查询格式：多个坐标组间用换行分隔，最大支持5000条";
        public static string IPTransformHelpInfo = @"IP单次输入格式：23.125.23.8
整型单次输入格式：394073864
批量查询格式：多个IP或整型间用换行分隔，最大支持5000条";
        public static string TimeTransformHelpInfo = @"日期单次输入格式：2021-3-13 23:34:53 或 2021/3/13 23:34:53，年份不要超过2105
绝对秒输入格式：1615649693，秒数不要超过4,294,967,295
批量查询格式：多个日期或绝对秒间用换行分隔，最大支持5000条";
        #endregion
        /// <summary>
        /// 默认为Information,除非有非常严重的错误,否则一般尽量用温和的提示信息.
        /// MessageBoxIcon.Hand 
        /// MessageBoxIcon.Asterisk      星号
        /// MessageBoxIcon.Exclamation
        /// MessageBoxIcon.Information   通知
        /// MessageBoxIcon.Warning       警告
        /// MessageBoxIcon.Error         错误
        /// MessageBoxIcon.Question      疑问
        /// MessageBoxIcon.Stop         
        /// MessageBoxIcon.None          无图标
        /// </summary>
        public static DialogResult ShowMessageBox(string message, string caption = "提示信息", MessageBoxIcon type = MessageBoxIcon.Information)
        {
            return MessageBox.Show(message, caption, MessageBoxButtons.OK, type);
        }
    }
}
