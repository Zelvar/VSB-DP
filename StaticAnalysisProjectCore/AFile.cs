using System.IO;
using StaticAnalysisProject.Classes;
using StaticAnalysisProject.Modules;
using StaticAnalysisProject.Output;
using StaticAnalysisProject.Helpers.Hash;
using System.Diagnostics;
using System;
using System.Collections.Generic;

namespace StaticAnalysisProject
{
    public class AFile
    {
        private string filePath = null;
        private byte[] fileLoaded = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath">File path for analysis</param>
        public AFile(string filePath)
        {
            this.filePath = filePath;

            if (File.Exists(filePath)) //Check if file exists on HDD
            {
                fileLoaded = File.ReadAllBytes(filePath);
            } else {
                throw new Exception("File not found");
            }

            if(fileLoaded.Length < 0 && fileLoaded != null) //Check if file is not empty
            {

            } else {
                throw new Exception("File is empty");
            }
        }

        /// <summary>
        /// 
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
            new Hashes(this.fileLoaded);


            return null;
        }

    }
}
