using System;
using System.IO;

namespace C2.Business.Model
{
    class ModelInfo
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
