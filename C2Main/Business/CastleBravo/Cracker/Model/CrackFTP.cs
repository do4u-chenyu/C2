using FluentFTP;
using System;
using System.Net;

namespace C2.Business.Cracker.Model
{
    class CrackFTP : CrackService
    {
        public CrackFTP()
        {

        }

        public override Server creack(String ip, int port, String username, String password, int timeOut)
        {

            FtpClient ftp = new FtpClient();
            Server server = new Server();
            if ("空".Equals(password))
            {
                password = string.Empty;
            }
            try
            {
                ftp.Host = ip;
                ftp.Port = port;
                ftp.Credentials = new NetworkCredential(username, password);
                ftp.ConnectTimeout = timeOut * 1000;
                ftp.ReadTimeout = timeOut * 1000;

                ftp.Connect();

                if (ftp.IsConnected)
                {
                    server.isSuccess = true;
                    server.banner = ftp.SystemType;
                }
            }
            catch (Exception e)
            {
                if (e.Message.IndexOf("cannot log in") == -1)
                {
                    throw e;
                }
            }
            finally
            {
                if (ftp != null)
                {
                    ftp.Disconnect();
                }
            }
            return server;
        }



    }
}
