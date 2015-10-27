using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Bdf.Json
{
    /// <summary>
    /// Josn Helper method
    /// </summary>
    public static class JsonHelper
    {
        public static string ConvertToJson(object obj, bool camelCase = false, bool indented = false)
        {
            var options = new JsonSerializerSettings();

            if (camelCase)
            {
                options.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            if (indented)
            {
                options.Formatting = Formatting.Indented;
            }

            return JsonConvert.SerializeObject(obj, options);
        }

        public static T ConvertJsonToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}