using System.IO;
using System.Text;

namespace StaticAnalysisProject.Helpers.Hash
{
    public class MD5 : IHash
    {
        public string GetHash(string filePath)
        {
            StringBuilder sb = new StringBuilder();
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    foreach (byte b in md5.ComputeHash(stream)) sb.AppendFormat("{0:x2}", b);
                }
            }

            return sb.ToString();
        }

        public string GetHash(byte[] file)
        {
            StringBuilder sb = new StringBuilder();
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                foreach (byte b in md5.ComputeHash(file)) sb.AppendFormat("{0:x2}", b);
            }

            return sb.ToString();
        }
    }
}
