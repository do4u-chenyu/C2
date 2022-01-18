using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.CastleBravo.Intruder.Config
{
    class Config
    {

        public List<string> GetDictByPath(string path)
        {
            List<string> dictPathList = new List<string>();
            if (Directory.Exists(path))
            {
                foreach (FileSystemInfo fsinfo in new DirectoryInfo(path).GetFiles())
                {
                    if (Path.GetExtension(fsinfo.FullName) == ".txt")
                        dictPathList.Add(fsinfo.FullName);
                }
            }

            return dictPathList;
        }

        public string GetFileSize(string sFullName)
        {
            long FactSize = 0;
            if (File.Exists(sFullName))
                FactSize = new FileInfo(sFullName).Length;

            string m_strSize = string.Empty;

            if (FactSize < 1024.00)
                m_strSize = FactSize.ToString("F1");
            else if (FactSize >= 1024.00 && FactSize < 1048576)
                m_strSize = (FactSize / 1024.00).ToString("F1") + " K";
            else if (FactSize >= 1048576 && FactSize < 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00).ToString("F1") + " M";
            else if (FactSize >= 1073741824)
                m_strSize = (FactSize / 1024.00 / 1024.00 / 1024.00).ToString("F1") + " G";
            return m_strSize;
        }

        public string GetFileLines(string path, Dictionary<string, List<string>> dictContent)
        {
            int lineCount = 0;

            List<string> contentList = new List<string>();
            FileStream fs_dir = null;
            StreamReader reader = null;
            try
            {
                fs_dir = new FileStream(path, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(fs_dir);

                string lineStr;

                while ((lineStr = reader.ReadLine()) != null)
                {
                    if (!lineStr.Equals(""))
                    {
                        contentList.Add(lineStr);
                        lineCount++;
                    }
                }
            }
            catch { }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (fs_dir != null)
                {
                    fs_dir.Close();
                }
                dictContent.Add(Path.GetFileName(path), contentList);
            }
            return lineCount.ToString();
        }
    }
}
