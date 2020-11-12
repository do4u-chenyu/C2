using C2.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.Model
{
    class ModelsInfo
    {
        public static string[] LoadAllModelTitle(string modelPath)
        {
            string[] modelTitles = new string[0];
            try
            {
                DirectoryInfo userDir = new DirectoryInfo(modelPath);
                DirectoryInfo[] dir = userDir.GetDirectories();
                modelTitles = Array.ConvertAll(dir, value => Convert.ToString(value));
            }
            catch { }
            return modelTitles;
        }
    }
}
