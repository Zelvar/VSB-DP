using System;
using System.ComponentModel;
using System.IO;
using VirusTotalNet;
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
        private string _filename = null;
        private byte[] _file = null;

        //"f22b0933cc2a457845ea9f3a00245a9e1fba87d5adaf81b0a367629205bbdf9d"
        VirusTotalNet.VirusTotal virusTotal = new VirusTotalNet.VirusTotal("f22b0933cc2a457845ea9f3a00245a9e1fba87d5adaf81b0a367629205bbdf9d");

        public string GetModulDescription() => "This modul send file to VirusTotal for antivirus analysis";
        public string GetModulName() => "VirusTotal";

        public string ToString()
        {
            throw new NotImplementedException();
        }

        public VirusTotal(string filename) 
            : this(File.ReadAllBytes(filename))
        {
            this._filename = filename;
        }

        public VirusTotal(byte[] file) {
            this._file = file;

            virusTotal.UseTLS = true;
            GetReport();
        }

        public void GetReport()
        {
            var task = virusTotal.GetFileReportAsync(this._file);
            task.Wait();
            FileReport fr = task.Result;

            if(fr.ResponseCode == FileReportResponseCode.Present) // File was allready scaned
            {
                Console.WriteLine(fr.ScanId);
                Console.WriteLine(fr.VerboseMsg);
                ReadReport(fr);
            }
            else
            {
                var task2 = virusTotal.ScanFileAsync(this._file, "filename");
                task2.Wait();
                ScanResult sr = task2.Result;

                Console.WriteLine(sr.ScanId);
                Console.WriteLine(sr.VerboseMsg);
                ReadReport(sr);
            }

        }

        public void ReadReport(object obj)
        {
            foreach (PropertyDescriptor a in TypeDescriptor.GetProperties(obj))
            {
                string name = a.Name;
                object value = a.GetValue(obj);
                Console.WriteLine("{0}={1}", name, value);
            }
        }
    }
}
