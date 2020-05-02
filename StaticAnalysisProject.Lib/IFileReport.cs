using Microsoft.ML.Data;
using System;
using System.Collections.Generic;

namespace StaticAnalysisProject
{
    public interface IFileReport
    {
        /// <summary>
        /// Classify class
        /// </summary>
        [LoadColumn(0)]
        public string Class { get; set; }

        /// <summary>
        /// Props from static analysis
        /// </summary>
        
        /// <summary>
        /// MIME format
        /// </summary>
        [LoadColumn(1)]
        public string MimeType { get; }

        /// <summary>
        /// Shannon entropy of file
        /// </summary>
        [LoadColumn(34)]
        public double Entropy { get; }

        #region PE file
        [LoadColumn(2)]
        public IList<string> Exports { get; }

        [LoadColumn(3)]
        public IList<string> Directories { get; }

        [LoadColumn(4)]
        public IDictionary<string, IList<string>> Imports { get; }

        [LoadColumn(5)]
        public IList<KeyValuePair<string, IList<string>>> Sections { get; }


        [LoadColumn(6)]
        public bool Is32b { get; }

        [LoadColumn(7)]
        public bool Is64b { get; }

        [LoadColumn(8)]
        public bool IsDotNet { get; }

        [LoadColumn(9)]
        public bool IsDriver { get; }

        [LoadColumn(10)]
        public bool IsExe { get; }

        [LoadColumn(11)]
        public bool IsDll { get; }


        [LoadColumn(12)]
        public string Filename { get; }

        [LoadColumn(13)]
        public string Machine { get; }


        [LoadColumn(14)]
        public DateTime DateTime { get; }


        [LoadColumn(15)]
        public string ImportHash { get; }


        [LoadColumn(16)]
        public uint EntryPoint { get; }

        [LoadColumn(17)]
        public ulong ImageBase { get; }


        [LoadColumn(18)]
        public long FileSize { get; }
        #endregion
        #region Strings

        [LoadColumn(19)]
        public IList<string> IPAddrs { get; }

        [LoadColumn(20)]
        public IList<string> Urls { get; }

        [LoadColumn(21)]
        public IList<string> Mails { get; }

        [LoadColumn(22)]
        public IList<string> Files { get; }

        [LoadColumn(23)]
        IDictionary<string, IList<string>> KnownMethods { get; }
        #endregion
        #region Hash

        [LoadColumn(24)]
        public string MD5 { get; }

        [LoadColumn(25)]
        public string SHA1 { get; }

        [LoadColumn(26)]
        public string SHA256 { get; }

        [LoadColumn(27)]
        public string SHA384 { get; }

        [LoadColumn(28)]
        public string SHA512 { get; }
        #endregion
        #region Yara

        [LoadColumn(29)]
        public IList<string> Behavior { get; }
        #endregion
        #region VirusTotal

        [LoadColumn(30)]
        public int PositiveTests { get; }

        [LoadColumn(31)]
        public int TotalTests { get; }

        [LoadColumn(32)]
        public string ScanId { get; }

        [LoadColumn(33)]
        public string Status { get; }
        #endregion
    }
}
