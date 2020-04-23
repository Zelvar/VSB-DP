using System.IO;
using System.Text;

namespace StaticAnalysisProject.Helpers.Hash
{
    public class SHA1 : IHash
    {
        public string GetHash(string filePath)
        {
            StringBuilder sb = new StringBuilder();
            using (var sha1 = System.Security.Cryptography.SHA1.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    foreach (byte b in sha1.ComputeHash(stream)) sb.AppendFormat("{0:x2}", b);
                }
            }

            return sb.ToString();
        }

        public string GetHash(byte[] file)
        {
            StringBuilder sb = new StringBuilder();
            using (var sha1 = System.Security.Cryptography.SHA1.Create())
            {
                foreach (byte b in sha1.ComputeHash(file)) sb.AppendFormat("{0:x2}", b);
            }

            return sb.ToString();
        }
    }
}
