using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StaticAnalysisProject.ML
{
    public class FileReportML
    {
        /// <summary>
        /// Info about classification
        /// </summary>
        [LoadColumn(0)]
        public bool IsMalware { get; set; }
        [LoadColumn(1)]
        public float Entropy { get; set; }
        [LoadColumn(2)]
        public float VirusTotal { get; set; }
        [LoadColumn(3)]
        public string[] Behavior { get; set; }
        [LoadColumn(4)]
        public bool IsDotNet { get; set; }
        [LoadColumn(5)]
        public bool IsDriver { get; set; }
        [LoadColumn(6)]
        public bool IsExe { get; set; }
        [LoadColumn(7)]
        public bool IsDll { get; set; }
        [LoadColumn(8)]
        public string[] KnownMethods { get; set; }
        [LoadColumn(9)]
        public bool ContainsEmail { get; set; }
        [LoadColumn(10)]
        public bool ContainsIP { get; set; }
        [LoadColumn(11)]
        public bool ContainsURL { get; set; }
        [LoadColumn(12)]
        public bool ContainsFiles { get; set; }
        [LoadColumn(13)]
        public string[] Imports { get; set; }
        [LoadColumn(14)]
        public bool IsSigned { get; set; }
        [LoadColumn(15)]
        public string MimeType { get; set; }
        [LoadColumn(16)]
        public float Sections { get; set; }

        public static FileReportML Convert(IFileReport file)
        {
            return new FileReportML()
            {
                IsMalware = file.Class == "malware",
                MimeType = file.MimeType,
                Entropy = (float)file.Entropy,
                IsDotNet = file.IsDotNet,
                IsDriver = file.IsDriver,
                IsExe = file.IsExe,
                IsDll = file.IsDll,
                IsSigned = file.IsSigned,
                Behavior = file.Behavior != null ? (file.Behavior as List<string>).ToArray() : new string[] { },
                VirusTotal = file.PositiveTests,
                ContainsEmail = file.Mails != null &&  (file.Mails as List<string>).ToArray().Length > 0,
                ContainsFiles = file.Files != null && (file.Files as List<string>).ToArray().Length > 0 ,
                ContainsIP = file.IPAddrs != null && (file.Files as List<string>).ToArray().Length > 0,
                Sections = file.Sections != null ? file.Sections.Count() : 0,
            };
        }
    }
}
