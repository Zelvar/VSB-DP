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
        [LoadColumn(1), ColumnName("Label")]
        public bool IsMalware { get; set; }

        /// <summary>
        /// Mime type
        /// </summary>
        [LoadColumn(2)]
        public string MimeType { get; set; }

        [LoadColumn(3)]
        public float Entropy { get; set; }
        [LoadColumn(4)]
        public string[] Exports { get; set; }
        [LoadColumn(5)]
        public string[] Directories { get; set; }
        [LoadColumn(6)]
        public string[] Imports { get; set; }
        [LoadColumn(7)]
        public string[] Sections { get; set; }

        [LoadColumn(8)]
        public bool Is32b { get; set; }
        [LoadColumn(9)]
        public bool Is64b { get; set; }
        [LoadColumn(10)]
        public bool IsDotNet { get; set; }
        [LoadColumn(11)]
        public bool IsDriver { get; set; }
        [LoadColumn(12)]
        public bool IsExe { get; set; }
        [LoadColumn(13)]
        public bool IsDll { get; set; }

        [LoadColumn(14)]
        public string ImportHash { get; set; }
        [LoadColumn(15)]
        public string[] IPAddrs { get; set; }
        [LoadColumn(16)]
        public string[] Urls { get; set; }
        [LoadColumn(17)]
        public string[] Mails { get; set; }
        [LoadColumn(18)]
        public string[] Files { get; set; }
        [LoadColumn(19)]
        public string[] KnownMethods { get; set; }
        [LoadColumn(20)]
        public string SHA256 { get; set; }
        [LoadColumn(21)]
        public string[] Behavior { get; set; }

        public static FileReportML Convert(IFileReport file) 
        {
            return new FileReportML()
            {
                IsMalware = file.Class == "malware",
                MimeType = file.MimeType,
                Entropy = (float)file.Entropy,
                Exports = file.Exports != null ? (file.Exports as List<string>).ToArray() : null,
                Directories = file.Directories != null ? (file.Directories as List<string>).ToArray() : null,
                Imports =  file.Imports != null && file.Imports.Count > 0 && file.Imports.Keys != null ? (file.Imports.Keys.ToArray()).Concat(file.Imports.Values.SelectMany(x => x).ToArray()).Select(x => x).Where(x => x != "" && x != null).ToArray() : null,
                Sections = file.Sections != null ? file.Sections.Select(x => x.Key).ToArray() : null,
                Is32b = file.Is32b,
                Is64b = file.Is64b,
                IsDotNet = file.IsDotNet,
                IsDriver = file.IsDriver,
                IsExe = file.IsExe,
                IsDll = file.IsDll,
                ImportHash = file.ImportHash != null ? "" : "",
                IPAddrs = file.IPAddrs != null ? (file.IPAddrs as List<string>).ToArray() : null,
                Urls = file.Urls != null ? (file.Urls as List<string>).ToArray() : null,
                Mails = file.Mails != null ? (file.Mails as List<string>).ToArray() : null,
                Files = file.Files != null ? (file.Files as List<string>).ToArray() : null,
                KnownMethods = file.KnownMethods != null && file.KnownMethods.Count > 0 && file.KnownMethods.Values != null ? file.KnownMethods.Values.SelectMany(x => x).ToArray() : null,
                Behavior = file.Behavior != null ? (file.Behavior as List<string>).ToArray() : null,
            };
        }
    }
}
