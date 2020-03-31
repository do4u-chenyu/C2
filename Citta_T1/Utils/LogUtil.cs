using Citta_T1.Dgvs;
using System.Reflection;
using log4net;

namespace Citta_T1.Utils
{
    class LogUtil
    {
        private static LogUtil instance = null;
        private ILog log;
        private string moduleName;

        public string ModuleName { get => moduleName; set => moduleName = value; }

        public LogUtil()
        {
            log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public static LogUtil GetInstance(string moduleName = "MainForm")
        {
            if (instance == null)
                instance = new LogUtil();
            instance.ModuleName = moduleName;
            return instance;
        }

        public LogUtil Warn(string content)
        {
            log.WarnFormat("{0}:{1}", this.ModuleName, content);
            if (Global.GetLogView() != null)
                Global.GetLogView().LogUpdate(content);
            return this;
        }

        public LogUtil Info(string content)
        {
            log.InfoFormat("{0}:{1}", this.ModuleName, content);
            if (Global.GetLogView() != null)
                Global.GetLogView().LogUpdate(content);
            return this;
        }

        public LogUtil Error(string content)
        {
            log.ErrorFormat("{0}:{1}", this.ModuleName, content);
            if (Global.GetLogView() != null)
                Global.GetLogView().LogUpdate(content);
            return this;
        }

        public LogUtil Fatal(string content)
        {
            log.FatalFormat("{0}:{1}", this.ModuleName, content);
            if (Global.GetLogView() != null)
                Global.GetLogView().LogUpdate(content);
            return this;
        }
    }
}
