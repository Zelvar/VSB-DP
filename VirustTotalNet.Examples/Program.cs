using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirusTotalNet.Objects;
using VirusTotalNet.ResponseCodes;
using VirusTotalNet.Results;

namespace VirusTotalNet.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            VirusTotal virusTotal = new VirusTotal("-- API KEY --");

            //Use HTTPS instead of HTTP
            virusTotal.UseTLS = true;

            //Create the EICAR test virus. See http://www.eicar.org/86-0-Intended-use.html
            byte[] eicar = Encoding.ASCII.GetBytes(@"X5O!P%@AP[4\PZX54(P^)7CC)7}$EICAR-STANDARD-ANTIVIRUS-TEST-FILE!$H+H*");

            //Check if the file has been scanned before.
            var task = virusTotal.GetFileReportAsync(eicar);
            FileReport fileReport = task.Result;

            bool hasFileBeenScannedBefore = fileReport.ResponseCode == FileReportResponseCode.Present;

            Console.WriteLine("File has been scanned before: " + (hasFileBeenScannedBefore ? "Yes" : "No"));

            //If the file has been scanned before, the results are embedded inside the report.
            if (hasFileBeenScannedBefore)
            {
                PrintScan(fileReport);
            }
            else
            {
                var task2 = virusTotal.ScanFileAsync(eicar, "EICAR.txt");
                ScanResult fileResult = task2.Result;
                PrintScan(fileResult);
            }

            Console.WriteLine();

            string scanUrl = "http://www.google.com/";

            var task3 = virusTotal.GetUrlReportAsync(scanUrl);
            UrlReport urlReport = task3.Result;

            bool hasUrlBeenScannedBefore = urlReport.ResponseCode == UrlReportResponseCode.Present;
            Console.WriteLine("URL has been scanned before: " + (hasUrlBeenScannedBefore ? "Yes" : "No"));

            //If the url has been scanned before, the results are embedded inside the report.
            if (hasUrlBeenScannedBefore)
            {
                PrintScan(urlReport);
            }
            else
            {
                var task4 = virusTotal.ScanUrlAsync(scanUrl);
                UrlScanResult urlResult = task4.Result;
                PrintScan(urlResult);
            }

            Console.ReadLine();
        }

        private static void PrintScan(UrlScanResult scanResult)
        {
            Console.WriteLine("Scan ID: " + scanResult.ScanId);
            Console.WriteLine("Message: " + scanResult.VerboseMsg);
            Console.WriteLine();
        }

        private static void PrintScan(ScanResult scanResult)
        {
            Console.WriteLine("Scan ID: " + scanResult.ScanId);
            Console.WriteLine("Message: " + scanResult.VerboseMsg);
            Console.WriteLine();
        }

        private static void PrintScan(FileReport fileReport)
        {
            Console.WriteLine("Scan ID: " + fileReport.ScanId);
            Console.WriteLine("Message: " + fileReport.VerboseMsg);

            if (fileReport.ResponseCode == FileReportResponseCode.Present)
            {
                foreach (KeyValuePair<string, ScanEngine> scan in fileReport.Scans)
                {
                    Console.WriteLine("{0,-25} Detected: {1}", scan.Key, scan.Value.Detected);
                }
            }

            Console.WriteLine();
        }

        private static void PrintScan(UrlReport urlReport)
        {
            Console.WriteLine("Scan ID: " + urlReport.ScanId);
            Console.WriteLine("Message: " + urlReport.VerboseMsg);

            if (urlReport.ResponseCode == UrlReportResponseCode.Present)
            {
                foreach (KeyValuePair<string, UrlScanEngine> scan in urlReport.Scans)
                {
                    Console.WriteLine("{0,-25} Detected: {1}", scan.Key, scan.Value.Detected);
                }
            }

            Console.WriteLine();
        }
    }
}