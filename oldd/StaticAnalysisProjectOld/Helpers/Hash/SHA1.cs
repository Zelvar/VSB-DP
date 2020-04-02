using System.IO;
using System.Text;

namespace StaticAnalysisProject.Helpers.Hash
{
    public class SHA1 : IHash
    {
        public static string GetHash(string filePath)
        {
            StringBuilder sb = new StringBuilder();
            using (var md5 = System.Security.Cryptography.SHA1.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    foreach (byte b in md5.ComputeHash(stream)) sb.AppendFormat("{0:x2}", b);
                }
            }

            return sb.ToString();
        }
    }
}
