using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.ML;
using Microsoft.ML.AutoML;
using StaticAnalysisProject.Helpers;

namespace StaticAnalysisProject.ML
{
    public class MachineLearning
    {
        //https://jkdev.me/simple-machine-learning-classification/

        private IFileReport fr = null;
        private IList<IFileReport> _fileReports = new List<IFileReport>();
        private IList<FileReportML> _fileReportsConverted = new List<FileReportML>();

        private static MLContext _mlContext;

        private string _pathToTrainingSet = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\ML\");
        private string[] _trainingDataSetFiles = null;

        public MachineLearning(string fileName)
        {
            //Analyse current file
            fr = new FileReport(fileName);



            _mlContext = new MLContext(seed: 0);
            LoadData();

            var trainingDataView = _mlContext.Data.LoadFromEnumerable<FileReportML>(_fileReportsConverted);
            /*using (FileStream stream = File.OpenWrite("output.data")) {
                _mlContext.Data.SaveAsText(trainingDataView, stream);
            }*/

            var experimentSettings = new MulticlassExperimentSettings();

            var cts = new CancellationTokenSource();
            experimentSettings.MaxExperimentTimeInSeconds = 120;
            experimentSettings.CancellationToken = cts.Token;

            var classExperiment = _mlContext.Auto().CreateMulticlassClassificationExperiment(experimentSettings);

            var metrics = classExperiment.Execute(trainingDataView, "Label");

            Console.WriteLine(metrics.BestRun.ValidationMetrics.ConfusionMatrix.NumberOfClasses);

            //metrics.BestRun.Model.Save();

            
            var predictor = _mlContext.Model.CreatePredictionEngine<FileReportML, FileReportPrediction>(metrics.BestRun.Model);

            var prediction = predictor.Predict(fr.ConvertML());

            Console.WriteLine("Predicted class: {0}", prediction.Class);
        }

        public void LoadData()
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

            foreach(var item in _fileReports)
            {
                _fileReportsConverted.Add(item.ConvertML());
            }
        }

        private IEstimator<ITransformer> GetTrainingPipeline(MLContext mlContext, IEstimator<ITransformer> pipeline)
        {
            return pipeline
                .Append(GetScadaTrainer(mlContext))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedClass"));
        }

        private IEstimator<ITransformer> GetScadaTrainer(MLContext mlContext)
        {
            return mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features");
        }

        private IEstimator<ITransformer> LoadDataProcessPipeline(MLContext mlContext)
        {
            return mlContext.Transforms.Concatenate("", "", "");
        }
    }
}
