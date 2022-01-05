using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace C2.Business.GlueWater
{
    class BaseGlueSetting : IGlueSetting
    {
        public int maxRow = 65534;
        public string txtDirectory = Path.Combine(Global.UserWorkspacePath, "胶水系统");
        public string bakDirectory = Path.Combine(Global.UserWorkspacePath, "胶水系统", "backup");
        public List<string> doubleTypeColList; 

        public BaseGlueSetting()
        {
            if(!Directory.Exists(txtDirectory))
                FileUtil.CreateDirectory(txtDirectory);

            if (!Directory.Exists(bakDirectory))
                FileUtil.CreateDirectory(bakDirectory);

            //加载默认涉赌/涉枪/涉黄数据包
            string txtModelDirectory = Path.Combine(Application.StartupPath, "Resources/Templates/胶水系统");
            if (!File.Exists(Path.Combine(txtDirectory, "DB_member.txt")))
                CopyDirContentIntoDestDirectory(txtModelDirectory, txtDirectory, true);

            doubleTypeColList = new List<string>() { "涉案金额", "涉赌人数", "涉黄人数" };
        }
        public static void CopyDirContentIntoDestDirectory(string srcdir, string dstdir, bool overwrite)
        {
            if (!Directory.Exists(dstdir))
                Directory.CreateDirectory(dstdir);

            foreach (var s in Directory.GetFiles(srcdir))
                File.Copy(s, Path.Combine(dstdir, Path.GetFileName(s)), overwrite);

            foreach (var s in Directory.GetDirectories(srcdir))
                CopyDirContentIntoDestDirectory(s, Path.Combine(dstdir, Path.GetFileName(s)), overwrite);
        }

        public virtual void InitDataTable()
        {

        }

        public virtual string UpdateContent(string excelPath)
        {
            return string.Empty;
        }

        public virtual string RefreshHtmlTable(bool freshTitle)
        {
            return string.Empty;
        }

        public virtual DataTable SearchInfo(string item)
        {
            return new DataTable();
        }

        public virtual DataTable SearchInfoReply(string item)
        {
            return new DataTable();
        }

        public virtual void SortDataTableByCol(string col, string sortType)
        {
            return;
        }

        public string FindExcelFromZip(string zipPath)
        {
            //return DealWebContent(excelPath) && DealMemberContent(excelPath);
            //先将压缩包解压到临时文件夹，防止解压失败时原模型文件被覆盖
            string tmpDir = Path.Combine(Global.TempDirectory, Path.GetFileNameWithoutExtension(zipPath));
            FileUtil.DeleteDirectory(Global.TempDirectory);
            FileUtil.CreateDirectory(tmpDir);
            string errMsg = ZipUtil.UnZipFile(zipPath, tmpDir);
            if (!string.IsNullOrEmpty(errMsg))
                return errMsg;

            return ListDirectory(tmpDir);
        }

        private string ListDirectory(string path)
        {
            DirectoryInfo theFolder = new DirectoryInfo(path);

            foreach (FileInfo subFile in theFolder.GetFiles())
            {
                //目前逻辑是压缩包里只有一个excel，所以读到就可以返回了
                if (subFile.Name.EndsWith(".xlsx") || subFile.Name.EndsWith(".xls"))
                    return Path.GetFullPath(subFile.FullName);
            }

            foreach (DirectoryInfo NextFolder in theFolder.GetDirectories())
            {
                return ListDirectory(NextFolder.FullName);
            }
            return "数据包内不包含excel文件";
        }

        public void BackupZip(string zipPath)
        {
            string zipMD5 = GetMD5HashFromFile(zipPath);
            int sameNameCount = 0;
            string desName = Path.GetFileNameWithoutExtension(zipPath);
            DirectoryInfo bakDir = new DirectoryInfo(bakDirectory);
            foreach (FileInfo subFile in bakDir.GetFiles())
            {
                string tmpMD5 = GetMD5HashFromFile(subFile.FullName);
                if (zipMD5 == tmpMD5)
                    return;

                if (desName == Path.GetFileNameWithoutExtension(subFile.Name).Split("_")[0])
                    sameNameCount++;
            }
            if (sameNameCount > 0)
                desName = desName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + sameNameCount;
            FileUtil.FileCopy(zipPath, Path.Combine(bakDirectory, desName + ".zip"));
        }

        private string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, System.IO.FileMode.Open);
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                //压缩包打开的时候无法计算MD5
                LogUtil.GetInstance().Info(ex.Message);
                return string.Empty;
            }
        }

        public List<int> IndexFilter(List<string> colList, List<List<string>> rowContentList)
        {
            List<int> headIndex = new List<int> { };
            foreach (string content in colList)
            {
                for (int i = 0; i < rowContentList[0].Count; i++)
                {
                    if (rowContentList[0][i] == content)
                    {
                        headIndex.Add(i);
                        break;
                    }
                }
            }
            if (headIndex.Count != colList.Count)
            {
                HelpUtil.ShowMessageBox("上传的数据格式错误。");
                return new List<int>();
            }
            return headIndex;
        }

        public List<string> ContentFilter(List<int> indexList, List<string> contentList)
        {
            List<string> resultList = new List<string> { };
            {
                foreach (int index in indexList)
                    resultList.Add(contentList[index]);
            }

            return resultList;
        }


        public void ReWriteResult(string txtPath, DataTable dataTable)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(txtPath, false, System.Text.Encoding.UTF8))
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        List<string> rowContent = new List<string>();
                        for (int i = 0; i < dataTable.Columns.Count; i++)
                            rowContent.Add(row[i].ToString());

                        sw.WriteLine(string.Join("\t", rowContent));
                    }
                }
            }
            catch (Exception ex)
            {
                HelpUtil.ShowMessageBox(ex.Message);
            }
        }

        public DataTable GenDataTable(string path, string[] colList)
        {
            DataTable dataTable = new DataTable(Path.GetFileNameWithoutExtension(path));

            //为了方便排序，特定字段需要特殊处理
            foreach (string col in colList)
            {
                Type type = doubleTypeColList.Contains(col) ? typeof(double) : typeof(string);
                dataTable.Columns.Add(col, type);
            }
                

            if (!File.Exists(path))
                return dataTable;

            FileStream fs_dir = null;
            StreamReader reader = null;
            try
            {
                fs_dir = new FileStream(path, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(fs_dir);

                string lineStr;
                while ((lineStr = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(lineStr))
                        continue;

                    string[] rowList = lineStr.TrimEnd(new char[] { '\r', '\n' }).Split('\t');
                    List<string> tmpRowList = new List<string>();
                    for (int j = 0; j < colList.Length; j++)
                    {
                        string cellValue = j < rowList.Length ? rowList[j] : string.Empty;
                        tmpRowList.Add(cellValue);
                    }
                    dataTable.Rows.Add(tmpRowList.ToArray());
                }
            }
            catch { }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (fs_dir != null)
                    fs_dir.Close();
            }

            return dataTable;
        }

    }
}
