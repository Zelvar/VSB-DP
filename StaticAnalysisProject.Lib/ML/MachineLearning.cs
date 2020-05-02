using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.ML;
using StaticAnalysisProject.Helpers;

namespace StaticAnalysisProject.ML
{
    public class MachineLearning
    {
        IFileReport fr = null;
        IList<FileReportRecovered> _fileReports = new List<FileReportRecovered>();

        private static MLContext _mlContext;
        private static IDataView _trainingDataView;
        private static PredictionEngine<FileReportRecovered, FileReportPrediction> _predEngine;

        private string _pathToTrainingSet = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\ML\");
        private string[] _trainingDataSetFiles = null;

        public MachineLearning(string fileName)
        {
            //Analyse current file
            fr = new FileReport(fileName);

            //Load datasets
            if (_fileReports.Count == 0) LoadData();

            //Prepare ML.NET
            _mlContext = new MLContext(seed: 0);
            //_trainingDataView = _mlContext.Data.LoadFromEnumerable<FileReportRecovered>((IEnumerable<FileReportRecovered>)_fileReports);

            //_mlContext.Data.
        }

        /// <summary>
        /// Load training data sets
        /// </summary>
        private void LoadData()
        {
            //Search for training data set files
            this._trainingDataSetFiles = Directory.GetFiles(this._pathToTrainingSet, "*.json", SearchOption.AllDirectories).ToArray();
            
            //Clear list with filereports
            _fileReports.Clear(); 

            //Append data to file reports
            foreach (var data in _trainingDataSetFiles)
            {
                IList<FileReportRecovered> list = ExtensionHelpers.ListFromJson(File.ReadAllText(data));
                _fileReports = _fileReports.Concat(list).ToList();
            }
        }

    }
}
