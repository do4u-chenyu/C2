﻿using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace MD5Plugin
{
    public partial class NTLMPlugin : CommonHashPlugin
    {
        public NTLMPlugin() : base()
        {
            InitializeComponent();
        }

        public abstract class MD4 : HashAlgorithm
        {
            static MD4()
            {
                CryptoConfig.AddAlgorithm(typeof(MD4CryptoServiceProvider), "System.Security.Cryptography.MD4");
            }

            protected MD4()
            {
                HashSizeValue = 128;
            }

            new static public MD4 Create()
            {
                return Create("System.Security.Cryptography.MD4");
            }

            new static public MD4 Create(string algName)
            {
                return (MD4)CryptoConfig.CreateFromName(algName);
            }
        }

        [System.Runtime.InteropServices.ComVisible(true)]
        public sealed class MD4CryptoServiceProvider : MD4
        {
            internal static class Utils
            {
                internal static Type UtilsType = Type.GetType("System.Security.Cryptography.Utils");

                public static T InvokeInternalMethodOfType<T>(object o, object pType, string methodName, params object[] args)
                {
                    var internalType = (pType is string internalTypeName) ? Type.GetType(internalTypeName) : (Type)pType;
                    var internalMethods = internalType.GetMethods(BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | (o == null ? BindingFlags.Static : 0));
                    var internalMethod = internalMethods.Where(m => m.Name == methodName && m.GetParameters().Length == args.Length).Single();
                    return (T)internalMethod?.Invoke(o, args);
                }

                public static T GetInternalPropertyValueOfInternalType<T>(object o, object pType, string propertyName)
                {
                    var internalType = (pType is string internalTypeName) ? Type.GetType(internalTypeName) : (Type)pType;
                    var internalProperty = internalType.GetProperty(propertyName, BindingFlags.NonPublic | (o == null ? BindingFlags.Static : 0));
                    return (T)internalProperty.GetValue(o);
                }

                internal static SafeHandle CreateHash(int algid)
                {
                    return InvokeInternalMethodOfType<SafeHandle>(null, UtilsType, "CreateHash", GetInternalPropertyValueOfInternalType<object>(null, UtilsType, "StaticProvHandle"), algid);
                }

                internal static void HashData(SafeHandle h, byte[] data, int ibStart, int cbSize)
                {
                    InvokeInternalMethodOfType<object>(null, UtilsType, "HashData", h, data, ibStart, cbSize);
                }

                internal static byte[] EndHash(SafeHandle h)
                {
                    return InvokeInternalMethodOfType<byte[]>(null, UtilsType, "EndHash", h);
                }
            }

            internal const int ALG_CLASS_HASH = (4 << 13);
            internal const int ALG_TYPE_ANY = (0);
            internal const int ALG_SID_MD4 = 2;
            internal const int CALG_MD4 = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_MD4);

            [System.Security.SecurityCritical]
            private SafeHandle _safeHashHandle = null;

            [System.Security.SecuritySafeCritical]
            public MD4CryptoServiceProvider()
            {
                if (CryptoConfig.AllowOnlyFipsAlgorithms)
                    throw new InvalidOperationException("Cryptography_NonCompliantFIPSAlgorithm");
                Contract.EndContractBlock();
                // cheat with Reflection
                _safeHashHandle = Utils.CreateHash(CALG_MD4);
            }

            protected override void Dispose(bool disposing)
            {
                if (_safeHashHandle != null && !_safeHashHandle.IsClosed)
                    _safeHashHandle.Dispose();
                base.Dispose(disposing);
            }

            public override void Initialize()
            {
                if (_safeHashHandle != null && !_safeHashHandle.IsClosed)
                    _safeHashHandle.Dispose();

                _safeHashHandle = Utils.CreateHash(CALG_MD4);
            }

            protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
            {
                Utils.HashData(_safeHashHandle, rgb, ibStart, cbSize);
            }

            protected override byte[] HashFinal()
            {
                return Utils.EndHash(_safeHashHandle);
            }
        }

        public static class NTLM
        {
            public static HashAlgorithm MD4Singleton;
            static NTLM()
            {
                MD4Singleton = MD4.Create();
            }
            private static byte[] ComputeHash(string s)
            {
                return MD4Singleton.ComputeHash(Encoding.Unicode.GetBytes(s));
            }
            public static string ComputeHashHexString(string s)
            {
                return String.Join("", ComputeHash(s).Select(h => h.ToString("x2")));
            }
        }

        public override void Encode(string str)
        {
            if (inputTextBox.Text == "请把你需要加密的内容粘贴在这里")
            {
                ResetTextBox();
            }
            else
            {
                outputTextBox.Text = NTLM.ComputeHashHexString(str);
            }
        }
    }
}