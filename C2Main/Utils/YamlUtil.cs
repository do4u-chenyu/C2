using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace C2.Utils
{
    class YamlUtil
    {

        public static Dictionary<object, object> YamlStringToDictionary(string yamlString)
        {

            Dictionary<object, object> ret = new Dictionary<object, object>();
            if (string.IsNullOrEmpty(yamlString))
                return ret;

            try 
            {
                ret = new DeserializerBuilder().Build().
                                                Deserialize<Dictionary<object, object>>(yamlString);
            }
            catch { }

            return ret;
        }
    }
}
