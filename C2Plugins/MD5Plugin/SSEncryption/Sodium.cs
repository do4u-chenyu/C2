using MD5Plugin;
using MD5Plugin.Properties;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Shadowsocks.Encryption
{
    public class Sodium
    {
        const string DLLNAME = "libsscrypto";

        static Sodium()
        {
            string tempPath = Path.GetTempPath();
            string dllPath = tempPath + "/libsscrypto.dll";
            try
            {
                Utils.UncompressFile(dllPath, Resources.libsscrypto_dll);
                LoadLibrary(dllPath);
            }
            catch (IOException)
            {
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            LoadLibrary(dllPath);
        }

        [DllImport("Kernel32.dll")]
        private static extern IntPtr LoadLibrary(string path);
        // 都是libsodium的函数,具体看网上libsodium的文档
        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static void crypto_stream_salsa20_xor_ic(byte[] c, byte[] m, ulong mlen, byte[] n, ulong ic, byte[] k);
        // The crypto_stream_chacha20_xor_ic() function is similar to crypto_stream_chacha20_xor() but adds the ability to set the initial value of the block counter to a non-zero value, ic.
        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static void crypto_stream_chacha20_xor_ic(byte[] c, byte[] m, ulong mlen, byte[] n, ulong ic, byte[] k);
    }
}
