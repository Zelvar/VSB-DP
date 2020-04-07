//using StaticAnalysisProject.Classes; Depracated
using PeNet;
using PeNet.Header;
using PeNet.Header.Pe;
using StaticAnalysisProject.Helpers;
using StaticAnalysisProject.Modules.Subclasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace StaticAnalysisProject.Modules
{
    public class PE : IModul
    {
        #region DATA
        private string _filename = null;
        private byte[] _file = null;

        private HashSet<ImportLibrary> _imports = new HashSet<ImportLibrary>();
        private HashSet<Section> _sections = new HashSet<Section>();
        private HashSet<Export> _exports = new HashSet<Export>();
        private HashSet<Subclasses.Directory> _directories = new HashSet<Subclasses.Directory>();

        /// <summary>
        /// Interface that workin with loaded PE file
        /// </summary>
        private PeFile _pefile = null;
        #endregion

        #region Default props
        public string GetModulDescription() => "";

        public string GetModulName() => "PE Analysis";
        #endregion
        #region Constructors
        public PE(string _filename) 
            : this(File.ReadAllBytes(_filename))
        {
            this._filename = _filename;
        }

        public PE(byte[] file)
        {
            this._file = file;
            this._pefile = new PeFile(file);

            LoadImports();      //Load list of imports
            LoadExports();      //Load list of exports
            LoadDirectories();  //Load list of directories
            LoadSections();     //Import sections
        }
        #endregion
        #region Getters
        /// <summary>
        /// Returns size of the file
        /// </summary>
        public long GetFileSize() => _pefile.FileSize;

        /// <summary>
        /// Image base of file
        /// </summary>
        public ulong GetImageBase() => _pefile.ImageNtHeaders.OptionalHeader.ImageBase;

        /// <summary>
        /// Entry point of the file
        /// </summary>
        public uint GetEntryPoint() => _pefile.ImageNtHeaders.OptionalHeader.AddressOfEntryPoint;

        /// <summary>
        /// Calculated has of imports
        /// </summary>
        public string GetImportHash() => _pefile.ImpHash;

        /// <summary>
        /// Original time stamp from PE file 
        /// </summary>
        public uint GetTimeDateStamp() => _pefile.ImageNtHeaders.FileHeader.TimeDateStamp;

        /// <summary>
        /// Convert time stamp to .NET DateTime
        /// </summary>
        public DateTime GetDateTime() {
            DateTime returnValue = new DateTime(1970, 1, 1, 0, 0, 0);
            returnValue = returnValue.AddSeconds(_pefile.ImageNtHeaders.FileHeader.TimeDateStamp);
            returnValue += TimeZoneInfo.Local.GetUtcOffset(returnValue);

            return returnValue;
        }

        /// <summary>
        /// Returns type of cpu that is file intended for.
        /// </summary>
        public string GetMachine() => _pefile.ImageNtHeaders.FileHeader.Machine.ToString();

        /// <summary>
        /// Returns _filename if file was loaded by this class
        /// </summary>
        public string Get_filename() => (_filename == null) ? "" : _filename;

        /// <summary>
        /// Dll boolean flag
        /// </summary>
        public bool IsDll() => _pefile.IsDll;

        /// <summary>
        /// Executable boolean flag
        /// </summary>
        public bool IsExe() => _pefile.IsExe;

        /// <summary>
        /// Driver boolean flag
        /// </summary>
        public bool IsDriver() => _pefile.IsDriver;

        /// <summary>
        /// 32-bit platform boolean flag
        /// </summary>
        public bool Is32b() => _pefile.Is32Bit;

        /// <summary>
        /// 64-bit platform boolean flag
        /// </summary>
        public bool Is64b() => _pefile.Is64Bit;

        /// <summary>
        /// .NET boolean flag
        /// </summary>
        public bool IsDotNet() => _pefile.IsDotNet;

        /// <summary>
        /// Returns list of sections
        /// </summary>
        public IList<string> GetSections() => _sections.GetList();

        /// <summary>
        /// Returns list of imported dlls
        /// </summary>
        public IList<string> GetImportedDlls() => _imports.GetList();

        /// <summary>
        /// Returns list of exported functions
        /// </summary>
        public IList<string> GetExports() => _exports.GetList();

        /// <summary>
        /// Returns list of used data directories
        /// </summary>
        public IList<string> GetDirectories() => _directories.GetList();
        #endregion

        #region Other functions
        /// <summary>
        /// Load imports DLL and functions
        /// </summary>
        private void LoadImports()
        {
            if (_pefile.ImportedFunctions != null)
                foreach (var import in _pefile.ImportedFunctions)
                {
                    if(!_imports.Contains(new ImportLibrary(import.DLL)))
                    {
                        var list = new List<string>();
                        list.Add(import.Name);
                        _imports.Add(new ImportLibrary(import.DLL, list));
                    }
                    else
                    {
                        ImportLibrary val;
                        if(_imports.TryGetValue(new ImportLibrary(import.DLL), out val))
                        {
                            val.Functions.Add(import.Name);
                        }
                    }
                }
        }

        /// <summary>
        /// Load exports of PE file
        /// </summary>
        private void LoadExports()
        {
            if (_pefile.ExportedFunctions != null)
                foreach (var export in _pefile.ExportedFunctions)
                {
                    _exports.Add(new Export(export));
                }
        }

        /// <summary>
        /// List directories of PE file
        /// </summary>
        private void LoadDirectories()
        {
            for (int i = 0; i < _pefile.ImageNtHeaders.OptionalHeader.DataDirectory.Length; i++)
            {
                if(_pefile.ImageNtHeaders.OptionalHeader.DataDirectory[i].Size != 0)
                    _directories.Add(
                        new Subclasses.Directory(
                            _pefile.ImageNtHeaders.OptionalHeader.DataDirectory[i],
                            (DataDirectoryType)i
                        )
                     );
            }
        }

        /// <summary>
        /// Load sections of PE file
        /// </summary>
        private void LoadSections()
        {
            foreach (var section in _pefile.ImageSectionHeaders)
            {
                _sections.Add(new Section(section));
            }
        }
        #endregion

        public string ToString()
        {
            StringBuilder sb = new StringBuilder();

            var props = GetType()
                .GetMethods()
                .Where(x => x.Name.Contains("Get") || x.Name.Contains("Is"))
                .Where(x => x.Name.CompareTo("GetModulName") != 0)        //Hide modul info
                .Where(x => x.Name.CompareTo("GetModulDescription") != 0) //Hide modul info
                .Where(x => x.Name.CompareTo("GetType") != 0)             //Default prop by .NET class
                .Where(x => x.Name.CompareTo("GetHashCode") != 0);        //Default prop by .NET class
                //.Where(x => !x.ReturnType.IsGenericType);               //Limit lists / dictionaries etc.

            foreach(var prop in props)
            {
                var value = prop.Invoke(this, null);
                string output = "";
                if (value != null) {
                    if (prop.ReturnType.IsGenericType)
                    {
                        if (value.GetType().Name.Contains("Dictionary")) {

                            output = ((IDictionary<string, string>)value).ToStringExtended();
                        } else if (value.GetType().Name.Contains("List")) {
                            output = ((IList<string>)value).ToStringExtended();
                        }
                    } else {
                        output = string.Format("{0}", value);
                    }

                    sb.AppendFormat("{0}: {1}",
                        prop
                        .Name
                        .Replace("Get", "")
                        .Replace("Is", "")
                        .ToWords()
                        .ToLower(),
                        output
                    );
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }
    }
}
