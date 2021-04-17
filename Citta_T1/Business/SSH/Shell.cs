using C2.Utils;
using Renci.SshNet;
using Renci.SshNet.Common;
using System;
using System.IO;
using System.Threading;

namespace C2.Business.SSH
{
    class Shell
    {
        private readonly ShellStream shell;
        private readonly AutoResetEvent dataReceivedEvent; // 信号量

        private static readonly byte[] TgzHeadBytes = new byte[] { 0x1f, 0x8b, 0x08 };  // 1f 8b 08 .tgz的文件头

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
            while (true)
            {
                int len = shell.Read(buffer, offset, count);
                if (len > 0)  // 如果读到就返回
                    return len;

                if (timeout.Ticks > 0)              // 超时等待
                {
                    if (!dataReceivedEvent.WaitOne(timeout))
                        return 0;
                }
                else
                    dataReceivedEvent.WaitOne();    // 永久等待
            }
        }

        // 
        // 等待TGZ头，并将超越的字节写入文件，返回写入的个数(包含TGZ头)
        // 如果超时返回0
        // 如果一个buffersize内没有等到，也返回0
        // 成功的情况下，至少大于等于3
        // 这个函数有潜在的越界风险, 调用时buffer的size要是4096的2倍以上
        internal long ExpectTGZ(byte[] buffer, FileStream fs, TimeSpan timeout, int maxBytesRead = 4096)
        {
            int count = 0;
            if (buffer.Length < maxBytesRead) // 有越界风险,认为失败
                return 0;
            int perBytesRead = (int)(maxBytesRead / 4);

            do
            {   // 每次读1K
                int bytesRead = Read(buffer, count, perBytesRead, timeout);
                if (bytesRead == 0)  // 超时，退出
                    return 0;

                count += bytesRead;
                // 当前数据流里判断是否发现TGZ头,返回所在位置
                int pos = IsExpected(buffer, count);
                if (pos >= 0)    // 发现
                {
                    // 写入文件头
                    fs.Write(TgzHeadBytes, 0, TgzHeadBytes.Length);
                    // 写入当前已知的文件内容, 这里也要替换CRLF
                    int real = ReplaceCRNLWrite(buffer, pos + TgzHeadBytes.Length, count - pos - TgzHeadBytes.Length, fs);
                    return real + TgzHeadBytes.Length;
                }
 
                count += bytesRead;
            }
            while (count < maxBytesRead); // 一共读4K

            return 0;
        }



        private int IsExpected(byte[] buffer, int count)
        {
            if (count < 3)
                return -1;

            // 从后往前遍历, 效率高
            int end = count - 2;
            for (int i = 0; i < end; i++)
                if (IsTGZ(buffer, end - i))
                    return end - i;
     
            return -1;
        }

        internal int ReplaceCRNLWrite(byte[] buffer, int offset, int count, FileStream fs)
        {
            count = Math.Min(buffer.Length, count + offset); // 保险一下，下载错误的文件比程序崩强

            int totalBytesWrite = 0; // 实际写入字节
            int curr = offset;       // 当前游标位置
            int head = offset;       // 当前写入起始位置

            if (count < 2)  // 不足2个字节,不可能含有CRNL
            {
                fs.Write(buffer, head, curr + 1 - head);
                return curr - head + 1;
            }

            //  |                |      [0, count - 1)             
            // [++++++\r\n++++++++++++-]
            do
            {
                // 找到 下一个 /r/n
                while (curr < count - 1 && !IsCRLF(buffer, curr))
                    curr++;

                int bytesWrite = curr - head + 1;

                // 没找到, 直接到结尾处,退出
                if (curr >= count - 1)
                {
                    fs.Write(buffer, head, bytesWrite);
                    return totalBytesWrite += bytesWrite;
                }
                // 找到CRNL 替换 成 NLNL， [head ... CRNL] => [head ... NLNL]
                // 写入[head ... NL], curr跳过NLNL,
                buffer[curr] = OpUtil.LF;
                fs.Write(buffer, head, bytesWrite);
                // 游标置于当前位置
                head = curr += 2;
                totalBytesWrite += bytesWrite;

            } while (head < count);

            return totalBytesWrite;
        }

        private bool IsTGZ(byte[] buffer, int i)
        {
            return buffer[i] == 0x1f && buffer[i + 1] == 0x8b && buffer[i + 2] == 0x08;
        }

        private bool IsCRLF(byte[] buffer, int i)
        {
            return buffer[i] == OpUtil.CR && buffer[i + 1] == OpUtil.LF;
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

        // 针对shell中的文本信息去掉各种格式化字符串
        public static String FormatShellString(String s)
        {
            return String.Empty;
        }

    }

}
