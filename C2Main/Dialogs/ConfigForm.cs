﻿using C2.Core;
using C2.IAOLab.Plugins;
using C2.Properties;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace C2.Dialogs
{
    public partial class ConfigForm : Form
    {
        public const int scalaMax = 13;
        private static readonly int PythonFFPColumnIndex = 0;
        private static readonly int AliasColumnIndex = 1;
        private static readonly int CheckBoxColumnIndex = 2;
        private static readonly Regex PythonVersionRegex = new Regex(@"^Python\s*(\d+\.\d+(\.\d+)?)\b", RegexOptions.IgnoreCase);
        private static readonly char[] IllegalCharacter = { ';', '?', '<', '>', '/', '|', '#', '!' };
        public string latStr;
        public string lonStr;
        public string scaleStr;
        public string baiduAPIKeyStr;
        public string baiduHeatAPI;
        private UpdateProgressBar progressBar;
        private readonly PluginsDownloader downloader;
        private string newSoftwareVersion;
        private string packageLocation;
        private string tmpPackageLocation;


        public ConfigForm()
        {
            InitializeComponent();
            Icon = C2.Properties.Resources.logo;
            progressBar = new UpdateProgressBar();
            progressBar.FormClosed += FormClosedEventHandler;
            downloader = new PluginsDownloader
            {
                Client_DownloadFileCompleted = this.Client_DownloadFileCompleted,
                Client_DownloadProgressChanged = this.Client_DownloadProgressChanged
            };
            lonStr = this.baiduLonTB.Text = Settings.Default.longitude;
            latStr = this.baiduLatTB.Text = Settings.Default.latitude;
            scaleStr = this.baiduScaleTB.Text = Settings.Default.scale;
            baiduAPIKeyStr = this.baiduAPIKey.Text = Settings.Default.baiduAPIKey;
        }


        private void AboutCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void CancelUpdateButton_Click(object sender, EventArgs e)
        {
            if (this.progressBar.Visible)
            {
                MessageBox.Show("正在下载更新，无法关闭",
                   "下载提示",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                return;
            }
               
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
            WFDConfigTabPage_Load();
        }

        private void PluginsConfigTabPage_Load()
        {
            RefreshInstalledPlugins();
            RefreshUpdatablePlugins();
        }

        private void UserModelTabPage_Load()
        {
            this.userModelTextBox.Text = Global.WorkspaceDirectory;
        }

        private void RefreshUpdatablePlugins()
        {
            using (new GuarderUtil.CursorGuarder())
            {
                this.availableDGV.Rows.Clear();
                List<string> updatableInfo = PluginsManager.Instance.UpdatablePluginList();
                foreach (string info in updatableInfo)
                {
                    string[] info_split = info.Split(OpUtil.TabSeparator);
                    if (info_split.Length < 3)
                        continue;
                    string pluginName = info_split[0];
                    string pluginVersion = info_split[1];
                    string pluginDesc = info_split[2];
                    this.availableDGV.Rows.Add(new Object[] { pluginName, pluginVersion, false, pluginDesc }); // 第4列隐藏
                }
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
        private void RefreshInstalledPlugins()
        {
            this.installedDGV.Rows.Clear();
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



        private void InstallButton_Click(object sender, EventArgs e)
        {
            using (GuarderUtil.WaitCursor)
            {
                foreach (DataGridViewRow row in this.availableDGV.Rows)
                {

                    if (row.Cells[2].Value == null || !(bool)row.Cells[2].Value)
                        continue;
                    try
                    {
                        PluginsManager.Instance.DownloadPlugin(GetPluginFullName(row));
                        MessageBox.Show("插件下载成功，请重启软件加载新插件功能");
                    }
                    catch (Exception ex)
                    {
                        HelpUtil.ShowMessageBox(ex.Message);
                    }
                    return;
                }
            }


        }
        private string GetPluginFullName(DataGridViewRow row)
        {
            try
            {
                return row.Cells[0].Value.ToString() + "-" + row.Cells[1].Value.ToString() + ".dll";
            }
            catch
            {
                return string.Empty;
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

        private void AvailableDGV_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            String pluginDesc = this.availableDGV.Rows[e.RowIndex].Cells[3].Value as String;
            this.availableTB.Text = pluginDesc;
        }

        private void PluginsTabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == this.availableSubPage)
                RefreshUpdatablePlugins(); // 头一个叫available,后一个偏要叫updatable,劝也不听
            if (e.TabPage == this.installedSubPage)
                RefreshInstalledPlugins();
        }
        #region 检查更新Tab

        private void MainTabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (!mainTabControl.SelectedTab.Text.Equals("检查更新"))
                return;
           
            Task.Run(() => CheckUpdate());
        }
        private void CheckUpdate()
        {
            string currentVersion = Program.Software_Version;
            this.Invoke((EventHandler)(delegate
            {
                this.currentVersion.Text = "V" + currentVersion;
            }));
            
            this.newSoftwareVersion = NewSoftewareVersion();
            if (IsFormNotExist())
                return;
            if (newSoftwareVersion.Contains(currentVersion))
            {             
                this.Invoke((EventHandler)(delegate
                 {
                     this.SuspendLayout();
                     this.title.Text = "当前已为最新版本";
                     this.versionLable.Text = @"当前版本:";
                     this.version.Text = currentVersion;                    
                     this.sizeLable.Visible = false;
                     this.size.Visible = false;
                     this.checking.Visible = false;
                     this.button3.Enabled = false;
                     this.ResumeLayout(false);
                 }));
            }
            else if (newSoftwareVersion.IsNullOrEmpty())
            {
                FetchNewVersionFail();
            }
            else
            {
                string softwareInfo = NewSoftwareInfo(newSoftwareVersion);
                string[] info_split = softwareInfo.Split(OpUtil.TabSeparator);
                if (info_split.Length < 3)
                {
                    FetchNewVersionFail();
                    return;
                }
                this.Invoke((EventHandler)(delegate
                {
                    this.SuspendLayout();
                    this.version.Text = info_split[0];
                    this.size.Text = info_split[1];
                    this.description.Text = info_split[2];
                    this.checking.Visible = false;
                    this.ResumeLayout(false);
                }));
            }
        }
        private void FetchNewVersionFail()
        {
            this.Invoke((EventHandler)(delegate
            {
                this.currentModelRunLab.Visible = false;
                this.checkStatus.Text = "联网检查更新失败";
            }));
        }
        private bool IsFormNotExist()
        {
            return (!this.IsHandleCreated || this.IsDisposed);
        }
        private string NewSoftewareVersion()
        {
            string htmlContent = this.downloader.GetHtmlContent(Global.SoftwareUrl);
            List<string> packageName = PluginsManager.Instance.GetPluginsNameList(htmlContent);
            return packageName.IsNullOrEmpty() ? string.Empty : packageName[0];
        }
        private string NewSoftwareInfo(string name)
        {
            string packageDir = Path.Combine(Global.SoftwareUrl, @"software/");
            return this.downloader.GetPluginInfo(name, packageDir);
        }
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            // 正在下载,不能点击更新
            if (this.progressBar.Visible)
                return;
            
            try
            {                 
                string softwareName = newSoftwareVersion.Replace(".info", String.Empty);
                string packageDir = Path.Combine(Global.SoftwareUrl, @"software/", softwareName);
                packageLocation = Path.Combine(Global.SoftwareSavePath, softwareName);
                tmpPackageLocation = packageLocation + ".download";
                if (File.Exists(packageLocation))
                    HelpUtil.ShowMessageBox("下载成功，请重启更新软件");
                else
                {
                    this.downloader.SoftwareDownload(packageDir, tmpPackageLocation);
                    this.progressBar.Status = "下载中";
                    this.progressBar.ShowDialog();
                }
                                                     
            }
            catch 
            {
                HelpUtil.ShowMessageBox("更新失败，请检查网络连接稍后重试");
            }
            
           
        }
        #endregion

        private void GisMapOKButton_Click(object sender, EventArgs e)
        {
            if (!IsValidLatitude())
            {
                HelpUtil.ShowMessageBox(HelpUtil.InvalidLatitude);
                return;
            }
            if (!IsValidLongitude())
            {
                HelpUtil.ShowMessageBox(HelpUtil.InvalidLongitude);
                return;
            }
            if (!IsValidScale())
            {
                HelpUtil.ShowMessageBox(HelpUtil.InvalidScaleHelpInfo);
                return;
            }            
            Settings.Default.latitude = this.baiduLatTB.Text;
            Settings.Default.longitude = this.baiduLonTB.Text;
            Settings.Default.scale = this.baiduScaleTB.Text;
            Settings.Default.baiduAPIKey = this.baiduAPIKey.Text;
            Settings.Default.Save();
            UpdateHtmlTemplate();
            this.Close();
        }

        private bool IsValidLongitude()
        {
            return float.TryParse(this.baiduLonTB.Text, out float lon) && -180 < lon && lon < 180;
        }

        private bool IsValidLatitude()
        {
            return float.TryParse(this.baiduLatTB.Text, out float lat) && -90 < lat && lat < 90;
        }
        private bool IsValidScale()
        {
            return int.TryParse(this.baiduScaleTB.Text, out int scale) && 0 < scale && scale < 20;
        }

        //下载进度变化触发事件
        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {

            this.progressBar.MinimumValue = 0;//进度条最小值
            this.progressBar.MaximumValue = (int)e.TotalBytesToReceive;//下载文件的总大小
            this.progressBar.CurrentValue = (int)e.BytesReceived;//已经下载的大小
            this.progressBar.ProgressPercentage = e.ProgressPercentage + "%";//更新界面展示
            this.progressBar.ProgressValue = e.ProgressPercentage;

        }

        //下载完文件触发此事件
        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (this.progressBar.ProgressValue == 100)
            {
                this.progressBar.Status = "下载完成,请重启软件实现更新";
                try 
                {
                    if (File.Exists(tmpPackageLocation))
                        File.Move(tmpPackageLocation, packageLocation);
                }
                catch 
                { }
               
            }
            else
                this.progressBar.Status = "下载失败，请检查网络重新下载";

        }
        private void FormClosedEventHandler(object sender, FormClosedEventArgs e)
        {
            this.downloader.StopDownloadAsync();
        }


        public void UpdateHtmlTemplate()
        {
            //---------------------读html模板页面到stringbuilder对象里----
            StringBuilder htmlSb = new StringBuilder();
            try
            {
                string url = Path.Combine(Application.StartupPath, "Business\\IAOLab\\WebEngine\\Html", "StartMap.html");
                using (StreamReader sr = new StreamReader(url)) //模板页路径
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        htmlSb.Append(line);
                        htmlSb.Append('\n');
                    }
                    sr.Close();
                }
            }
            catch
            {

                HelpUtil.ShowMessageBox("文件读取错误！");

            }
            //----------替换html内容
            string htmlText = htmlSb.ToString();
            htmlText = Regex.Replace(htmlText, "ak=.*\"", "ak=" + this.baiduAPIKey.Text + "\"");

            //----------生成htm文件------------------
            try
            {
                string url = Path.Combine(Application.StartupPath, "Business\\IAOLab\\WebEngine\\Html", "StartMap.html");
                using (StreamWriter sw = new StreamWriter(url, false, System.Text.Encoding.GetEncoding("GB2312"))) //保存地址
                {
                    sw.WriteLine(htmlText);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch
            {
                HelpUtil.ShowMessageBox("更改API失败！");
            }
        }

        private void GisMapCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WFDCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WFDOKButton_Click(object sender, EventArgs e)
        {
            //检验输入是否正确，并修改配置文件
            Close();
        }

        private void WFDResetButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WFDConfigTabPage_Load()
        {
            /*  初始化
             *  
             *  判断本地是否有配置文件，配置文件中是否有该字段，该字段是否有值？
             *     有值  ->  填入首选项
             *     其他  ->  默认值（有个默认值）
             */
            if (!LoadXmlWFDConfig())
                return;
            
        }

        private bool LoadXmlWFDConfig()
        {
            string xmlPath = Path.Combine(Global.WorkspaceDirectory, Global.GetUsername(), "侦察兵", "WFDTasks.xml");
            if (!File.Exists(xmlPath))
                return false;

            XmlDocument xDoc = new XmlDocument();
            try
            {
                xDoc.Load(xmlPath);
                xDoc.SelectNodes(@"WFDTasks/task");



            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}