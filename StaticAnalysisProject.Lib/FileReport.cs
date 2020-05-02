using System.IO;
using System;
using System.Collections.Generic;
using StaticAnalysisProject.Modules;
using System.Text;
using HeyRed.Mime;
using System.Diagnostics;

namespace StaticAnalysisProject
{
    public class FileReport 
        : IFileReport
    {
        #region DATA
        private string _filePath = null;
        private byte[] _fileLoaded = null;

        private Hashes hashesInstance = null;
        private Strings stringsInstance = null;
        private VirusTotal virusTotalInstance = null;
        private PE peInstance = null;
        private DetectWithYara yaraInstance = null;
        private Entropy entropyInstance = null;

        private bool _runVirusTotal = true;
        private bool _runYara = true;
        #endregion

        #region Public DATA
        /// <summary>
        /// Classification informations
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// Mime type of file
        /// </summary>
        public string MimeType { get; private set; }

        /// <summary>
        /// Shannon entropy of file
        /// </summary>
        public double Entropy => entropyInstance.Value;

            #region PE file
            public IList<string> Exports => (peInstance.ISPeFile()) ? peInstance.GetExports() : null;
            public IList<string> Directories => (peInstance.ISPeFile()) ? peInstance.GetDirectories() : null;
            public IDictionary<string, IList<string>> Imports => (peInstance.ISPeFile()) ? peInstance.GetImportedDllsAndFunctions() : null;
            public IList<KeyValuePair<string, IList<string>>> Sections => (peInstance.ISPeFile()) ? peInstance.GetSectionsWithCharacteristics() : null;

            public bool Is32b => (peInstance.ISPeFile()) ? peInstance.Is32b() : false;
            public bool Is64b => (peInstance.ISPeFile()) ? peInstance.Is64b() : false;
            public bool IsDotNet => (peInstance.ISPeFile()) ? peInstance.IsDotNet() : false;
            public bool IsDriver => (peInstance.ISPeFile()) ? peInstance.IsDriver() : false;
            public bool IsExe => (peInstance.ISPeFile()) ? peInstance.IsExe() : false;
            public bool IsDll => (peInstance.ISPeFile()) ? peInstance.IsDll() : false;

            public string Filename => Path.GetFileName(this._filePath);
            public string Machine => (peInstance.ISPeFile()) ? peInstance.GetMachine() : "Not PE file";

            public DateTime DateTime => (peInstance.ISPeFile()) ? peInstance.GetDateTime() : File.GetCreationTime(this._filePath);

            public string ImportHash => (peInstance.ISPeFile()) ? peInstance.GetImportHash() : "";

            public uint EntryPoint => (peInstance.ISPeFile()) ? peInstance.GetEntryPoint() : 0;
            public ulong ImageBase => (peInstance.ISPeFile()) ? peInstance.GetImageBase() : 0;

            public long FileSize => (peInstance.ISPeFile()) ? peInstance.GetFileSize() : _fileLoaded.Length;
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
            public int PositiveTests => (virusTotalInstance == null) ? 0 : virusTotalInstance.GetPositiveTests();
            public int TotalTests => (virusTotalInstance == null) ? 0 : virusTotalInstance.GetTotalTests();
            public string ScanId => (virusTotalInstance == null) ? "" : virusTotalInstance.ScanID;
            public string Status => (virusTotalInstance == null) ? "" : virusTotalInstance.GetResponseCode.ToString();
        #endregion
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor that load file
        /// </summary>
        public FileReport(string filePath) 
            : this(File.ReadAllBytes(filePath), filePath)
        {}

        /// <summary>
        /// Constructor for training classification
        /// </summary>
        public FileReport(string filePath, string className)
            : this(File.ReadAllBytes(filePath), filePath, className)
        {}

        /// <summary>
        /// Constructor for optinional turning off virus total or yara
        /// </summary>
        public FileReport(string filePath, string className, bool runVT, bool runYARA)
            : this(File.ReadAllBytes(filePath), filePath, className, runVT, runYARA)
        { }

        /// <summary>
        /// Constructur that load byte array of file
        /// </summary>
        private FileReport(byte[] file, string filePath = "", string className = "", bool runVT = true, bool runYARA = true)
        {
            this._runYara = runYARA;
            this._runVirusTotal = runVT;

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
            // Detect MIME type
            MimeType = new Mime(this._filePath).GetMime();

            // Get hashes
            hashesInstance = new Hashes(this._filePath);

            if (_runVirusTotal)    // Can be disable to not run
            {
                try
                {
                    virusTotalInstance = new VirusTotal(this._fileLoaded);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message.ToString());
                }
            }

            //Calculate entropy
            entropyInstance = new Entropy(this._filePath);

            // Get PE analysis
            peInstance = new PE(this._fileLoaded);

            //Run string analysis
            stringsInstance = new Strings(this._fileLoaded);

            if(_runYara)    // Can be disable to not run
                yaraInstance = new DetectWithYara(this._filePath);
        }
        #endregion
        #region Basic methods
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(hashesInstance.ToString());
            sb.AppendLine(virusTotalInstance.ToString());
            sb.AppendLine(entropyInstance.ToString());
            sb.AppendLine(peInstance.ToString());
            sb.AppendLine(stringsInstance.ToString());
            sb.AppendLine(yaraInstance.ToString());

            return sb.ToString();
        }
        #endregion
    }
}
