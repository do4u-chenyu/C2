using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2.Business.CastleBravo.Binary
{
    class XiseDES
    {
        // 密钥置换表
        private readonly byte[] __pc1 = new byte[]
        {
            56, 48, 40, 32, 24, 16,  8,
            0, 57, 49, 41, 33, 25, 17,
            9,  1, 58, 50, 42, 34, 26,
            18, 10,  2, 59, 51, 43, 35,
            62, 54, 46, 38, 30, 22, 14,
            6, 61, 53, 45, 37, 29, 21,
            13,  5, 60, 52, 44, 36, 28,
            20, 12,  4, 27, 19, 11,  3
        };

        // 每轮循环左移位数
        private readonly byte[] __left_rotations = new byte[] { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };

        // 压缩置换
        private readonly byte[] __pc2 = new byte[]
        {
            13, 16, 10, 23,  0,  4,
             2, 27, 14,  5, 20,  9,
            22, 18, 11,  3, 25,  7,
            15,  6, 26, 19, 12,  1,
            40, 51, 30, 36, 46, 54,
            29, 39, 50, 44, 32, 47,
            43, 48, 38, 55, 33, 52,
            45, 41, 49, 35, 28, 31
        };

        // 初始置换IP
        private readonly byte[] __ip = new byte[]
        {
            57, 49, 41, 33, 25, 17, 9,  1,
            59, 51, 43, 35, 27, 19, 11, 3,
            61, 53, 45, 37, 29, 21, 13, 5,
            63, 55, 47, 39, 31, 23, 15, 7,
            56, 48, 40, 32, 24, 16, 8,  0,
            58, 50, 42, 34, 26, 18, 10, 2,
            60, 52, 44, 36, 28, 20, 12, 4,
            62, 54, 46, 38, 30, 22, 14, 6
        };

        // 扩展变换
        private readonly byte[] __expansion_table = new byte[]
        {
             31,  0,  1,  2,  3,  4,
             3,  4,  5,  6,  7,  8,
             7,  8,  9, 10, 11, 12,
            11, 12, 13, 14, 15, 16,
            15, 16, 17, 18, 19, 20,
            19, 20, 21, 22, 23, 24,
            23, 24, 25, 26, 27, 28,
            27, 28, 29, 30, 31,  0
        };

        // S 盒
        private readonly byte[,] __sbox = new byte[8, 64]
        {  
            // S1
            {14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7,
             0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8,
             4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0,
             15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13 },

            // S2
            {15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10,
             3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5,
             0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15,
             13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9},

            // S3
            {10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8,
             13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1,
             13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7,
             1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12 },

            // S4
            {7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15,
             13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9,
             10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4,
             3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14},

            // S5
            { 2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9,
             14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6,
             4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14,
             11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3},

            // S6
            {12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11,
            10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8,
             9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6,
             4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13 },

            // S7
            {4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1,
             13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6,
             1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2,
             6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12 },

            // S8
            {13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7,
             1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2,
             7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8,
             2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11}
        };

        // P-盒置换
        private readonly byte[] __p = new byte[]
        {
            15, 6, 19, 20, 28, 11,
            27, 16, 0, 14, 22, 25,
            4, 17, 30, 9, 1, 7,
            23,13, 31, 26, 2, 8,
            18, 12, 29, 5, 21, 10,
            3, 24
        };

        private readonly byte[] __fp = new byte[]
        {
            39,  7, 47, 15, 55, 23, 63, 31,
            38,  6, 46, 14, 54, 22, 62, 30,
            37,  5, 45, 13, 53, 21, 61, 29,
            36,  4, 44, 12, 52, 20, 60, 28,
            35,  3, 43, 11, 51, 19, 59, 27,
            34,  2, 42, 10, 50, 18, 58, 26,
            33,  1, 41,  9, 49, 17, 57, 25,
            32,  0, 40,  8, 48, 16, 56, 24
        };

        private byte[] __Bytes_to_BitList(IList<byte> data)
        {
            // Turn the byte[] data,into a list of bits
            int sl = data.Count * 8;
            int pos = 0;
            byte[] result = new byte[sl];  // 共有 sl 位
            foreach (byte ch in data)
            {
                int i = 7;
                while (i >= 0)
                {
                    if ((ch & (1 << i)) != 0)
                        result[pos] = 1;
                    else
                        result[pos] = 0;

                    pos += 1;
                    i -= 1;
                }
            }

            return result;
        }

        private byte[] __BitList_to_Bytes(IList<byte> data)
        {
            // Turn the list of bits(data) into a byte[]
            List<byte> result = new List<byte>();
            int pos = 0;
            int c = 0;

            while (pos < data.Count)
            {
                c += data[pos] << (7 - (pos % 8));
                if (pos % 8 == 7)
                {
                    result.Add((byte)c);
                    c = 0;
                }
                pos += 1;
            }

            return result.ToArray();
        }

        // 用指定的table置换block
        private byte[] Permutata(byte[] table, IList<byte> block)
        {
            byte[] ret = new byte[table.Length];
            for (int i = 0; i < table.Length; i++)
                ret[i] = block[table[i]];
            return ret;
        }

        // 16 * 48
        private List<byte[]> CreateKeys(byte[] K1)
        {
            List<byte[]> subKey = new List<byte[]>();
            // 转换成56位
            byte[] key1 = Permutata(__pc1, __Bytes_to_BitList(K1));
            // 由K1 生成1到5轮的子密钥
            // 将key1分割左右各28位
            List<byte> L1 = key1.Take(28).ToList();
            List<byte> R1 = key1.Skip(28).ToList();

            for (int i = 0; i < 16; i++)
            {
                int j = 0;
                while (j < __left_rotations[i])
                {
                    L1.Add(L1[0]);
                    L1.RemoveAt(0);

                    R1.Add(R1[0]);
                    R1.RemoveAt(0);
                    j++;
                }
                // 压缩置换生成子密钥
                subKey.Add(Permutata(__pc2, L1.Concat(R1).ToArray()));
            }
            return subKey;
        }

        // block 是二进制块 64位
        private byte[] Des_DecryptBlock(byte[] block, byte[] K1)
        {
            // 初始IP置换
            block = Permutata(__ip, block);

            // 分块
            List<byte> L = block.Take(32).ToList();
            List<byte> R = block.Skip(32).ToList();

            int iteration = 15;
            int iteration_adjustment = -1;

            //# 异或函数
            //xorfun = lambda x,y: x ^ y
            // 生成子密钥
            List<byte[]> Kn = CreateKeys(K1);
            // 进行16轮加密
            for (int i = 0; i < 16; i++)
            {
                List<byte> tempR = new List<byte>(R);
                // 扩展置换
                R = Permutata(__expansion_table, R).ToList();
                // 与子密钥进行异或
                R = XorFun(R, Kn[iteration]);
                // 将R分割成 6 * 8
                List<byte[]> B = new List<byte[]> { 
                    R.Take(6).ToArray(),
                    R.Skip(6).Take(6).ToArray(),
                    R.Skip(12).Take(6).ToArray(),
                    R.Skip(18).Take(6).ToArray(),
                    R.Skip(24).Take(6).ToArray(),
                    R.Skip(30).Take(6).ToArray(),
                    R.Skip(36).Take(6).ToArray(),
                    R.Skip(42).Take(6).ToArray(),
                };
                // S盒代替 B[1] 到 B[8] 最终生成32位的Bn
                int pos = 0;
                byte[] Bn = new byte[32];
                for (int j = 0; j < 8; j++)
                {
                    // 计算行
                    int m = (B[j][0] << 1) + B[j][5];
                    // 计算列
                    int n = (B[j][1] << 3) + (B[j][2] << 2) + (B[j][3] << 1) + (B[j][4]);
                    // 得到S-盒 j的置换值
                    byte v = __sbox[j, (m << 4) + n];
                    // 转换成二进制
                    Bn[pos] = (byte)((v & 8) >> 3);
                    Bn[pos + 1] = (byte)((v & 4) >> 2);
                    Bn[pos + 2] = (byte)((v & 2) >> 1);
                    Bn[pos + 3] = (byte)(v & 1);
                    pos += 4;
                }
                R = Permutata(__p, Bn).ToList();  // P - 盒代替
                R = XorFun(R, L);                   // 与 L 异或
                L = tempR;
                iteration += iteration_adjustment;
            }


            // 最后IP 置换
            return Permutata(__fp, R.Concat(L).ToArray());
        }
        private byte[] DES_Decrypt(byte[] data, byte[] K1)
        {
            List<byte> result = new List<byte>();
            int i = 0;
            while (i < data.Length)
            {
                byte[] block = __Bytes_to_BitList(data.Skip(i).Take(8).ToArray());
                byte[] processed_block = Des_DecryptBlock(block, K1);
                result.AddRange(__BitList_to_Bytes(processed_block));
                i += 8;
            }
            return result.ToArray();
        }

        private byte[] Decrypt(byte[] data, byte[] K1)
        {
            if (K1.Length != 8)
            {
                Console.WriteLine("K1 不是 64 位的密钥");
                return new byte[0];
            }

            if (data == null || data.Length == 0)
                return new byte[0];

            if (data.Length % 8 != 0)
            {
                Console.WriteLine("Invalid data length,data must be a mutiple of 8 bytes");
                return new byte[0];
            }

            return DES_Decrypt(data, K1);
        }

        private List<byte> XorFun(IList<byte> L, IList<byte> R)
        {
            List<byte> ret = new List<byte>();
            for (int i = 0; i < L.Count; i++)
            {
                ret.Add((byte)(L[i] ^ R[i]));
            }
            return ret;
            
        }

        private byte[] XOR8(string pass)
        {
            int i = 0;
            byte[] bytes8 = new byte[8];
            byte[] bytesP = Encoding.Default.GetBytes(pass);
            foreach (byte b in bytesP)
            {
                bytes8[i] = (byte)(b ^ bytes8[i]);
                i = i < 7 ? i + 1 : 0;
            }
            return bytes8;
        }

        private byte[] XOR8(byte[] pass)
        {
            int i = 0;
            byte[] bytes8 = new byte[8];
            foreach (byte b in pass)
            {
                bytes8[i] = (byte)(b ^ bytes8[i]);
                i = i < 7 ? i + 1 : 0;
            }
            return bytes8;
        }

        
        public string XiseDecrypt(string plainText)
        {
            string[] ret = plainText.Split("~", StringSplitOptions.RemoveEmptyEntries);
            
            if (ret.Length < 2)
                return string.Empty;

            string pass = ret[0].Trim('?').Trim();
            string text = ret[1].Trim('?').Trim();

            byte[] pass_bytes = ST.DecimalHexStringToBytes(pass, "?");
            byte[] text_bytes = ST.DecimalHexStringToBytes(text, "?");
            if (text_bytes.Length % 8 != 0)
                return "格式错误:密文转换成字节数组后长度必须是8的整倍数" +
                    Environment.NewLine +
                    text; 

            pass_bytes = Decrypt(pass_bytes, ConvertUtil.ReverseBytes(XOR8("goklong soft")));
            pass_bytes = pass_bytes.Skip(4).ToArray(); // 去掉4位长度前缀

            text_bytes = Decrypt(text_bytes, ConvertUtil.ReverseBytes(XOR8(pass_bytes)));
            text_bytes = text_bytes.Skip(4).ToArray(); // 去掉4位长度前缀

            return Encoding.Default.GetString(text_bytes);
        }

        public string XiseHexDecrypt(string hexText)
        {
            string text = ST.HexToString(hexText);
            if (text.Contains("~") && text.Contains("?"))
                text = XiseDecrypt(text);
            else
                return text;

            if (text.Contains(@"{~x~}"))
            {
                string[] ret = text.Split(@"{~x~}", StringSplitOptions.RemoveEmptyEntries);
                if (ret.Length == 0)
                    return string.Empty;
                if (ret.Length == 1)
                    return ret[0];
                if (ret.Length < 2)
                    return string.Empty;
                
                string host = ret[0].Trim();
                string pass = ret[1].Trim().TrimEnd('\0');
                if (pass.Contains("~") && pass.Contains("?"))
                    pass = XiseDecrypt(pass);

                return string.Format("{0}\t{1}", host, pass);
            }
            return "格式错误";
        }
    }
}
