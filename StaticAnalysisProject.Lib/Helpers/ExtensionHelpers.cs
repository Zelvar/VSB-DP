using StaticAnalysisProject.ML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace StaticAnalysisProject.Helpers
{
    public static class ExtensionHelpers
    {
        /// <summary>
        /// Converts Camel Case to Words (CamelCase -> Camel Case)
        /// </summary>
        public static string ToWords(this string inputCamelCase)
        {
            return Regex.Replace(inputCamelCase, "(\\B[A-Z])", " $1");
        }

        /// <summary>
        /// Converts values of List to text separated by comma
        /// </summary>
        public static string ToStringExtended(this IList<string> list)
        {
            return string.Join(", ", list.ToArray());
        }

        /// <summary>
        /// Converts values of Dictionary to text separated by comma
        /// </summary>
        public static string ToStringExtended(this IDictionary<string, string> dict)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var elm in dict)
                sb.AppendFormat("{0}: {1}\n", elm.Key, elm.Value);
            return sb.ToString();
        }

        /// <summary>
        /// Adds to list of key if key allready exists (Extended ADD with feature of used IList as value)
        /// </summary>
        public static IDictionary<string, IList<string>> AddToListValue(this IDictionary<string, IList<string>> dict, string key, string value)
        {
            IList<string> existingKey = null;

            if (!dict.TryGetValue(key, out existingKey))
            {
                existingKey = dict[key] = new List<string>();
            }

            existingKey.Add(value);

            return dict;
        }

        /// <summary>
        /// Return list of from hashset objects of IPESubclass
        /// </summary>
        public static IList<string> GetList<IPESubclass>(this HashSet<IPESubclass> exports) => exports.Select(x => x.ToString()).ToList();

        /// <summary>
        /// Return JSON format of File report object
        /// </summary>
        public static string ToJson(this IFileReport obj) => JsonSerializer.Serialize(obj);

        /// <summary>
        /// Return JSON format of List with File report objects
        /// </summary>
        public static string ToJson(this IList<IFileReport> obj) => JsonSerializer.Serialize(obj);

        /// <summary>
        /// Return IFileReport from JSON file
        /// </summary>
        public static FileReportRecovered ReportFromJson(string json)
        {
            return JsonSerializer.Deserialize<FileReportRecovered>(json);
        }

        /// <summary>
        /// Return list of IFileReports from JSON file
        /// </summary>
        public static IList<FileReportRecovered> ListFromJson(string json)
        {
            return JsonSerializer.Deserialize<IList<FileReportRecovered>>(json);
        }

        /// <summary>
        /// Calc bits per byte entropy of byte array
        /// https://kennethghartman.com/calculate-file-entropy/
        /// </summary>
        public static double Entropy(this byte[] data)
        {
            var counts = data.GroupBy(n => n)
                    .Select(c => new { Key = c.Key, Total = c.Count() });

            double ent = 0;

            foreach( var freq in counts)
            {
                double f = (double)freq.Total / data.Length;

                if (f > 0)
                    ent -= f * (Math.Log(f) / Math.Log(2)); // Math.Log(f, 2);
            }

            return Math.Abs(ent);
        }

        /// <summary>
        /// Converts FileReport to ML.NET format
        /// </summary>
        public static FileReportML ConvertML(this IFileReport report)
        {
            return FileReportML.Convert(report);
        }
    }
}
