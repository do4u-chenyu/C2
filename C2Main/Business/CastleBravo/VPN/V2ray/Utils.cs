

namespace v2rayN
{
    // 这是一个为了跟v2ray源码移植兼容的临时类
    class Utils
    {
        public static bool IsNullOrEmpty(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return true;
            }
            if (text.Equals("null"))
            {
                return true;
            }
            return false;
        }
    }
}
