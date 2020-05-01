using Microsoft.ML.Data;

namespace StaticAnalysisProject.ML
{
    class FileReportPrediction
    {
        [ColumnName("PredictedLabel")]
        public string Class;
    }
}
