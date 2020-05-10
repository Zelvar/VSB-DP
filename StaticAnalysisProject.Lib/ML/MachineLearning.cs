using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.ML;
using Microsoft.ML.AutoML;
using StaticAnalysisProject.Helpers;
using static Microsoft.ML.DataOperationsCatalog;

namespace StaticAnalysisProject.ML
{
    public class MachineLearning
    {
        //https://jkdev.me/simple-machine-learning-classification/

        public static string SaveModelPath = DataHelper.MLDataModelPath;

        private IFileReport fr = null;
        private IList<IFileReport> _fileReports = new List<IFileReport>();
        private IList<FileReportML> _fileReportsConverted = new List<FileReportML>();

        ITransformer _model = null;

        private static MLContext _mlContext;
        IDataView trainingDataView;

        private string _pathToTrainingSet = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), DataHelper.MLDataPath);
        private string[] _trainingDataSetFiles = null;

        public MachineLearning()
        {
            _mlContext = new MLContext(seed: 0);
            LoadData();

            var loc = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), SaveModelPath);

            if (!File.Exists(loc))
            {
                trainingDataView = _mlContext.Data.LoadFromEnumerable<FileReportML>(_fileReportsConverted);
                #region ML Auto
                /*var experiment = _mlContext.Auto().CreateBinaryClassificationExperiment(10);

                var progress = new Progress<RunDetail<Microsoft.ML.Data.BinaryClassificationMetrics>>(p => { 
                    if(p.ValidationMetrics != null)
                    {
                        Console.WriteLine("Current result: {0} {1}", p.TrainerName, p.ValidationMetrics.Accuracy);
                    }
                });

                var result = experiment.Execute(trainingDataView, labelColumnName: "PredictedClass", progressHandler: progress);
                Console.WriteLine("Best run: ");
                _model = result.BestRun.Model;*/
                #endregion

                TrainTestData splitDataView = _mlContext.Data.TrainTestSplit(trainingDataView, testFraction: 0.3);

                var dataProcessPipeline =
                    _mlContext.Transforms.NormalizeBinning("Entropy", maximumBinCount: 16)
                    //PE info
                    .Append(_mlContext.Transforms.Categorical.OneHotEncoding("IsExeEncoded", "IsExe"))
                    .Append(_mlContext.Transforms.Categorical.OneHotEncoding("IsDllEncoded", "IsDll"))
                    .Append(_mlContext.Transforms.Categorical.OneHotEncoding("IsIsDotNetEncoded", "IsDotNet"))
                    .Append(_mlContext.Transforms.Categorical.OneHotEncoding("IsSignedEncoded", "IsSigned"))
                    //Strings
                    .Append(_mlContext.Transforms.Categorical.OneHotEncoding("ContainsIPEncoded", "ContainsIP"))
                    .Append(_mlContext.Transforms.Categorical.OneHotEncoding("ContainsFilesEncoded", "ContainsFiles"))
                    .Append(_mlContext.Transforms.Categorical.OneHotEncoding("ContainsEmailEncoded", "ContainsEmail"))
                    .Append(_mlContext.Transforms.Categorical.OneHotEncoding("ContainsURLEncoded", "ContainsURL"))
                    //Strings + Yara + pe
                    .Append(_mlContext.Transforms.Categorical.OneHotEncoding("FeaturedBehavior", "Behavior", Microsoft.ML.Transforms.OneHotEncodingEstimator.OutputKind.Bag))
                    .Append(_mlContext.Transforms.Categorical.OneHotEncoding("FeaturedKnownMethods", "KnownMethods", Microsoft.ML.Transforms.OneHotEncodingEstimator.OutputKind.Bag))
                    .Append(_mlContext.Transforms.Categorical.OneHotEncoding("FeaturedImports", "Imports", Microsoft.ML.Transforms.OneHotEncodingEstimator.OutputKind.Bag))
                    // Mime
                    .Append(_mlContext.Transforms.Categorical.OneHotEncoding("MimeTypeEncoded", "MimeType"))

                    .Append(
                        _mlContext.Transforms.Concatenate("Features",
                            "FeaturedBehavior",

                            "VirusTotal", 
                            "FeaturedKnownMethods",
                            "FeaturedImports",
                            "Entropy",
                            "MimeTypeEncoded",
                            //"Sections",

                            "IsExeEncoded",
                            "IsDllEncoded",
                            "IsIsDotNetEncoded",
                            "IsSignedEncoded",

                            "ContainsIPEncoded",
                            "ContainsFilesEncoded",
                            "ContainsEmailEncoded",
                            "ContainsURLEncoded"
                        )
                    )
                    .Append(_mlContext.BinaryClassification.Trainers
                        .SdcaLogisticRegression(
                            labelColumnName: "IsMalware",
                            featureColumnName: "Features"
                        ));

                #region workin
                /*var dataProcessPipeline = _mlContext.Transforms
                    .Concatenate("Features", //Column name
                        "IsInjectThread",
                        "IsHijackNetwork",
                        "IsCreateService",
                        "IsCreateComService",
                        "IsNetworkUdpSock",
                        "IsNetworkTcpListen",
                        "IsNetworkDyndns",
                        "IsNetworkToredo",
                        "IsNetworkSmtpDotNet",
                        "IsNetworkSmtpRaw",
                        "IsNetworkSmtpVb",
                        "IsNetworkP2pWin",
                        "IsNetworkTor",
                        "IsNetworkIrc",
                        "IsNetworkHttp",
                        "IsNetworkDropper",
                        "IsNetworkFtp",
                        "IsNetworkTcpSocket",
                        "IsNetworkDns",
                        "IsNetworkSsl",
                        "IsNetworkDga",
                        "IsBitcoin",
                        "IsCertificate",
                        "IsEscalatePriv",
                        "IsScreenshot",
                        "IsLookupip",
                        "IsDyndns",
                        "IsLookupgeo",
                        "IsKeylogger",
                        "IsCredLocal",
                        "IsSniffAudio",
                        "IsCredFf",
                        "IsCredVnc",
                        "IsCredIe7",
                        "IsSniffLan",
                        "IsMigrateApc",
                        "IsSpreadingFile",
                        "IsSpreadingShare",
                        "IsRatVnc",
                        "IsRatRdp",
                        "IsRatTelnet",
                        "IsRatWebcam",
                        "IsWinMutex",
                        "IsWinRegistry",
                        "IsWinToken",
                        "IsWinPrivateProfile",
                        "IsWinFilesOperation",
                        "IsStrWin32Winsock2Library",
                        "IsStrWin32WininetLibrary",
                        "IsStrWin32InternetAPI",
                        "IsStrWin32HttpAPI",
                        "IsLdpreload",
                        "IsContainsSystemTools",
                        "IsContainsBrowsers",
                        "IsContainsRETools",
                        "IsContainsAntivirus",
                        "IsContainsVMGenericDetection",
                        "IsContainsVMWareDetection",
                        "IsContainsSandboxieDetection",
                        "IsContainsVirtualPCDetection",
                        "IsContainsVirtualBoxDetection",
                        "IsContainsParallelsDetection",
                        "IsContainsQemuDetection",
                        "IsContainsDropperStrings",
                        "IsContainsAutoITCompiledScript",
                        "IsContainsWMIStrings",
                        "IsContainsObfuscatedStrings",
                        "IsContainsBase64dPE",
                        "IsContainsMiscSuspiciousStrings",
                        "IsContainsBITSCLSID",
                        "IsContainsHexEncodedTextPE"
                    ).Append(_mlContext.BinaryClassification.Trainers
                        .FastTree(
                            labelColumnName: "IsMalware", 
                            featureColumnName: "Features"
                        )
                    );*/
                #endregion

                _model = dataProcessPipeline.Fit(splitDataView.TrainSet);
                var predictions = _model.Transform(splitDataView.TestSet);
                var metrics = _mlContext.BinaryClassification.Evaluate(
                    data: predictions,
                    labelColumnName: "IsMalware"
                );

                // Now run the 5-fold cross-validation experiment, using the same pipeline.
                var cvResults = _mlContext.BinaryClassification.CrossValidate(
                    data: trainingDataView,
                    estimator: dataProcessPipeline,
                    numberOfFolds: 10,
                    labelColumnName: "IsMalware"
                );

                var accuracies = cvResults.Select(r => r.Metrics.Accuracy);
                Console.WriteLine(accuracies.Average());
                Console.WriteLine(metrics.Accuracy);

                using (FileStream fs = File.OpenWrite(MachineLearning.SaveModelPath))
                    _mlContext.Model.Save(_model, trainingDataView.Schema, fs);
            }
            else
            {
                DataViewSchema modelSchema;
                _model = _mlContext.Model.Load(MachineLearning.SaveModelPath, out modelSchema);
            }
        }

        public FileReportPrediction Predict(string fileName)
        {
            //Analyse current file
            fr = new FileReport(fileName);
            return Predict(fr);
        }

        public FileReportPrediction Predict (IFileReport fr)
        {

            var predictor = _mlContext.Model.CreatePredictionEngine<FileReportML, FileReportPrediction>(_model);
            var predictModel = fr.ConvertML();
            var prediction = predictor.Predict(predictModel);

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
    }
}
