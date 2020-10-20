namespace C2.IAOLab.PythonOP
{
    class PythonInterpreterInfo
    {
        private string pythonFFP;    // Python解释器全路径
        private string pythonAlias;  // 别名
        private bool chosenDefault;  // 被选中为Python算子默认采用的


        public PythonInterpreterInfo(string pythonFFP, string alias, bool chosen)
        {
            this.pythonFFP = pythonFFP;
            this.pythonAlias = alias;
            this.chosenDefault = chosen;
        }

        public string PythonFFP { get => pythonFFP; }
        public string PythonAlias { get => pythonAlias; }
        public bool ChosenDefault { get => chosenDefault; }
    }
}
