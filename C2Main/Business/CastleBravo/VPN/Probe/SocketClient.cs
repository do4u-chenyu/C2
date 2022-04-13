using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.CastleBravo.VPN.Probe
{
    
    public class SocketClient
    {
        private static Encoding encode = Encoding.Default;
        public SocketClient()
        { }
        public static Socket Send(string host, int port, string data)
        {
            try
            {
                Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                {
                    ReceiveTimeout= 10000
                };
                clientSocket.Connect(host, port);
                clientSocket.Send(encode.GetBytes(data));
                return clientSocket;
            }
            catch 
            {
                return null;
            }
        }

        public static string Receive(Socket socket) //10s 
        {
            string result = string.Empty;
            List<byte> data = new List<byte>();
            byte[] buffer = new byte[1024];          
            try
            {
                int length = 0;
                while ((length = socket.Receive(buffer)) > 0)
                {
                    for (int j = 0; j < length; j++)
                        data.Add(buffer[j]);
                    if (length < buffer.Length)
                        break;
                }
                if (data.Count > 0)
                {
                    result = encode.GetString(data.ToArray(), 0, data.Count);
                }
            }
            catch(Exception ex) 
            {
                result = ex.Message;
            }
            
            return result;
        }

        public static void DestroySocket(Socket socket)
        {
            if (socket.Connected)
            {
                try { socket.Shutdown(SocketShutdown.Both); }
                catch { }
            }             
            socket.Close();
        }
    }
}
