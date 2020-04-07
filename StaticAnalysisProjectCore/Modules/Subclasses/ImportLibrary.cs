using PeNet.Header.Pe;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace StaticAnalysisProject.Modules.Subclasses
{
    class ImportLibrary : IPESubclass
    {
        /// <summary>
        /// List of imported functions
        /// </summary>
        private IList<ImportFunction> _importFunction = new List<ImportFunction>();

        /// <summary>
        /// Library name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// List of used functions
        /// </summary>
        public IList<string> Functions { get; private set; }

        public ImportLibrary( string name , IList<string> list = null) {
            this.Name = name;
            this.Functions = (list != null ? list : new List<string>());
        }

        /// <summary>
        /// Add function to list
        /// </summary>
        public void AddFce(string name) {
            Functions.Add(name);
        }

        /// <summary>
        /// Add function to list
        /// </summary>
        public void AddFce(ImportFunction import)
        {
            this.AddFce(import.Name);
            this._importFunction.Add(import);
        }

        /// <summary>
        /// Returns Name of library
        /// </summary>
        public override string ToString() => Name.ToString();

        /// <summary>
        /// Override default method GetHashCode
        /// </summary>
        public override int GetHashCode()
        {
            return BitConverter.ToInt32(
                SHA256.Create().ComputeHash(
                    Encoding.UTF8.GetBytes(this.Name)
                    ),
                0);
        }

        /// <summary>
        /// Check if library exists
        /// </summary>
        public override bool Equals(object obj)
        {
            return this.Name.Equals(((ImportLibrary)obj).Name);
        }
    }
}
