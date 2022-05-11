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

        public static Socket Send(string host, int port, string data, int timeout)
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                ReceiveTimeout = timeout
            };
            clientSocket.Connect(host, port);
            clientSocket.Send(Encoding.Default.GetBytes(data));
            return clientSocket;

        }

        public static string Receive(Socket socket) //10s 
        {
            string result = string.Empty;
            List<byte> data = new List<byte>();
            byte[] buffer = new byte[1024];

            int length;
            while ((length = socket.Receive(buffer)) > 0)
            {
                for (int j = 0; j < length; j++)
                    data.Add(buffer[j]);
                if (length < buffer.Length)
                    break;
            }
            if (data.Count > 0)
            {
                result = Encoding.Default.GetString(data.ToArray(), 0, data.Count);
            }
            return result;
        }

        public static void DestroySocket(Socket socket)
        {
            if (socket == null)
                return;
            if (socket.Connected)
            {
                try { socket.Shutdown(SocketShutdown.Both); }
                catch { }
            }
            socket.Close();
        }

        public static string RndProbeResponse(string ip, int port, string data, int timeout)
        {
            Socket s = null;
            string result = string.Empty;
            string time = DateTime.Now.ToString("yyyyMMddhhmmss");
            try
            {
                s = Send(ip, port, data, timeout * 1000);
                result = Receive(s);

            }
            catch (SocketException e)
            {
                result = e.SocketErrorCode.ToString();
            }
            finally
            {
                DestroySocket(s);
            }
            return time + "\t" + data.Length + "\t" + result;

        }
    }
}
