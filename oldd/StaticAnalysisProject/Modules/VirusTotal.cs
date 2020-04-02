using System;
using VirusTotalNet;
using VirusTotalNet.Objects;
using VirusTotalNet.ResponseCodes;
using VirusTotalNet.Results;


namespace StaticAnalysisProject.Modules
{
    class VirusTotal : IModul
    {
        public string GetModulDescription() => "This modul send file to VirusTotal for antivirus analysis";
        public string GetModulName() => "VirusTotal";

        public string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
