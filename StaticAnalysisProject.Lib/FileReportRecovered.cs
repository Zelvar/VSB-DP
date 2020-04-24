using System;
using System.Collections.Generic;

namespace StaticAnalysisProject
{
    class FileReportRecovered : IFileReport
    {
        /// <summary>
        /// Info about classification
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// Getters from static analysis
        /// </summary>
        #region PE file
        public IList<string> Exports { get; set; }
        public IList<string> Directories { get; set; }
        public IDictionary<string, IList<string>> Imports { get; set; }
        public IDictionary<string, IList<string>> Sections { get; set; }

        public bool Is32b { get; set; }
        public bool Is64b { get; set; }
        public bool IsDotNet { get; set; }
        public bool IsDriver { get; set; }
        public bool IsExe { get; set; }
        public bool IsDll { get; set; }

        public string Filename { get; set; }
        public string Machine { get; set; }

        public DateTime DateTime { get; set; }

        public string ImportHash { get; set; }

        public uint EntryPoint { get; set; }
        public ulong ImageBase { get; set; }

        public long FileSize { get; set; }
        #endregion
        #region Strings
        public IList<string> IPAddrs { get; set; }
        public IList<string> Urls { get; set; }
        public IList<string> Mails { get; set; }
        public IList<string> Files { get; set; }
        public IDictionary<string, IList<string>> KnownMethods { get; set; }
        #endregion
        #region Hash
        public string MD5 { get; set; }
        public string SHA1 { get; set; }
        public string SHA256 { get; set; }
        public string SHA384 { get; set; }
        public string SHA512 { get; set; }
        #endregion
        #region Yara
        public IList<string> Behavior { get; set; }
        #endregion
        #region VirusTotal
        public int PositiveTests { get; set; }
        public int TotalTests { get; set; }
        public string ScanId { get; set; }
        public string Status { get; set; }
        #endregion
    }
}
