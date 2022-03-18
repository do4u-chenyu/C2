using System;
using System.Collections.Generic;
using System.Reflection;
namespace Shadowsocks.Encryption
{
    public static class EncryptorFactory
    {
        private static Dictionary<string, Type> _registeredEncryptors;

        private static Type[] _constructorTypes = new Type[] { typeof(string), typeof(string) };

        static EncryptorFactory()
        {   // 三大类? Table, PolarSSL, Sodium
            _registeredEncryptors = new Dictionary<string, Type>();
            foreach (string method in TableEncryptor.SupportedCiphers())
            {
                _registeredEncryptors.Add(method, typeof(TableEncryptor));
            }
            foreach (string method in PolarSSLEncryptor.SupportedCiphers())
            {
                _registeredEncryptors.Add(method, typeof(PolarSSLEncryptor));
            }
            foreach (string method in SodiumEncryptor.SupportedCiphers())
            {
                _registeredEncryptors.Add(method, typeof(SodiumEncryptor));
            }
        }
        // 反射一个IEncryptor新对象出来并返回
        public static IEncryptor GetEncryptor(string method, string password)
        {
            if (string.IsNullOrEmpty(method))
            {
                method = "table";
            }
            method = method.ToLowerInvariant();
            Type t = _registeredEncryptors[method];
            ConstructorInfo c = t.GetConstructor(_constructorTypes);// 十几年了,我还是看这种方式觉得丑陋
            IEncryptor result = (IEncryptor)c.Invoke(new object[] { method, password });
            return result;
        }
    }
}
