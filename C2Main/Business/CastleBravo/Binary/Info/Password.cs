using C2.Core;
using C2.Utils;
using System.Collections.Generic;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.Binary.Info
{
    class Password
    {
        private static Password instance = null;

        private readonly List<string> dicts;
        private readonly List<string> passwords;
        
        public List<string> Pass { get => passwords; }
        
        public static Password GetInstance()
        {
            if (instance == null)
                instance = new Password();
            return instance;
        }

        private Password()
        {
            dicts = new List<string>() {
                Application.StartupPath + Settings.Path_Behinder_RKL_2944,
                Application.StartupPath + Settings.Path_JS_RKL_0_1000,
                Application.StartupPath + Settings.Path_JS_RKL_1000_8000,
                Application.StartupPath + Settings.Path_JS_RKL_8000_26000,
            };
            
            passwords = new List<string>();
            Load();
        }

        private void Load()
        {
            foreach(string ffp in dicts)
            {
                passwords.AddRange(FileUtil.FileReadToEnd(ffp).SplitLine());
            }
        }
    }
}
