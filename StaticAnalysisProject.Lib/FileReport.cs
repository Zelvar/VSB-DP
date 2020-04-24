using System.IO;
using System;
using System.Collections.Generic;
using StaticAnalysisProject.Modules;
using System.Text;

namespace StaticAnalysisProject
{
    public class FileReport : IFileReport
    {
        #region DATA
        private string _filePath = null;
        private byte[] _fileLoaded = null;

        private Hashes hashesInstance = null;
        private Strings stringsInstance = null;
        private VirusTotal virusTotalInstance = null;
        private PE peInstance = null;
        private DetectWithYara yaraInstance = null;
        #endregion

        #region Public DATA
            /// <summary>
            /// Classification informations
            /// </summary>
            public string Class { get; private set; }

            #region PE file
            public IList<string> Exports => peInstance.GetExports();
            public IList<string> Directories => peInstance.GetDirectories();
            public IDictionary<string, IList<string>> Imports => peInstance.GetImportedDllsAndFunctions();
            public IDictionary<string, IList<string>> Sections => peInstance.GetSectionsWithCharacteristics();

            public bool Is32b => peInstance.Is32b();
            public bool Is64b => peInstance.Is64b();
            public bool IsDotNet => peInstance.IsDotNet();
            public bool IsDriver => peInstance.IsDriver();
            public bool IsExe => peInstance.IsExe();
            public bool IsDll => peInstance.IsDll();

            public string Filename => Path.GetFileName(this._filePath);
            public string Machine => peInstance.GetMachine();

            public DateTime DateTime => peInstance.GetDateTime();

            public string ImportHash => peInstance.GetImportHash();

            public uint EntryPoint => peInstance.GetEntryPoint();
            public ulong ImageBase => peInstance.GetImageBase();

            public long FileSize => peInstance.GetFileSize();
            #endregion
            #region Strings
            public IList<string> IPAddrs => stringsInstance.GetIPs();
            public IList<string> Urls => stringsInstance.GetURLs();
            public IList<string> Mails => stringsInstance.GetMails();
            public IList<string> Files => stringsInstance.GetFiles();
            public IDictionary<string, IList<string>> KnownMethods => stringsInstance.GetKnownMethodsInDictionary();
            #endregion
            #region Hash
            public string MD5 => hashesInstance.ToString("MD5");
            public string SHA1 => hashesInstance.ToString("SHA1");
            public string SHA256 => hashesInstance.ToString("SHA256");
            public string SHA384 => hashesInstance.ToString("SHA384");
            public string SHA512 => hashesInstance.ToString("SHA512");
            #endregion
            #region Yara
            public IList<string> Behavior => yaraInstance.GetResults();
            #endregion
            #region VirusTotal
            public int PositiveTests => virusTotalInstance.GetPositiveTests();
            public int TotalTests => virusTotalInstance.GetTotalTests();
            public string ScanId => virusTotalInstance.ScanID;
            public string Status => virusTotalInstance.GetResponseCode.ToString();
        #endregion
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor that load file
        /// </summary>
        public FileReport(string filePath) 
            : this(File.ReadAllBytes(filePath), filePath)
        {
            this._filePath = filePath;
        }

        /// <summary>
        /// Constructor for training classification
        /// </summary>
        public FileReport(string filePath, string className)
            : this(File.ReadAllBytes(filePath), filePath, className)
        {
            this.Class = className;
        }

        /// <summary>
        /// Constructur that load byte array of file
        /// </summary>
        public FileReport(byte[] file, string filePath = "", string className = "")
        {
            if (file != null) {
                _fileLoaded = file;
                _filePath = filePath;
                this.Class = className;
                LoadInstances();    //Prepare everything
            }else{
                throw new StaticAnalysisProjectException("File is empty");
            }
        }
        #endregion
        #region Main code
        /// <summary>
        /// Prepare everything
        /// </summary>
        private void LoadInstances()
        {
            peInstance = new PE(this._fileLoaded);
            hashesInstance = new Hashes(this._fileLoaded);
            stringsInstance = new Strings(this._fileLoaded);
            virusTotalInstance = new VirusTotal(this._fileLoaded);
            yaraInstance = new DetectWithYara(this._filePath);
        }
        #endregion
        #region Basic methods
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(hashesInstance.ToString());
            sb.AppendLine(virusTotalInstance.ToString());
            sb.AppendLine(peInstance.ToString());
            sb.AppendLine(stringsInstance.ToString());
            sb.AppendLine(yaraInstance.ToString());

            return sb.ToString();
        }
        #endregion
    }
}
