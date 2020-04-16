using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Linq;
using VirusTotalNet.Objects;
using VirusTotalNet.ResponseCodes;
using VirusTotalNet.Results;


namespace StaticAnalysisProject.Modules
{
    /// <summary>
    /// TODO ANALYSIS
    /// TODO OUTPUT
    /// </summary>
    public class VirusTotal : IModul
    {
        //"f22b0933cc2a457845ea9f3a00245a9e1fba87d5adaf81b0a367629205bbdf9d"
        VirusTotalNet.VirusTotal virusTotal = new VirusTotalNet.VirusTotal("f22b0933cc2a457845ea9f3a00245a9e1fba87d5adaf81b0a367629205bbdf9d");
        #region DATA
        private string _filename = null;
        private byte[] _file = null;

        private IDictionary<string, string> _values = new Dictionary<string, string>();
        #endregion
        #region Getters
        public string GetScanID { get; private set; }
        public string GetVerboseMsg { get; private set; }

        public IDictionary<string, string> GetReportValues => _values;
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
            : this(File.ReadAllBytes(filename))
        {
            this._filename = filename;
        }

        /// <summary>
        /// Constructor that loads file by its byte array
        /// </summary>
        public VirusTotal(byte[] file) {
            this._file = file;

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
            FileReport fr = task.Result;

            if(fr.ResponseCode == FileReportResponseCode.Present) // File was allready scaned
            {
                this.GetScanID = fr.ScanId;
                this.GetVerboseMsg = fr.VerboseMsg;
                ReadReport(fr);
            }
            else
            {
                var task2 = virusTotal.ScanFileAsync(this._file, "filename");
                task2.Wait();
                ScanResult sr = task2.Result;

                this.GetScanID = sr.ScanId;
                this.GetVerboseMsg = sr.VerboseMsg;
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
                            _values.Add(b.Key, b.Value.Result.ToString());
                                //    Console.WriteLine("{0}: {1}", b.Key, b.Value.Result);
                    }
                }
            }
        }
        #endregion
        
        /// <summary>
        /// Overrided ToString method to get report simply
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format("Scan ID: {0}", this.GetScanID));
            sb.AppendLine(string.Format("Verbose message: {0}", this.GetVerboseMsg));

            foreach (var value in this.GetReportValues)
            {
                sb.AppendLine(string.Format("{0}: {1}", value.Key, value.Value));
            }

            return sb.ToString();
        }
    }
}
