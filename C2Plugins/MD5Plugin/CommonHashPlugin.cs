namespace MD5Plugin
{
    public partial class CommonHashPlugin : CommonPlugin
    {
        public CommonHashPlugin()
        {
            InitializeComponent();
            this.encodeTypeCB.SelectedIndex = 0;
        }

        protected byte[] GetBytes()
        {
            return new byte[0];
        }
    }
}
