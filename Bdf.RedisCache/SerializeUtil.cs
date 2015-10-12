using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Bdf.RedisCache
{
    public class SerializeUtil
    {
        public static byte[] Serialize(object data)
        {
            var formatter = new BinaryFormatter();
            var rems = new MemoryStream();
            formatter.Serialize(rems, data);
            return rems.GetBuffer();
        }

        public static object Deserialize(byte[] data)
        {
            using (var memoryStream = new MemoryStream(data))
            {
                return new BinaryFormatter().Deserialize(memoryStream);
            }
        }
    }
}