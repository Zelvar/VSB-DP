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

        ITransformer _model = null;

        private static MLContext _mlContext;

        private string _pathToTrainingSet = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\ML\");
        private string[] _trainingDataSetFiles = null;

        public MachineLearning()
        {
            _mlContext = new MLContext(seed: 0);
            LoadData();

            var trainingDataView = _mlContext.Data.LoadFromEnumerable<FileReportML>(_fileReportsConverted);
            var experiment = _mlContext.Auto().CreateBinaryClassificationExperiment(10);

            var progress = new Progress<RunDetail<Microsoft.ML.Data.BinaryClassificationMetrics>>(p => { 
                if(p.ValidationMetrics != null)
                {
                    Console.WriteLine("Current result: {0} {1}", p.TrainerName, p.ValidationMetrics.Accuracy);
                }
            });

            var result = experiment.Execute(trainingDataView, labelColumnName: "PredictedClass", progressHandler: progress);



            Console.WriteLine("Best run: ");
            Console.WriteLine(result.BestRun.TrainerName);

            _model = result.BestRun.Model;

            using (FileStream fs = File.OpenWrite("Output.zip"))
                _mlContext.Model.Save(_model, trainingDataView.Schema, fs);
        }

        public FileReportPrediction Predict(string fileName)
        {
            //Analyse current file
            fr = new FileReport(fileName);

            var predictor = _mlContext.Model.CreatePredictionEngine<FileReportML, FileReportPrediction>(_model);
            var predictModel = fr.ConvertML();
            var prediction = predictor.Predict(predictModel);

            //Debug.WriteLine("Predicted class: {0} {1}", prediction.IsMalware);

            return prediction;
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
