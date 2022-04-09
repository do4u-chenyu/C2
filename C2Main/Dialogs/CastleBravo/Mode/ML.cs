using C2.Core;

namespace C2.Dialogs.CastleBravo.Mode
{
    class ML
    {
        // 模式01: MD5($Pass.$Salt)
        public static string MD5_Pass_Salt(string pass, string salt, string u) 
        {
            return ST.GenerateMD5(pass + salt);
        }
        // 模式02: MD5($Salt.$Pass)
        public static string MD5_Salt_Pass(string pass, string salt, string u) 
        {
            return ST.GenerateMD5(salt + pass);
        }
        // 模式03: MD5($Salt.$Pass.$Salt)
        public static string MD5_Salt_Pass_Salt(string pass, string salt, string u)
        { 
            return ST.GenerateMD5(salt + pass + salt);
        }
        // 模式04: MD5(MD5($Pass).$Salt)
        public static string MD5_MD5_Pass_Salt(string pass, string salt, string u) 
        {
            return ST.GenerateMD5(ST.GenerateMD5(pass) + salt);
        }
        // 模式05: MD5($Salt.MD5($Pass))
        public static string MD5_Salt_MD5_Pass(string pass, string salt, string u) 
        {
            return ST.GenerateMD5(salt + ST.GenerateMD5(pass));
        }
        // 模式06: MD5(MD5($Pass.$Salt))
        public static string MD5_MD5_Pass_Salt_2(string pass, string salt, string u) 
        { 
            return ST.GenerateMD5(ST.GenerateMD5(pass + salt)); 
        }
        // 模式07: MD5(MD5($Salt.$Pass))
        public static string MD5_MD5_Salt_Pass(string pass, string salt, string u) 
        {
            return ST.GenerateMD5(ST.GenerateMD5(salt + pass));
        }
        // 模式08: MD5(MD5($Pass).MD5($Salt))
        public static string MD5_MD5_Pass_MD5_Salt(string pass, string salt, string u) 
        { 
            return ST.GenerateMD5(ST.GenerateMD5(pass) + ST.GenerateMD5(salt));
        }
        // 模式09: MD5(MD5($Salt).MD5($Pass))
        public static string MD5_MD5_Salt_MD5_Pass(string pass, string salt, string u) 
        { 
            return ST.GenerateMD5(ST.GenerateMD5(salt) + ST.GenerateMD5(pass));
        }
        // 模式10: MD5($Salt.MD5($Salt.$Pass))
        public static string MD5_Salt_MD5_Salt_Pass(string pass, string salt, string u) 
        { 
            return ST.GenerateMD5(salt + ST.GenerateMD5(salt + pass)); 
        }
        // 模式11: MD5($Salt.MD5($Pass.$Salt))
        public static string MD5_Salt_MD5_Pass_Salt(string pass, string salt, string u) 
        {
            return ST.GenerateMD5(salt + ST.GenerateMD5(pass + salt));
        }
        // 模式12: MD5(MD5($Salt.$Pass).$Salt)
        public static string MD5_MD5_Salt_Pass_Salt(string pass, string salt, string u) 
        {
            return ST.GenerateMD5(ST.GenerateMD5(salt + pass) + salt);
        }
        // 模式13: MD5(MD5($Pass.$Salt).$Salt)
        public static string MD5_MD5_Pass_Salt_Salt(string pass, string salt, string u) 
        {
            return ST.GenerateMD5(ST.GenerateMD5(pass + salt) + salt);
        }
        // 模式14: MD5($U.$Pass.$Salt)
        public static string MD5_U_Pass_Salt(string pass, string salt, string u) 
        {
            return ST.GenerateMD5(u + pass + salt);
        }
        // 模式15: MD5($U.$Salt.$Pass)
        public static string MD5_U_Salt_Pass(string pass, string salt, string u) 
        {
            return ST.GenerateMD5(u + salt + pass);
        }

        // 模式16: MD5($Salt.$U.$Pass)
        public static string MD5_Salt_U_Pass(string pass, string salt, string u) 
        {
            return ST.GenerateMD5(salt + u + pass);
        }
        
        // 模式17: MD5($Salt.$Pass.$U)
        public static string MD5_Salt_Pass_U(string pass, string salt, string u) 
        { 
            return ST.GenerateMD5(salt + pass + u);
        }
        // 模式18: MD5($Pass.$Salt.$U)
        public static string MD5_Pass_Salt_U(string pass, string salt, string u) 
        {
            return ST.GenerateMD5(pass + salt + u);
        }
        // 模式19: MD5($Pass.$U.$Salt)
        public static string MD5_Pass_U_Salt(string pass, string salt, string u) 
        {
            return ST.GenerateMD5(pass + u + salt);
        }

        // 模式20: MD5($U.$Pass.MD5($Salt))
        public static string MD5_U_Pass_MD5_Salt(string pass, string salt, string u) 
        {
            return ST.GenerateMD5(u + pass + ST.GenerateMD5(salt));
        }
        // 模式21: MD5($U.MD5($Pass).$Salt)
        public static string MD5_U_MD5_Pass_Salt(string pass, string salt, string u) 
        { 
            return ST.GenerateMD5(u + ST.GenerateMD5(pass) + salt);
        }
        // 模式22: MD5(MD5($U).$Pass.$Salt)
        public static string MD5_MD5_U_Pass_Salt(string pass, string salt, string u) 
        { 
            return ST.GenerateMD5(ST.GenerateMD5(u) + pass + salt);
        }
        // 模式23: MD5(MD5($U.$Pass).$Salt)
        public static string MD5_MD5_U_Pass_Salt_2(string pass, string salt, string u) 
        {
            return ST.GenerateMD5(ST.GenerateMD5(u + pass) + salt);
        }
        // 模式24: MD5(MD5($U.$Pass.$Salt))
        public static string MD5_MD5_U_Pass_Salt_3(string pass, string salt, string u) 
        {
            return ST.GenerateMD5(ST.GenerateMD5(u + pass + salt));
        }
        // 模式25: MD5(MD5($U.MD5($Pass)).$Salt)
        public static string MD5_MD5_U_MD5_Pass_Salt(string pass, string salt, string u) 
        {
            return ST.GenerateMD5(ST.GenerateMD5(u + ST.GenerateMD5(pass)) + salt);
        }
        // 模式26: MD5(MD5($U).MD5($Pass).MD5($Salt))
        public static string MD5_MD5_U_MD5_Pass_MD5_Salt(string pass, string salt, string u) 
        {
            return ST.GenerateMD5(ST.GenerateMD5(u) + ST.GenerateMD5(pass) + ST.GenerateMD5(salt));
        }
        // 模式27: MD5(MD5($U).$Pass.MD5($Salt))
        public static string MD5_MD5_U_Pass_MD5_Salt(string pass, string salt, string u) 
        {
            return ST.GenerateMD5(ST.GenerateMD5(u) + pass + ST.GenerateMD5(salt));
        }
        // 模式28: MD5($U.MD5($Pass).MD5($Salt))
        public static string MD5_U_MD5_Pass_MD5_Salt(string pass, string salt, string u) 
        {
            return ST.GenerateMD5(u + ST.GenerateMD5(pass) + ST.GenerateMD5(salt));
        }
        // 模式29: MD5($U.MD5($Pass.$Salt))
        public static string MD5_U_MD5_Pass_Salt_2(string pass, string salt, string u)
        {
            return ST.GenerateMD5(u + ST.GenerateMD5(pass + salt));
        }
        // 模式30: MD5(MD5($Salt).$Pass)
        public static string MD5_MD5_Salt_Pass_2(string pass, string salt, string u) 
        {
            return ST.GenerateMD5(ST.GenerateMD5(salt) + pass);
        }
        // 模式31: MD5(SHA1($Pass))
        public static string MD5_SHA1(string pass, string salt, string u) 
        { 
            return ST.GenerateMD5(ST.SHA1(pass)); 
        }
        // 模式32: MD5(SHA256($Pass))
        public static string MD5_SHA256(string pass, string salt, string u) 
        {
            return ST.GenerateMD5(ST.SHA256(pass));
        }
        // 模式33: MD5(SHA512($Pass))
        public static string MD5_SHA512(string pass, string salt, string u) 
        {
            return ST.GenerateMD5(ST.SHA512(pass));
        }
    }
}
