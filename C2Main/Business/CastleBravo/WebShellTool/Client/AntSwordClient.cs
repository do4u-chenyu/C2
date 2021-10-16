namespace C2.Business.CastleBravo.WebShellTool
{
    class AntSwordClient : CommonClient
    {
        public AntSwordClient(string password, string clientSetting)
            :base(password, clientSetting)
        {
            this.prefix = password + "=";
        }

        private string SPL
        {
            get { return string.Empty; }
        }

        private string SPR
        {
            get { return string.Empty; }
        }
        
        private string ValueAB
        {
            get { return string.Empty; }
        }

        private string TmpDir
        {
            get { return string.Empty; }
        }

        private string Param1K
        {
            get { return string.Empty; }
        }

        //private string IndexTemplate;
        //private string PhpinfoTemplate;
        //private string ReadDictTemplate;
        //private string ShellTemplate;

        private string PhpinfoStart()
        {
            return string.Empty;
        }

        private void PhpinfoEnd()
        {
            
        }

        public override string PHPInfo()
        {
            return string.Empty;
        }


    }
}
