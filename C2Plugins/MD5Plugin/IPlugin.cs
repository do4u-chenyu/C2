using System;
using System.Drawing;
using System.Windows.Forms;

namespace C2.IAOLab.Plugins
{
    interface IPlugin
    {
        String GetPluginName();
        String GetPluginDescription();
        String GetPluginVersion();
        Image GetPluginImage(); 
        DialogResult ShowFormDialog();
        //加密
        void Md5Code_128(string str);
        void Md5Code_64(string str);
        void EncodeBase64(string filePath);
        void UrlEncode(string url);
        void UnicodeChineseEncode(string str);
        void HexEncode(string str);
        void AES128Encode(string EncryptStr, string Key, string iv);
        void SHA1Encrypt(string str);
        void SHA256Encrypt(string str);
        void SHA512Encrypt(string str);
        void NTLMEncrypt(string str);
        //解密
        void DecodeBase64(string base64Str);
        void UrlDecode(string url);
        void UnicodeChineseDecode(string str);
        void HexDecode(string str);
        void AES128Decode(string DecryptStr, string Key);
    }
}
