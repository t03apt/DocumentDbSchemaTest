using System.IO;

namespace Experimental.Tools.CosmoDb.Common
{
    public class FileWriter
    {
        public void SaveToFile(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
        }
    }
}
