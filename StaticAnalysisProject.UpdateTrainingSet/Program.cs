using StaticAnalysisProject.Helpers;
using StaticAnalysisProject.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StaticAnalysisProject.UpdateTrainingSet
{
    /// <summary>
    /// Simple app to update existing file reports with new props, that was not included in previous datasets.
    /// 
    /// What was updating:
    /// Updating SIGN informations
    /// </summary>

    class Program
    {
        const string _fileReportName = "report";
        private static string _finalFileName = string.Format("{1}-{0}.json", DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss"), _fileReportName);

        static void Main(string[] args)
        {
            if (args.Length < 1) throw new Exception("Missing parameter");

            //Search for training data set files
            var jsonFiles = Directory.GetFiles("Data/ML/", "*.json", SearchOption.AllDirectories).ToArray();
            IList<IFileReport> _fileReports = new List<IFileReport>();

            string[] files = Directory
                .GetFiles(args[0], "*", SearchOption.AllDirectories)
                .OrderBy(f => new FileInfo(f).Length)
                .ToArray();

            //Append data to file reports
            foreach (var data in jsonFiles)
            {
                IList<FileReportRecovered> list = ExtensionHelpers.ListFromJson(File.ReadAllText(data));
                _fileReports = _fileReports.Concat(list).ToList();
            }

            int i = 0;

            foreach(var file in files)
            {
                try
                {
                    var pefile = new PE(file);
                    var sha256 = new Hashes(file).ToString("sha256");
                    //Console.WriteLine("{0} - Signed {1} {2}", file, pefile.IsSigned(), sha256);

                    if (pefile.ISPeFile())
                    {
                        var val = _fileReports.FirstOrDefault(x => string.Compare(x.SHA256, sha256) == 0) as FileReportRecovered;
                        if (val != null && pefile != null)
                        {
                            i++;
                            val.IsSigned = pefile.IsSigned();

                            if (val.IsSigned == true)
                            {
                                val.SignIssuer = pefile.GetSignIssuer();
                                val.SignSubject = pefile.GetSignSubject();
                            }

                            Console.WriteLine("{0}={1}", val.Filename, file);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0}", e);
                }
            }

            Console.WriteLine("Found files: {0} of {1}", i, files.Length);

            WriteToFile(_fileReports);
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
    }
}
