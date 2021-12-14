using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.GlueWater.Settings
{
    class SqGlueSetting : BaseGlueSetting
    {
        private string SqWebPath;
        private string SqMemberPath;

        private List<string> SqWebExcelColList;//excel里字段
        private List<string> SqMemberExcelColList;

        private string[] SqWebColList;//内存datatable里字段
        private string[] SqMemberColList;

        private static SqGlueSetting SqGlueSettingInstance;
        public static SqGlueSetting GetInstance()
        {
            if (SqGlueSettingInstance == null)
            {
                SqGlueSettingInstance = new SqGlueSetting();
            }
            return SqGlueSettingInstance;
        }

        public SqGlueSetting() : base()
        {
            SqWebPath = Path.Combine(txtDirectory, "SQ_web.txt");
            SqMemberPath = Path.Combine(txtDirectory, "SQ_member.txt");

            SqWebExcelColList = new List<string> { "网站名称", "网站网址", "网站Ip", "REFER标题", "REFER", "金额", "用户数", "赌博类型(多值##分隔)", "开始运营时间", "归属地", "发现时间" };
            SqMemberExcelColList = new List<string> { "网站网址", "认证账号", "最后登录IP", "登陆账号(多值##分隔)", "登陆密码(多值##分隔)" };

            SqWebColList = new string[] { "网站名称", "域名", "IP", "Refer对应Title", "Refer", "涉案金额", "涉赌人数", "赌博类型", "运营时间", "发现地市", "发现时间" };
            SqMemberColList = new string[] { "域名", "认证账号", "登陆IP", "登陆账号", "登陆密码", "安全码", "登陆地址" };

        }

        public override void InitDataTable()
        {
        }

        public override string RefreshHtmlTable(bool freshTitle = true)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<tr name=\"row\">" +
                      "    <th>论坛名称/网址/IP</th>" +
                      "    <th>认证账号/登陆IP</th>" +
                      "    <th>登陆账号/登陆密码</th>" +
                      "    <th>论坛注册时间</th>" +
                      "    <th>主题数/回帖数</th>" +
                      "    <th>发现地市/发现时间</th>" +
                      "</tr>"
                      );
            return sb.ToString();
        }

        public override DataTable SearchInfo(string item)
        {
            return base.SearchInfo(item);
        }

        public override string UpdateContent(string excelPath)
        {
            return base.UpdateContent(excelPath);
        }
    }
}
