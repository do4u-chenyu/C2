using C2.Core;
using C2.IAOLab.Plugins;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace C2.Dialogs
{
    public partial class ConfigForm : Form
    {
        private static LogUtil log = LogUtil.GetInstance("ConfigForm");
        private static readonly int PythonFFPColumnIndex = 0;
        private static readonly int AliasColumnIndex = 1;
        private static readonly int CheckBoxColumnIndex = 2;
        private static readonly Regex PythonVersionRegex = new Regex(@"^Python\s*(\d+\.\d+(\.\d+)?)\b", RegexOptions.IgnoreCase);
        private static readonly char[] IllegalCharacter = { ';', '?', '<', '>', '/', '|', '#', '!' };

        public ConfigForm()
        {
            InitializeComponent();
        }

        private void UserModelOkButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UserModelCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutOkButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PluginsOKButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PythonBrowseButton_Click(object sender, EventArgs e)
        {
            DialogResult dr = pythonOpenFileDialog.ShowDialog();
            if (dr != DialogResult.OK)
                return;
            string pythonFFP = pythonOpenFileDialog.FileName.Trim();
            string pythonVersion = String.Empty;
            if (CheckPythonInterpreter(pythonFFP, ref pythonVersion) != 0)
            {
                MessageBox.Show(String.Format("Python解释器似乎无法正常使用: {0}", pythonFFP),
                    "Python解释器导入错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            if (PythonInterpreterUsed(pythonFFP))
            {
                MessageBox.Show(String.Format("该Python解释器已经导入过了: {0}", pythonFFP),
                    "Python解释器重复导入",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            pythonOpenFileDialog.InitialDirectory = String.Empty;
            AddPythonInterpreter(pythonFFP, pythonVersion);

        }

        private bool PythonInterpreterUsed(string pythonFFP)
        {
            foreach (DataGridViewRow row in this.dataGridView.Rows)
            {
                if (row.Cells.Count > 0 && row.Cells[0].Value is string)
                {
                    if (row.Cells[0].Value.ToString() == pythonFFP)
                        return true;
                }

            }
            return false;
        }

        private void PythonConfigCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddPythonInterpreter(string pythonFFP, string pythonVersion)
        {
            // 默认别名采用 python 解释器的可执行文件名,但一般都是python.exe,看不出区别
            // 用正则表达式从路径中提取带版本号的python解释器名称,失败时采用默认命名
            string aliasDefault = "Python" + pythonVersion;
            this.pythonFFPTextBox.Text = pythonFFP;
            // 第一行 默认选中
            if (this.dataGridView.Rows.Count == 0)
            {
                this.dataGridView.Rows.Add(new Object[] { pythonFFP, aliasDefault, true });
                this.chosenPythonLable.Text = aliasDefault;
            }
            else
                this.dataGridView.Rows.Add(new Object[] { pythonFFP, aliasDefault, false });
        }

        private void AddPythonInterpreter(string pythonFFP, string alias, bool ifCheck)
        {
            this.dataGridView.Rows.Add(new Object[] { pythonFFP, alias, ifCheck });
            if (ifCheck)
            {
                this.pythonFFPTextBox.Text = pythonFFP;
                this.chosenPythonLable.Text = alias;
            }
        }

        private bool SavePythonConfig()
        {
            bool changed = false;
            // <add key="python" value="C:\PythonFake\Python37\python.exe|Python37|true;C:\PythonFake\Python37\python.exe|Python37|false;" />
            StringBuilder sb = new StringBuilder();
            foreach (DataGridViewRow row in this.dataGridView.Rows)
            {
                string pythonFFP = row.Cells[PythonFFPColumnIndex].Value.ToString().Trim();
                string alias = row.Cells[AliasColumnIndex].Value.ToString().Trim();
                bool ifCheck = (bool)row.Cells[CheckBoxColumnIndex].Value;
                sb.Append(pythonFFP).Append('|')
                  .Append(alias).Append('|')
                  .Append(ifCheck).Append(';');
            }
            string newPythonConfigString = sb.ToString();
            string oldPythonConfigString = ConfigUtil.TryGetAppSettingsByKey("python");
            if (newPythonConfigString != oldPythonConfigString)
            {
                ConfigUtil.TrySetAppSettingsByKey("python", sb.ToString());
                changed = true;
            }
            return changed;
        }

        // 运行python --version, 检查环境是否有问题
        private int CheckPythonInterpreter(string pythonFFP, ref string pythonVersion)
        {

            int defaultExitCode = 1;
            Process p = new Process();
            p.StartInfo.FileName = pythonFFP;
            p.StartInfo.Arguments = "--version";
            p.StartInfo.UseShellExecute = false; // 不显示用户界面
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardInput = true;//可以重定向输入  
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;

            try
            {
                if (!p.Start())
                    return defaultExitCode;
                // windows下 python.exe --version 命令输出, 有的版本是stderr,有的版本是stdout
                string version = p.StandardOutput.ReadToEnd();
                if (String.IsNullOrEmpty(version))
                    version = p.StandardError.ReadToEnd();

                p.WaitForExit(30 * 1000);

                if (p.ExitCode != 0)
                    return p.ExitCode;
                defaultExitCode = p.ExitCode;
                // 能匹配中就OK
                Match mat = PythonVersionRegex.Match(version);
                if (mat.Success)
                    pythonVersion = mat.Groups[1].Value.Trim();
                else // 可能不是python.exe程序, 报错
                    defaultExitCode = 1;
            }
            catch
            {
                pythonVersion = String.Empty;
                defaultExitCode = 1;
            }
            finally
            {
                if (p != null)
                    p.Close();
            }
            return defaultExitCode;
        }

        private void PythonConfigOkButton_Click(object sender, EventArgs e)
        {
            SavePythonConfig();
            Close();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            UserModelTabPage_Load();
            PythonConfigTabPage_Load();
            PluginsConfigTabPage_Load();
        }

        private void PluginsConfigTabPage_Load()
        {
            InitDefaultPlugins();
        }

        private void UserModelTabPage_Load()
        {
            this.userModelTextBox.Text = Global.WorkspaceDirectory;
        }

        private void UpdatablePlugins_Load()
        {
            List<string> updatableInfo = PluginsManager.Instance.UpdatablePluginList();
            foreach (string info in updatableInfo)
            {  
                this.availableDGV.Rows.Add(new Object[] { info.Split('\t')[0], info.Split('\t')[1], false });     
                
            }
        }




        private string GetDllVersion(string name)
        {
            try
            {
                return name.Replace(".dll", "").Split('-')[1];
            }
            catch
            {
                return string.Empty;
            }
        }
        private void PythonConfigTabPage_Load()
        {
            // 配置文件样例
            // <add key="python" value="C:\PythonFake\Python37\python.exe|Python37|true;C:\PythonFake\Python37\python.exe|Python37|false;" />
            // 加载已有的配置文件
            string value = ConfigUtil.TryGetAppSettingsByKey("python");
            foreach (string pItem in value.Split(';'))
            {
                string[] oneConfig = pItem.Split('|');
                // 格式不对, 忽略
                if (oneConfig.Length != 3)
                    continue;
                string pythonFFP = oneConfig[0].Trim();
                string alias = oneConfig[1].Trim();
                bool ifCheck = ConvertUtil.TryParseBool(oneConfig[2]);
                AddPythonInterpreter(pythonFFP, alias, ifCheck);
            }


            if (!String.IsNullOrEmpty(pythonOpenFileDialog.InitialDirectory))
                return;
            // 寻找可能的Python路径,
            string possibleInitialDirectory = ConfigUtil.GetDefaultPythonOpenFileDirectory();
            if (!String.IsNullOrEmpty(possibleInitialDirectory))
                pythonOpenFileDialog.InitialDirectory = possibleInitialDirectory;
        }


        private void DataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex != 1)
                return;

            string value = e.FormattedValue.ToString().Trim();
            // 别名不能为空
            if (String.IsNullOrEmpty(value))
            {
                dataGridView.Rows[e.RowIndex].ErrorText = "别名不能为空";
                e.Cancel = true;
                return;
            }
            // 特殊字符判断
            if (value.IndexOfAny(IllegalCharacter) != -1)
            {
                dataGridView.Rows[e.RowIndex].ErrorText = "别名不能含有特殊字符 " + String.Join(" ", IllegalCharacter);
                e.Cancel = true;
                return;
            }
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView.Rows[e.RowIndex];
            row.ErrorText = String.Empty;
            // 如果是当前选中的行的别名被修改, 更新chosen
            if (e.ColumnIndex == AliasColumnIndex && (bool)row.Cells[CheckBoxColumnIndex].Value)
                this.chosenPythonLable.Text = row.Cells[AliasColumnIndex].Value.ToString().Trim();
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != CheckBoxColumnIndex || e.RowIndex == -1) return;
            // 所有第三列的checkbox只允许互斥单选
            for (int i = 0; i < dataGridView.Rows.Count; i++)
                if (i != e.RowIndex)
                    (dataGridView.Rows[i].Cells[CheckBoxColumnIndex] as DataGridViewCheckBoxCell).Value = false;
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                // 全部都没选中时, chosen清空，  选定的当前列, 需要用 EditedFormattedValue,此时Value尚未提交
                DataGridViewCell cell = dataGridView.Rows[i].Cells[CheckBoxColumnIndex];
                if (i == e.RowIndex ? (bool)cell.EditedFormattedValue : (bool)cell.Value)
                {
                    this.chosenPythonLable.Text = dataGridView.Rows[i].Cells[AliasColumnIndex].Value.ToString().Trim();
                    return;
                }
            }
            this.chosenPythonLable.Text = String.Empty;
        }

        private void DataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                // 全部都没选中时, chosen清空，  选定的当前列, 需要用 EditedFormattedValue,此时Value尚未提交
                DataGridViewCell cell = row.Cells[CheckBoxColumnIndex];
                if ((bool)cell.Value)
                {
                    this.chosenPythonLable.Text = row.Cells[AliasColumnIndex].Value.ToString().Trim();
                    return;
                }
            }
            this.chosenPythonLable.Text = String.Empty;
        }


        private void PluginsCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region 插件
        private void InitDefaultPlugins()
        {
            foreach (IPlugin plugin in PluginsManager.Instance.Plugins)
            {
                this.installedDGV.Rows.Add(new Object[] { plugin.GetPluginName(), plugin.GetPluginVersion(), true });
                this.installedDGV.Rows[this.installedDGV.Rows.Count - 1].ReadOnly = true;
            }
        }
        #endregion
        private void InstalledDGV_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            String pluginName = this.installedDGV.Rows[e.RowIndex].Cells[0].Value as String;
            IPlugin pg = PluginsManager.Instance.FindPlugin(pluginName);
            this.installedTB.Text = pg.GetPluginDescription();
        }

        private void pluginsTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.availableDGV.Rows.Clear();
            if (this.pluginsTabControl.SelectedIndex != 1)
                return;
            UpdatablePlugins_Load();
        }

        private void InstallButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.availableDGV.Rows)
            {

                if (row.Cells[2].Value == null || !(bool)row.Cells[2].Value)
                    continue;
                try
                {

                    PluginsManager.Instance.DownloadPlugin(row.Cells[0].Value.ToString());
                    MessageBox.Show("插件下载成功，请重启软件加载新插件功能");

                }
                catch (Exception ex)
                {
                    HelpUtil.ShowMessageBox(ex.Message);
                }
                return;
            }

        }

        private void AvailableDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != CheckBoxColumnIndex || e.RowIndex == -1) return;
            // 所有第三列的checkbox只允许互斥单选
            for (int i = 0; i < availableDGV.Rows.Count; i++)
                if (i != e.RowIndex)
                    (availableDGV.Rows[i].Cells[CheckBoxColumnIndex] as DataGridViewCheckBoxCell).Value = false;
        }
    }
}
