using C2.Utils;
namespace C2Shell
{
    class Program
    {
        static void Main(params string[] args)
        {
            if (args.Length > 0)
            {
                string path = string.Empty;
                for (int i = 0; i < args.Length; i++)
                    path += args[i] + string.Empty;
                path = path.TrimEnd(OpUtil.Blank);
                SoftwareUpdate updateInstance = new SoftwareUpdate();
                if (updateInstance.IsNeedUpdate())
                {
                    if (!updateInstance.ExecuteUpdate())
                        updateInstance.Rollback();
                    updateInstance.Clear();
                }
                updateInstance.StartCoreProcess(path);
            }
            else {
                SoftwareUpdate updateInstance = new SoftwareUpdate();
                if (updateInstance.IsNeedUpdate())
                {
                    if (!updateInstance.ExecuteUpdate())
                        updateInstance.Rollback();
                    updateInstance.Clear();
                }
                updateInstance.StartCoreProcess();
            }
        }
    }
}