using System.IO;

namespace Bdf.IO.Extensions
{
    /// <summary>
    /// The helper class of Stream class
    /// </summary>
    public static class StreamExtensions
    {
        public static byte[] GetAllBytes(this Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}