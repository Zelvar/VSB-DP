﻿using Microsoft.ML.Data;
using System;
using System.Collections.Generic;

namespace StaticAnalysisProject
{
    public interface IFileReport
    {
        /// <summary>
        /// Classify class
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// Props from static analysis
        /// </summary>
        
        /// <summary>
        /// MIME format
        /// </summary>
        public string MimeType { get; }

        /// <summary>
        /// Shannon entropy of file
        /// </summary>
        public double Entropy { get; }

        #region PE file
        public IList<string> Exports { get; }
        public IList<string> Directories { get; }
        public IDictionary<string, IList<string>> Imports { get; }
        public IList<KeyValuePair<string, IList<string>>> Sections { get; }
        public bool Is32b { get; }
        public bool Is64b { get; }
        public bool IsDotNet { get; }
        public bool IsDriver { get; }
        public bool IsExe { get; }
        public bool IsDll { get; }
        public string Filename { get; }
        public string Machine { get; }
        public DateTime DateTime { get; }
        public string ImportHash { get; }
        public uint EntryPoint { get; }
        public ulong ImageBase { get; }
        public long FileSize { get; }
        public bool IsSigned { get; }
        public string SignIssuer { get; }
        public string SignSubject { get; }
        #endregion
        #region Strings
        public IList<string> IPAddrs { get; }
        public IList<string> Urls { get; }
        public IList<string> Mails { get; }
        public IList<string> Files { get; }
        IDictionary<string, IList<string>> KnownMethods { get; }
        #endregion
        #region Hash
        public string MD5 { get; }
        public string SHA1 { get; }
        public string SHA256 { get; }
        public string SHA384 { get; }
        public string SHA512 { get; }
        #endregion
        #region Yara
        public IList<string> Behavior { get; }
        #endregion
        #region VirusTotal
        public int PositiveTests { get; }
        public int TotalTests { get; }
        public string ScanId { get; }
        public string Status { get; }
        #endregion
    }
}
