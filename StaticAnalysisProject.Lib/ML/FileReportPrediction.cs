using Microsoft.ML.Data;
using System.Text;

namespace StaticAnalysisProject.ML
{
    public class FileReportPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool IsMalware { get; set; }

        [ColumnName("Probability")]
        public float Probability { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("File is detected as malware: {0}", this.IsMalware).AppendLine();
            sb.AppendFormat("Prediction probability: {0:n2} %", (this.Probability*100)).AppendLine();
            
            return sb.ToString();
        }
    }
}
