﻿using C2.Configuration;
using C2.Controls;
using C2.Core;
using C2.Model.Documents;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace C2
{
    static class Helper
    {

        public static void OpenUrl(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                try
                {
                    if (File.Exists(url) && StringComparer.OrdinalIgnoreCase.Equals(Path.GetExtension(url), Document.Extension))
                        Global.GetMainForm().OpenMindMapDocument(url);
                    else
                        Process.Start(url);
                }
                catch (System.Exception ex)
                {
                    Helper.WriteLog(ex);
                    Global.GetMainForm().ShowMessage(ex);
                }
            }
        }

        public static void OpenUri(Uri uri)
        {
            if (uri != null)
            {
                OpenUrl(uri.ToString());
            }
        }

        public static void OpenFolder(string folder)
        {
            if (string.IsNullOrEmpty(folder) || !Directory.Exists(folder))
                return;

            try
            {
                string argument = string.Format("\"{0}\"", folder);
                Process.Start("explorer.exe", argument);
            }
            catch (System.Exception ex)
            {
                Helper.WriteLog(ex);
                Global.GetMainForm().ShowMessage(ex);
            }
        }

        public static void OpenFolder(string folder, bool tryCreate)
        {
            if (string.IsNullOrEmpty(folder))
                return;

            try
            {
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
            }
            catch
            {
                return;
            }

            OpenFolder(folder);
        }

        public static void OpenContainingFolder(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException("filename");
            if (!File.Exists(filename))
                throw new FileNotFoundException();

            Process.Start("Explorer.exe", string.Format("/select, \"{0}\"", filename));
        }

        public static bool TestModifierKeys(Keys keys)
        {
            return (Control.ModifierKeys & keys) == keys;
        }

        public static bool Equals(Array array1, Array array2)
        {
            if (array1 == null && array2 == null)
                return true;
            else if (array1 == null || array2 == null)
                return false;
            else if (array1.Length != array2.Length)
                return false;

            for (int i = 0; i < array1.Length; i++)
            {
                if (array2.GetValue(i) != array1.GetValue(i))
                    return false;
            }
            return true;
        }

        public static int GetMax(params int[] values)
        {
            int v = values[0];
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] > v)
                    v = values[i];
            }

            return v;
        }

        [DllImport("user32.dll")]
        static extern IntPtr LoadCursorFromFile(string lpFileName);

        public static Cursor LoadCursor(byte[] data)
        {
            string path = Path.GetTempFileName();
            File.WriteAllBytes(path, data);
            Cursor hand = new Cursor(LoadCursorFromFile(path));
            File.Delete(path);
            return hand;
        }

        public static void WriteLog(string text)
        {
            if (string.IsNullOrEmpty(text))
                return;

            try
            {
                string filename = Path.Combine(ProgramEnvironment.ApplicationDataDirectory, "error.log");
                using (StreamWriter sw = new StreamWriter(filename, true))
                {
                    sw.Write(string.Format("[{0:yyyy-MM-dd HH:mm:ss}]\t", DateTime.Now));
                    sw.WriteLine(text);
                    sw.Close();
                }
            }
            catch
            {
            }
        }

        public static void WriteLog(Exception ex)
        {
            WriteLog(ex.Message);
        }
    }
}
