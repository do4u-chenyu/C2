using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Shadowsocks.Encryption
{
    public abstract class IVEncryptor
        : EncryptorBase
    {
        protected static byte[] tempbuf = new byte[MAX_INPUT_SIZE];

        protected Dictionary<string, int[]> ciphers;//密码本

        private static readonly Dictionary<string, byte[]> CachedKeys = new Dictionary<string, byte[]>();
        protected byte[] _encryptIV;
        protected byte[] _decryptIV;
        protected bool _decryptIVReceived; // 第一个报文要把IV头越过
        protected bool _encryptIVSent;     // 第一个报文要把IV塞里面
        protected int _encryptIVOffset = 0;
        protected int _decryptIVOffset = 0;
        protected string _method;
        protected int _cipher;
        protected int[] _cipherInfo;       // keyLen,ivLen,cipher,ctx的结构体长度,
        protected byte[] _key; // 明文密码的实际内存存储 md5(password)+md5(md5(password)+password)
        protected int keyLen;  // 分段长度
        protected int ivLen;   // IV向量长度


        public IVEncryptor(string method, string password)
            : base(method, password)
        {
            InitKey(method, password);
        }

        protected abstract Dictionary<string, int[]> getCiphers();

        protected void InitKey(string method, string password)
        {  // 从已有的Cihper中选择一个
            method = method.ToLower();
            _method = method;
            string k = method + ":" + password;
            ciphers = getCiphers();//约定接口
            _cipherInfo = ciphers[_method];  // 居然用int数组当信息类,是为了好序列化?
            _cipher = _cipherInfo[2];
            if (_cipher == 0)
            {
                throw new Exception("method not found");
            }
            keyLen = ciphers[_method][0];  // keyLen,ivLen,cipher
            ivLen = ciphers[_method][1];
            if (CachedKeys.ContainsKey(k)) // 密码缓存
            {
                _key = CachedKeys[k];
            }
            else
            {
                byte[] passbuf = Encoding.UTF8.GetBytes(password);
                _key = new byte[32];        //key是256位, 64个字母
                byte[] iv = new byte[16]; 
                bytesToKey(passbuf, _key);  //根据password算出key,然后缓存起来,
                CachedKeys[k] = _key;       //md5(password)+md5(md5(password)+password)
            }                               //这么复杂,估计是怕dump内存
        }

        protected void bytesToKey(byte[] password, byte[] key)
        {
            byte[] result = new byte[password.Length + 16];
            int i = 0;
            byte[] md5sum = null;
            while (i < key.Length)
            {
                MD5 md5 = MD5.Create();
                if (i == 0)
                {
                    md5sum = md5.ComputeHash(password);
                }
                else
                {
                    md5sum.CopyTo(result, 0);
                    password.CopyTo(result, md5sum.Length);
                    md5sum = md5.ComputeHash(result);
                }
                md5sum.CopyTo(key, i);
                i += md5sum.Length;
            }
        }

        protected static void randBytes(byte[] buf, int length)
        {
            byte[] temp = new byte[length];
            new Random().NextBytes(temp);
            temp.CopyTo(buf, 0);
        }
        // 底层接口没有IV这个概念,这层开个接口给IV设置
        protected virtual void initCipher(byte[] iv, bool isCipher)
        {
            if (ivLen > 0)
            {
                if (isCipher)
                {
                    _encryptIV = new byte[ivLen];
                    Array.Copy(iv, _encryptIV, ivLen);
                }
                else
                {
                    _decryptIV = new byte[ivLen];
                    Array.Copy(iv, _decryptIV, ivLen);
                }
            }
        }
        // 真正的加密函数
        protected abstract void cipherUpdate(bool isCipher, int length, byte[] buf, byte[] outbuf);

        public override void Encrypt(byte[] buf, int length, byte[] outbuf, out int outlength)
        {
            if (!_encryptIVSent) // IV难道要搞成一次性的? 第一次要把IV也塞里面
            {
                _encryptIVSent = true;
                randBytes(outbuf, ivLen);  // 生成IV, 塞到outbuf最前面,每次都能拿到
                initCipher(outbuf, true);  // 设置IV
                outlength = length + ivLen;
                lock (tempbuf)
                {
                    cipherUpdate(true, length, buf, tempbuf);
                    outlength = length + ivLen;
                    Buffer.BlockCopy(tempbuf, 0, outbuf, ivLen, length);
                }
            }
            else   // 后续的直接加密
            {
                outlength = length;
                cipherUpdate(true, length, buf, outbuf);
            }
        }

        public override void Decrypt(byte[] buf, int length, byte[] outbuf, out int outlength)
        {
            if (!_decryptIVReceived)//第一个报文要去掉IV头
            {
                _decryptIVReceived = true;
                initCipher(buf, false);
                outlength = length - ivLen;
                lock (tempbuf)
                {
                    // C# could be multi-threaded
                    Buffer.BlockCopy(buf, ivLen, tempbuf, 0, length - ivLen);
                    cipherUpdate(false, length - ivLen, tempbuf, outbuf);
                }
            }
            else
            {
                outlength = length;
                cipherUpdate(false, length, buf, outbuf);
            }
        }
    }
}
