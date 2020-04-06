using PeNet.Header.Pe;
using System;
using System.Collections.Generic;
using System.Text;

namespace StaticAnalysisProject.Modules.Subclasses
{
    class ImportLibrary
    {
        /// <summary>
        /// List of imported functions
        /// </summary>
        private IList<ImportFunction> _importFunction = new List<ImportFunction>();

        /// <summary>
        /// DLL name
        /// </summary>
        public string DLL { get; private set; }

        /// <summary>
        /// List of used functions
        /// </summary>
        public IList<string> Functions { get; private set; }

        public ImportLibrary( string name , IList<string> list = null) {
            this.DLL = name;
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
        /// Returns DLL name
        /// </summary>
        public string ToString() => DLL.ToString();
    }
}
