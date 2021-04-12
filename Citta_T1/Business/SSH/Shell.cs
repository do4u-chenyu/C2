using Renci.SshNet;
using System;


namespace C2.Business.SSH
{
    class Shell
    {
        public const long BufferSize = 4096 * 2;
        private ShellStream shell;

        public byte[] InComing { get; }
        public Shell(ShellStream ssm)
        {
            this.shell = ssm;
            InComing = new byte[BufferSize];
        }

        public int Read(int count, TimeSpan timeout)
        {
            return shell.Read(InComing, 0, count);
        }

    }
}
