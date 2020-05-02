using Microsoft.ML.Data;
using System;
using System.Collections.Generic;

namespace StaticAnalysisProject
{
    public class FileReportRecovered 
        : IFileReport
    {
        /// <summary>
        /// Info about classification
        /// </summary>
        [LoadColumn(0)]
        public string Class { get; set; }

        /// <summary>
        /// Mime type
        /// </summary>
        [LoadColumn(1)]
        public string MimeType { get; set; }

        [LoadColumn(34)]
        public double Entropy { get; set; }

        /// <summary>
        /// Shannon entropy of file
        /// </summary>

        /// <summary>
        /// Getters from static analysis
        /// </summary>
        #region PE file
        [LoadColumn(2)]
        public IList<string> Exports { get; set; }
        [LoadColumn(3)]
        public IList<string> Directories { get; set; }
        [LoadColumn(4)]
        public IDictionary<string, IList<string>> Imports { get; set; }
        [LoadColumn(5)]
        public IList<KeyValuePair<string, IList<string>>> Sections { get; set; }

        [LoadColumn(6)]
        public bool Is32b { get; set; }
        [LoadColumn(7)]
        public bool Is64b { get; set; }
        [LoadColumn(8)]
        public bool IsDotNet { get; set; }
        [LoadColumn(9)]
        public bool IsDriver { get; set; }
        [LoadColumn(10)]
        public bool IsExe { get; set; }
        [LoadColumn(11)]
        public bool IsDll { get; set; }

        [LoadColumn(12)]
        public string Filename { get; set; }
        [LoadColumn(13)]
        public string Machine { get; set; }

        [LoadColumn(14)]
        public DateTime DateTime { get; set; }

        [LoadColumn(15)]
        public string ImportHash { get; set; }

        [LoadColumn(16)]
        public uint EntryPoint { get; set; }
        [LoadColumn(17)]
        public ulong ImageBase { get; set; }

        [LoadColumn(18)]
        public long FileSize { get; set; }
        #endregion
        #region Strings
        [LoadColumn(19)]
        public IList<string> IPAddrs { get; set; }
        [LoadColumn(20)]
        public IList<string> Urls { get; set; }
        [LoadColumn(21)]
        public IList<string> Mails { get; set; }
        [LoadColumn(22)]
        public IList<string> Files { get; set; }
        [LoadColumn(23)]
        public IDictionary<string, IList<string>> KnownMethods { get; set; }
        #endregion
        #region Hash
        [LoadColumn(24)]
        public string MD5 { get; set; }
        [LoadColumn(25)]
        public string SHA1 { get; set; }
        [LoadColumn(26)]
        public string SHA256 { get; set; }
        [LoadColumn(27)]
        public string SHA384 { get; set; }
        [LoadColumn(28)]
        public string SHA512 { get; set; }
        #endregion
        #region Yara
        [LoadColumn(29)]
        public IList<string> Behavior { get; set; }
        #endregion
        #region VirusTotal
        [LoadColumn(30)]
        public int PositiveTests { get; set; }
        [LoadColumn(31)]
        public int TotalTests { get; set; }
        [LoadColumn(32)]
        public string ScanId { get; set; }
        [LoadColumn(33)]
        public string Status { get; set; }
        #endregion
    }
}
