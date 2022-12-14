using C2.Core;
using log4net;
using System.Reflection;

namespace C2.Utils
{
    class LogUtil
    {
        private static LogUtil instance = null;
        private ILog log;
        private string moduleName;

        public string ModuleName { get => moduleName; set => moduleName = value; }

        private LogUtil()
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
                Global.GetLogView().LogUpdateWarn(content);
            return this;
        }

        public LogUtil Info(string content)
        {
            log.InfoFormat("{0}:{1}", this.ModuleName, content);
            if (Global.GetLogView() != null)
                Global.GetLogView().LogUpdateInfo(content);
            return this;
        }

        public LogUtil Debug(string content)
        {
            log.DebugFormat("{0}:{1}", this.ModuleName, content);
            if (Global.GetLogView() != null)
                Global.GetLogView().LogUpdateDebug(content);
            return this;
        }

        public LogUtil Error(string content)
        {
            log.ErrorFormat("{0}:{1}", this.ModuleName, content);
            if (Global.GetLogView() != null)
                Global.GetLogView().LogUpdateError(content);
            return this;
        }
        public LogUtil ErrorFromDataBase(string content) 
        {
            log.ErrorFormat("{0}:{1}", this.ModuleName, content);
            if (Global.GetLogView() != null)
                Global.GetLogView().ActiveUpdateLog(content);
            return this;
        }
        public LogUtil Fatal(string content)
        {
            log.FatalFormat("{0}:{1}", this.ModuleName, content);
            if (Global.GetLogView() != null)
                Global.GetLogView().LogUpdateFatal(content);
            return this;
        }
    }
}
