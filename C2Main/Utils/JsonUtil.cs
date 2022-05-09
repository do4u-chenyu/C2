using Newtonsoft.Json;
using System.Collections.Generic;

namespace C2.Utils
{
    class JsonUtil
    {
        public static Dictionary<string, string> JsonStringToDictionary(string jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr))
                return new Dictionary<string, string>();

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonStr);
        }
    }
}
