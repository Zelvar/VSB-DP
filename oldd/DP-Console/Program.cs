using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

using StaticAnalysisProject;
using StaticAnalysisProject.Modules;

namespace DP_Console
{
    class Program {
       public static void Main(string[] args)
        {
            //VirusTotalAsync().Wait();
            //var a = new AFile(@"C:\Users\Zelvar\source\repos\KeyLoggerVSB\KeyLoggerVSB\bin\Release\App.exe");
            Console.ReadLine();
            //new Hashes(File.ReadAllBytes(@"C:\Users\Zelvar\source\repos\KeyLoggerVSB\KeyLoggerVSB\bin\Release\App.exe"));
            Console.ReadLine();

        }

        /*#region TESTS
        private static async System.Threading.Tasks.Task VirusTotalAsync()
        {
            VirusTotal virusTotal = new VirusTotal("f22b0933cc2a457845ea9f3a00245a9e1fba87d5adaf81b0a367629205bbdf9d");
            virusTotal.UseTLS = true;

            var file = File.ReadAllBytes(@"C:\Users\Zelvar\source\repos\KeyLoggerVSB\KeyLoggerVSB\bin\Release\App.exe");

            FileReport fileReport = await virusTotal.GetFileReportAsync(file);

            bool hasFileBeenScannedBefore = fileReport.ResponseCode == FileReportResponseCode.Present;

            Console.WriteLine("File has been scanned before: " + (hasFileBeenScannedBefore ? "Yes" : "No"));

            //If the file has been scanned before, the results are embedded inside the report.
            if (hasFileBeenScannedBefore)
            {
                PrintScan(fileReport);
            }
            else
            {
                ScanResult fileResult = await virusTotal.ScanFileAsync(file, "EICAR.txt");
                PrintScan(fileResult);
            }

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
            foreach(PropertyDescriptor a in TypeDescriptor.GetProperties(fileReport))
            {
                string name = a.Name;
                object value = a.GetValue(fileReport);
                Console.WriteLine("{0}={1}", name, value);
            }

            if (fileReport.ResponseCode == FileReportResponseCode.Present)
            {
                foreach (KeyValuePair<string, ScanEngine> scan in fileReport.Scans)
                {
                    Console.WriteLine("" +
                    "{0,-25}\n" +
                        " - Detected: {1}\n" +
                        " - Version = {2}\n" +
                        " - Update = {3}\n" +
                        " - Result = {4}\n", 
                        scan.Key, 
                        scan.Value.Detected, 
                        scan.Value.Version, 
                        scan.Value.Update, 
                        scan.Value.Result
                    );
                }
            }

            Console.WriteLine();
        }
        #endregion TESTS*/
    }
}
