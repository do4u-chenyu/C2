namespace C2Shell
{
    class Program
    {
        static void Main()
        {

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
