using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace StaticAnalysisProject.BuildTraningSet
{
    class Program
    {
        #region Output data / info
        const string _fileReportName = "report";
        private static string _finalFileName = string.Format("{1}-{0}.json", DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss"), _fileReportName);
        #endregion
        #region Methods
        /// <summary>
        /// Argument handling & processing
        /// </summary>
        static void Main(string[] args)
        {
            //Console.WriteLine(_finalFileName);

            try
            {
                if (args.Length >= 1)
                {
                    if (
                        args.Length == 1 && 
                        Directory.Exists(args[0]))
                    {
                        Process(args[0]);
                    }
                    else if (
                        args.Length == 2 && 
                        Directory.Exists(args[0]) && 
                        string.Compare(args[1], "") != 0)
                    {
                        Process(args[0], args[1]);
                    }
                    else if (args.Length > 2)
                    {
                        throw new Exception("Too many arguments.");
                    }
                    else
                    {
                        throw new Exception("Unknown path.");
                    }
                }
                else
                {
                    throw new Exception("Missing path.");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                Console.WriteLine();

                Console.WriteLine(e.ToString());
                    
                Console.WriteLine("To generate training data use this command:");
                Console.WriteLine("{0} C:/filepath/to/malware", Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName));
            }
        }

        /// <summary>
        /// Processing scan
        /// </summary>
        private static void Process(string path, string className = "")
        {
            #region DATA
            string[] files = Directory
                .GetFiles(path, "*", SearchOption.AllDirectories)
                .OrderBy(f => new FileInfo(f).Length)
                .ToArray();

            IList<IFileReport> fileReports = new List<IFileReport>();
            #endregion
            #region Scan files
            Stopwatch swFinal = new Stopwatch();
            if (files.Length >= 1)
            {
                Console.WriteLine("Found {0} files for analysis.", files.Length);
                swFinal.Start();
                int i = 1;
                #region Analysis

                Stopwatch[] virusTotal = new Stopwatch[] { new Stopwatch(), new Stopwatch(), new Stopwatch(), new Stopwatch() };

                foreach (var file in files) {
                    Stopwatch sw = new Stopwatch();
                    Console.WriteLine("{0} started", Path.GetFileName(file));

                    //Now wait for time limit of VirusTotal
                    if (
                        virusTotal[i % 4].IsRunning && 
                        virusTotal[i % 4].ElapsedMilliseconds < (60 * 1000 + 1000))
                    { //4 req. / min + 5s 
                      //Wait to limit and reset
                        if (virusTotal[i % 4].ElapsedMilliseconds - 60 * 1000 + 1000 < 0)
                            Thread.Sleep(Convert.ToInt32((60 * 1000) - virusTotal[i % 4].ElapsedMilliseconds));

                        virusTotal[i % 4].Reset();
                        virusTotal[i % 4].Start();
                    }
                    else
                    {
                        virusTotal[i % 4].Start();
                    }

                    sw.Start();
                    FileReport report = new FileReport(file, "", true, true);
                    sw.Stop();

                    /* Make it editable */
                    if (className != "")
                        report.Class = className;

                    /* Add to list */
                    fileReports.Add(report);

                    /* Write info to console */
                    Console.WriteLine("{0}/{1} done - {2} - after {3}",
                        i++,
                        files.Length,
                        Path.GetFileName(file),
                        sw.Elapsed
                    );

                    if (i % 4 == 0) //Write all data in case of crash and check for VirusTotal limit
                    {
                        WriteToFile(fileReports);
                    }

                }
                #endregion
                swFinal.Stop();
            }
            else
            {
                throw new Exception("There is no file to scan.");
            }
            #endregion
            #region Output
            WriteToFile(fileReports);

            for (int i = 0; i < 3; i++) Console.WriteLine();

            Console.WriteLine("Finished after {0}", swFinal.Elapsed);
            Console.WriteLine("File was saved to {0}", Path.GetFullPath(_finalFileName));

            if (string.Compare(className, "") == 0)
            {
                Console.WriteLine("Files was classified as {0} class", className);
            }
            #endregion
        }

        /// <summary>
        /// Write data to file
        /// </summary>
        private static void WriteToFile(IList<IFileReport> fileReports)
        {
            try
            {
                File.WriteAllText(_finalFileName, Helpers.ExtensionHelpers.ToJson(fileReports));
                Console.WriteLine("Writing data.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot write data. {0}", e.ToString());
            }
        }
        #endregion
    }
}
