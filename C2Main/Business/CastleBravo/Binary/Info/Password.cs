﻿using C2.Core;
using C2.Utils;
using System.Collections.Generic;
using System.Linq;
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
            if (dicts.IsEmpty())
                return;
            // 找不到时, 按顺序加载, 能省一点是一点
            string ffp = dicts.First();
            dicts.RemoveAt(0);
            string text = FileUtil.FileReadToEnd(ffp);
            passwords.AddRange(text.SplitLine());
        }

        public void MissLoad()
        {
            Load();
        }
    }
}
