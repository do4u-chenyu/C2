using Renci.SshNet;
using Renci.SshNet.Common;
using System;
using System.Threading;

namespace C2.Business.SSH
{
    class Shell
    {
        public const int BufferSize = 4096 * 2;
        private readonly ShellStream shell;

        private readonly AutoResetEvent dataReceivedEvent;

        private byte[] InComing { get; }
        public Shell(ShellStream ssm)
        {
            InComing = new byte[1];
            dataReceivedEvent = new AutoResetEvent(false);
            shell = ssm;
            shell.DataReceived += DataReceived; 
        }

        ~Shell() { this.shell.DataReceived -= DataReceived; }

        private void DataReceived(object sender, ShellDataEventArgs e)
        {
            dataReceivedEvent.Set();
        }

        public int Read(byte[] buffer, int offset, int count, TimeSpan timeout)
        {
            int len;
            while (true)
            {
                len = shell.Read(buffer, offset, count);
                if (len > 0)  // 如果读到就返回
                    break;

                if (timeout.Ticks > 0 && !dataReceivedEvent.WaitOne(timeout))
                    return 0; // 超时返回
                else
                    dataReceivedEvent.WaitOne();

            }
            return len;
        }

        public bool ReadByte(ref byte b, TimeSpan timeout)
        {
            if (Read(InComing, 0, 1, timeout) > 0)
            {
                b = InComing[0];
                return true;
            }
                
            return false;
        }
    
    }
}
