using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using StaticAnalysisProject;

namespace StaticAnalysisProject.BuildTraningSet
{
    class Program
    {
        /// <summary>
        /// Argument handling & processing
        /// </summary>
        static void Main(string[] args)
        {
            try
            {
                if (args.Length >= 1)
                {
                    if (args.Length == 1 && Directory.Exists(args[0]))
                    {
                        Process(args[0]);
                    }
                    else if (args.Length > 1)
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
        private static void Process(string path)
        {
            string[] files = Directory
                .GetFiles(path, "*", SearchOption.AllDirectories)
                .OrderBy(f => new FileInfo(f).Length)
                .ToArray();

            IList<IFileReport> fileReports = new List<IFileReport>();
             Object myLock = new Object();

            if (files.Length > 1)
            {
                Console.WriteLine("Found {0} files for analysis.", files.Length);

                int i = 0;

                //Parallel.ForEach(files, (file) =>
                //{
                foreach (var file in files) {
                    Stopwatch sw = new Stopwatch();
                    Console.WriteLine("{0} started", Path.GetFileName(file));

                    sw.Start();
                    var report = new FileReport(file, "", true, true);
                    sw.Stop();
                    //lock (myLock)   // APPEND SAFE TO FILE
                    //{
                        fileReports.Add(report);
                        Console.WriteLine("{0}/{1} done - {2} - after {3}",
                            ++i,
                            files.Length,
                            Path.GetFileName(file),
                            sw.Elapsed
                        );

                        if (i % 3 == 0) //Write all data in case of crash
                        {
                            WriteToFile(fileReports);
                        }
                    //}
                }
                //});
            }
            else
            {
                throw new Exception("There is no file to scan.");
            }

            WriteToFile(fileReports);
        }

        /// <summary>
        /// Write data to file
        /// </summary>
        private static void WriteToFile(IList<IFileReport> fileReports)
        {
            try
            {
                File.WriteAllText("report.json", Helpers.ExtensionHelpers.ToJson(fileReports));
                Console.WriteLine("Writing data.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot write data. {0}", e.ToString());
            }
        }
    }
}
