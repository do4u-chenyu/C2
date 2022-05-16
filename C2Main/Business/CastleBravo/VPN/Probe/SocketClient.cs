using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace C2.Business.CastleBravo.VPN.Probe
{

    public class SocketClient
    {
        public static Socket Create(out Socket s, int timeout)
        {
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                SendTimeout = timeout,
                ReceiveTimeout = timeout,
            };
            return s;
        }
        public static Socket Connect(Socket s, string host, int ip)
        { 
            IAsyncResult iar = s.BeginConnect(host, ip, null, null);
            if (iar.AsyncWaitHandle.WaitOne(s.SendTimeout, false))
                return s;
            throw new Exception("TCP_Connect_Timeout");
        }

        public static Socket Send(Socket socket, string data)
        {
            socket.Send(Encoding.Default.GetBytes(data));
            return socket;
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

        public static string RndProbeResponse(string ip, int port, string data, int timeoutSec)
        {
            Socket s = null;
            string m = string.Empty;
            try
            {
                // 探针分为三个阶段: 建立链接, 发送数据, 接收数据, 三阶段都可能超时(失败)
                // 建立链接超时 说明IPort+TCP不通，与后面收发数据超时意义不同
                // 发送数据失败 可能是对方问题,但大概率是自己问题
                // 接收数据失败 可能是自己问题,但大概率是对方问题
                // 期望的是能建立链接(三方握手过), 且能发送数据, 但对方不应答(超时)或者应答FIN
                m = Receive(Send(Connect(Create(out s, timeoutSec * 1000), ip, port), data));
            }
            catch (SocketException e)
            {
                m = e.SocketErrorCode.ToString();
            }
            catch (Exception e)
            {
                m = e.Message;
            }
            finally
            {
                DestroySocket(s);
            }
            return string.Format("{0}\t{1}\t{2}", DateTime.Now.ToString("yyyyMMddhhmmss"), data.Length, m);
        }
    }
}
