using Microsoft.ML.Data;

namespace StaticAnalysisProject.ML
{
    public class FileReportPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool IsMalware { get; set; }
    }
}
