using System.IO;
using System;
using System.Collections.Generic;

using StaticAnalysisProject.Modules;

namespace StaticAnalysisProject
{
    public class AFile
    {
        private string filePath = null;
        private byte[] fileLoaded = null;

        private Hashes hashesInstance = null;

        /// <summary>
        /// Constructor that load file
        /// </summary>
        /// <param name="filePath">File path for analysis</param>
        public AFile(string filePath) : this(File.ReadAllBytes(filePath))
        {
            this.filePath = filePath;
        }

        /// <summary>
        /// Constructur that load byte array of file
        /// </summary>
        /// <param name="file">File </param>
        public AFile(byte[] file)
        {
            if (file != null) {
                fileLoaded = file;
            }else{
                throw new Exception("File is empty");
            }
        }

        public IDictionary<string, string> GetHashes()
        {
            if(hashesInstance == null)
                hashesInstance = new Hashes(this.fileLoaded);

            return hashesInstance.GetHashes();
        }

    }
}
