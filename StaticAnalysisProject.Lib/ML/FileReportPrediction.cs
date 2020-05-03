using Microsoft.ML.Data;

namespace StaticAnalysisProject.ML
{
    class FileReportPrediction
    {
        [ColumnName("PredictedClass")]
        public string Class { get; set; }
    }
}
