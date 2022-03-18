using MD5Plugin;
using MD5Plugin.Properties;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Shadowsocks.Encryption
{
    public class PolarSSL
    {
        const string DLLNAME = "libsscrypto";
        // 不知道这个是干嘛用的
        public const int AES_CTX_SIZE = 8 + 4 * 68;  // 280
        public const int AES_ENCRYPT = 1;
        public const int AES_DECRYPT = 0;

        static PolarSSL()
        {   // libsscrypto.dll作为资源嵌入,运行时解压缩
            string tempPath = Path.GetTempPath();
            string dllPath = tempPath + "/libsscrypto.dll";
            try
            {
                Utils.UncompressFile(dllPath, Resources.libsscrypto_dll);
            }
            catch (IOException)
            {
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            LoadLibrary(dllPath);  // 加载libsscrypto.dll
        }

        [DllImport("Kernel32.dll")]
        private static extern IntPtr LoadLibrary(string path);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static void aes_init(IntPtr ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static void aes_free(IntPtr ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static int aes_setkey_enc(IntPtr ctx, byte[] key, int keysize);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static int aes_crypt_cfb128(IntPtr ctx, int mode, int length, ref int iv_off, byte[] iv, byte[] input, byte[] output);

        // 不知道这个是干嘛用的
        public const int ARC4_CTX_SIZE = 264; // 4 + 4 + 256, PolaRSSL中arc4_context结构体的长度

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static void arc4_init(IntPtr ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static void arc4_free(IntPtr ctx);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static void arc4_setup(IntPtr ctx, byte[] key, int keysize);

        [DllImport(DLLNAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static int arc4_crypt(IntPtr ctx, int length, byte[] input, byte[] output);

    }
}
