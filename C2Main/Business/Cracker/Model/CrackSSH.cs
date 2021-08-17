using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.Cracker.Model
{
    class CrackSSH : CrackService
    {
        public CrackSSH()
        {

        }

        public override Server creack(string ip, int port, string username, string password, int timeOut)
        {
            //Chilkat.Ssh ssh = new Chilkat.Ssh();
            Server server = new Server();
            SshClient ssh = new SshClient(ip, port, username, password);
            ssh.ConnectionInfo.Timeout = TimeSpan.FromSeconds(timeOut);
            try
            {
                ssh.Connect();
                server.isSuccess = true;
            }
            catch (Exception)
            {
                throw new Exception("SSH连接失败！");
            }
            finally
            {
                ssh.Disconnect();
            }
            return server;
        }

    }
}
