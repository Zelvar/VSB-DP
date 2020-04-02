//using StaticAnalysisProject.Classes; Depracated
using PeNet;
using PeNet.Header;
using PeNet.Header.Pe;
using StaticAnalysisProject.Helpers;
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
        private string filename = null;
        private byte[] file = null;

        private PeFile pefile = null;

        #region Default props
        public string GetModulDescription() => "";

        public string GetModulName() => "PE Analysis";
        #endregion
        #region Constructors
        public PE(string filename) 
            : this(File.ReadAllBytes(filename))
        {
            this.filename = filename;
        }

        public PE(byte[] file)
        {
            this.file = file;
            this.pefile = new PeFile(file);
        }
        #endregion
        #region Getters
        public long GetFileSize() => pefile.FileSize;

        public ulong GetImageBase() => pefile.ImageNtHeaders.OptionalHeader.ImageBase;
        public uint GetEntryPoint() => pefile.ImageNtHeaders.OptionalHeader.AddressOfEntryPoint;

        public string GetImportHash() => pefile.ImpHash;

        public uint GetTimeDateStamp() => pefile.ImageNtHeaders.FileHeader.TimeDateStamp;

        public DateTime GetDateTime() {
            DateTime returnValue = new DateTime(1970, 1, 1, 0, 0, 0);
            returnValue = returnValue.AddSeconds(pefile.ImageNtHeaders.FileHeader.TimeDateStamp);
            returnValue += TimeZoneInfo.Local.GetUtcOffset(returnValue);

            return returnValue;
        }

        public string GetMachine() => pefile.ImageNtHeaders.FileHeader.Machine.ToString();

        public string GetFileName() => (filename == null) ? "" : filename;

        public bool IsDll() => pefile.IsDll;
        public bool IsExe() => pefile.IsExe;
        public bool IsDriver() => pefile.IsDriver;
        public bool Is32b() => pefile.Is32Bit;
        public bool Is64b() => pefile.Is64Bit;
        public bool IsDotNet() => pefile.IsDotNet;

        public IList<string> GetSections()
        {
            IList<string> sections = new List<string>();
            foreach (var section in pefile.ImageSectionHeaders) {
                sections.Add(section.Name);
            }

            return sections;
        }

        public IList<string> GetImportedDlls()
        {
            HashSet<string> imports = new HashSet<string>();
            if(pefile.ImportedFunctions != null)
                foreach (var import in pefile.ImportedFunctions)
                {
                    imports.Add(import.DLL.ToString());
                }

            return imports.ToList();
        }

        public IList<string> GetExports()
        {
            HashSet<string> exports = new HashSet<string>();
            if (pefile.ExportedFunctions != null)
                foreach (var export in pefile.ExportedFunctions)
                {
                    exports.Add(export.Name.ToString());
                }

            return exports.ToList();
        }

        public IList<string> GetDirectories()
        {
            IList<string> directories = new List<string>();
            for(int i = 0; i < pefile.ImageNtHeaders.OptionalHeader.DataDirectory.Length; i++)
            {
                if(pefile.ImageNtHeaders.OptionalHeader.DataDirectory[i].Size != 0)
                    directories.Add(Enum.GetName(typeof(DataDirectoryType), i));
            }

            return directories;
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
