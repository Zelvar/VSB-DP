using StaticAnalysisProject.Helpers.Hash;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;
using StaticAnalysisProject.Helpers;

namespace StaticAnalysisProject.Modules
{
    public class Hashes : IModul
    {
        private IDictionary<string, string> hashes = new Dictionary<string, string>();
        private string filename = null;
        private byte[] file = null;

        #region Default props
        public string GetModulDescription() => "This modul create all avaible hashes";

        public string GetModulName() => "Hashes";
        #endregion
        #region Modul code
        /// <summary>
        /// Constructor that load file
        /// </summary>
        /// <param name="filename">Path to file</param>
        public Hashes(string filename) 
            : this(File.ReadAllBytes(filename)) 
        {
            this.filename = filename;
        }

        /// <summary>
        /// Constructor that load byte array and calc hashes
        /// </summary>
        /// <param name="file">Input byte array of file</param>
        public Hashes(byte[] file)
        {
            this.file = file;
            var hashType = typeof(IHash);
            var types = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(
                    a => hashType.IsAssignableFrom(a)
                );

            types = types.Where(a => a.Name.ToString() != "IHash");
            
            foreach(var type in types)
            {
                var hash = Activator.CreateInstance(type);
                var method = type.GetMethod(
                    "GetHash",
                    new Type[]{
                            typeof(byte[])
                    }
                );

                string invoke = (string)(method.Invoke(hash, new object[] { file }));
                hashes.Add(hash.GetType().Name.ToString(), invoke);
            }
        }

        /// <summary>
        /// Getter for list of hashes
        /// </summary>
        /// <returns>Returns type of hash and calculated hash for byte array of file</returns>
        public IDictionary<string, string> GetHashes() => hashes;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var hash in this.GetHashes())
            {
                sb.AppendFormat("{0}: {1}", hash.Key.ToLower(), hash.Value);
                sb.AppendLine();
            }

            return sb.ToString();
        } 
        #endregion
    }
}
