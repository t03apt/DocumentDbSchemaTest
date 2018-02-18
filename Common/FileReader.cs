using System.IO;

namespace Experimental.Tools.CosmoDb.Common
{
    public class FileReader
    {
        public string ReadAll(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            using (var stream = fileInfo.OpenRead())
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
