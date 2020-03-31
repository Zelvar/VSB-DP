using System.IO;
using StaticAnalysisProject.Classes;
using StaticAnalysisProject.Modules;
using StaticAnalysisProject.Output;
using StaticAnalysisProject.Helpers.Hash;
using System.Diagnostics;
using System;

namespace StaticAnalysisProject
{
    public class AFile
    {
        private string filePath;

        public AFile(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            if (!fi.Exists) throw new FileNotFoundException();

            Console.WriteLine(fi.Name);

            /*this.md5 = MD5.GetHash(filePath);
            this.sha1 = SHA1.GetHash(filePath);
            this.sha256 = SHA256.GetHash(filePath);
            this.sha384 = SHA384.GetHash(filePath);
            this.sha512 = SHA512.GetHash(filePath);*/



        }
    }
}
