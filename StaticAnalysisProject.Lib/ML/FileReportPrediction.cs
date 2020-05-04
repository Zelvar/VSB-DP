using Microsoft.ML.Data;

namespace StaticAnalysisProject.ML
{
    public class FileReportPrediction
    {
        [ColumnName("PredictedClass")]
        public bool IsMalware { get; set; }
    }
}
