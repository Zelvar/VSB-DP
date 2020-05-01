using StaticAnalysisProject.Helpers.Hash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace StaticAnalysisProject.Modules
{
    public class Hashes : IModul
    {
        #region DATA
        private IDictionary<string, string> hashes = new Dictionary<string, string>();
        private string _filename = null;
        private byte[] _file = null;
        #endregion
        #region Default props
        public string GetModulDescription() => "This modul create all avaible hashes";

        public string GetModulName() => "Hashes";
        #endregion
        #region Constructors
        /// <summary>
        /// Constructor that load file
        /// </summary>
        /// <param name="filename">Path to file</param>
        public Hashes(string filename) 
            : this(File.ReadAllBytes(filename), filename) 
        {
            this._filename = filename;
        }

        /// <summary>
        /// Constructor that load byte array and calc hashes
        /// </summary>
        /// <param name="file">Input byte array of file</param>
        private Hashes(byte[] file, string filename = "")
        {
            this._file = file;
            this._filename = filename;
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

                string invoke = (string)(method.Invoke(hash, new object[] { _file }));
                hashes.Add(hash.GetType().Name.ToString(), invoke);
            }
        }
        #endregion
        #region Modul code
        /// <summary>
        /// Getter for list of hashes
        /// </summary>
        /// <returns>Returns type of hash and calculated hash for byte array of file</returns>
        public IDictionary<string, string> GetHashes() => hashes;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Hashes:");
            foreach (var hash in this.GetHashes())
            {
                sb.AppendFormat("{0}: {1}", hash.Key.ToLower(), hash.Value);
                sb.AppendLine();
            }

            return sb.ToString();
        } 

        public string ToString(string type)
        {
            return this.GetHashes().Where(x => x.Key.ToLower() == type.ToLower()).Select(x => x.Value).First();
        }
        #endregion
    }
}
