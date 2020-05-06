using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Linq;
using VirusTotalNet.Objects;
using VirusTotalNet.ResponseCodes;
using VirusTotalNet.Results;
using System;

namespace StaticAnalysisProject.Modules
{
    /// <summary>
    /// Casted VirusTotal enums together (FileReportResponseCode + ScanFileResponseCode)
    /// </summary>
    public enum VirusTotalStatus{
        Error = -1,
        NotPresent = 0,
        Queued = 1,
        Present = 2,
    }

    /// <summary>
    /// TODO ANALYSIS
    /// TODO OUTPUT
    /// </summary>
    public class VirusTotal : IModul
    {
        //Constructor for lib - parameter is API KEY
        VirusTotalNet.VirusTotal virusTotal = new VirusTotalNet.VirusTotal("db0b54c917cc3ef141b894cd4231bf2fd471e93a3f6732184e3557c385494b9c");

        #region DATA
        private string _filename = null;
        private byte[] _file = null;

        private const string _textAVDelimiter = "AVSW___";

        private IDictionary<string, string> _values = new Dictionary<string, string>();
        #endregion
        #region Getters
        public string ScanID { get; private set; }
        public string VerboseMsg { get; private set; }

        /// <summary>
        /// Return number of positive tests by AV on VirusTotal
        /// </summary>
        public int GetPositiveTests() {
                int val;
                Int32.TryParse(
                    GetReportValues()
                        .Where(x => x.Key.Equals("Positives"))
                        .Select(x => x.Value)
                        .FirstOrDefault()
                    , out val);

                return val;
        }

        /// <summary>
        /// Return number of tests in total on VirusTotal
        /// </summary>
        public int GetTotalTests()
        {
            int val;
            Int32.TryParse(
                GetReportValues()
                    .Where(x => x.Key.Equals("Total"))
                    .Select(x => x.Value)
                    .FirstOrDefault()
                , out val);

            return val;
        }

        /// <summary>
        /// Return dictionary with details about test
        /// </summary>
        public Dictionary<string, string> GetAVOutput => GetReportValues()
                    .Where(x => x.Key.StartsWith(_textAVDelimiter))
                    .Select(x => x)
                    .ToDictionary(o => o.Key, o => o.Value);

        /// <summary>
        /// Return responseCode
        /// </summary>
        public VirusTotalStatus GetResponseCode { get; private set; }

        /// <summary>
        /// Check if is queued to test
        /// </summary>
        public bool IsQueued => GetResponseCode == VirusTotalStatus.Queued;

        /// <summary>
        /// Check if test is allready done
        /// </summary>
        public bool IsPresent => GetResponseCode == VirusTotalStatus.Present;

        public IDictionary<string, string> GetReportValues() => _values;
        #endregion
        #region Default props
        public string GetModulDescription() => "This modul send file to VirusTotal for antivirus analysis and generates report";
        public string GetModulName() => "VirusTotal";
        #endregion
        #region Constructors
        /// <summary>
        /// Constructor that loads file by filename
        /// </summary>
        /// <param name="filename"></param>
        public VirusTotal(string filename) 
            : this(File.ReadAllBytes(filename), filename)
        {
            this._filename = filename;
        }

        /// <summary>
        /// Constructor that loads file by its byte array
        /// </summary>
        public VirusTotal(byte[] file, string filename = "") {
            this._file = file;
            this._filename = filename;

            virusTotal.UseTLS = true;
            GetReport();
        }
        #endregion
        #region Basic methods
        /// <summary>
        /// Download report from VirusTotal
        /// </summary>
        private void GetReport()
        {
            var task = virusTotal.GetFileReportAsync(this._file);
            task.Wait();
            VirusTotalNet.Results.FileReport fr = task.Result;

            if(fr.ResponseCode == FileReportResponseCode.Present) // File was allready scaned
            {
                this.ScanID = fr.ScanId;
                this.VerboseMsg = fr.VerboseMsg;
                this.GetResponseCode = CastStatus(fr.ResponseCode);
                ReadReport(fr);
            }
            else
            {
                var task2 = virusTotal.ScanFileAsync(this._file, "filename");
                task2.Wait();
                ScanResult sr = task2.Result;

                this.ScanID = sr.ScanId;
                this.VerboseMsg = sr.VerboseMsg;
                this.GetResponseCode = CastStatus(sr.ResponseCode);
                ReadReport(sr);
            }

        }

        /// <summary>
        /// Read detailed report from VirusTotal
        /// </summary>
        private void ReadReport(object obj)
        {
            foreach (PropertyDescriptor a in TypeDescriptor.GetProperties(obj))
            {
                var value = a.GetValue(obj);
                if (!value.GetType().Name.Contains("Dictionary"))
                {
                    _values.Add(a.Name, value.ToString());
                }
                else
                {
                    foreach(var b in (IDictionary<string, ScanEngine>)value)
                    {
                        if (b.Value.Detected)
                            _values.Add(string.Format("{0}{1}", _textAVDelimiter, b.Key), b.Value.Result.ToString());
                    }
                }
            }
        }
        
        private VirusTotalStatus CastStatus(FileReportResponseCode status) 
        {
            if (status == FileReportResponseCode.Queued)
                return VirusTotalStatus.Queued;
            else if (status == FileReportResponseCode.Present)
                return VirusTotalStatus.Present;
            else if (status == FileReportResponseCode.NotPresent)
                return VirusTotalStatus.NotPresent;

            return VirusTotalStatus.Error;
        }
        private VirusTotalStatus CastStatus(ScanFileResponseCode status)
        {
            if (status == ScanFileResponseCode.Queued)
                return VirusTotalStatus.Queued;

            return VirusTotalStatus.Error;
        }


        /// <summary>
        /// Overrided ToString method to get report simply
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("VirusTotal report:");
            sb.AppendLine(string.Format("Scan ID: {0}", this.ScanID));
            sb.AppendLine(string.Format("Verbose message: {0}", this.VerboseMsg));

            foreach (var value in this.GetReportValues())
            {
                sb.AppendLine(string.Format("{0}: {1}", value.Key.Replace(_textAVDelimiter, ""), value.Value));
            }

            return sb.ToString();
        }
        #endregion
    }
}
