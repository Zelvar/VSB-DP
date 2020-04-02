using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StaticAnalysisProject.Helpers
{
    public static class ExtensionHelpers
    {
        public static string ToWords(this string inputCamelCase)
        {
            return Regex.Replace(inputCamelCase, "(\\B[A-Z])", " $1");
        }

        public static string ToStringExtended(this IList<string> list)
        {
            return string.Join(", ", list.ToArray());
        }

        public static string ToStringExtended(this IDictionary<string, string> dict)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var elm in dict)
                sb.AppendFormat("{0}: {1}\n", elm.Key, elm.Value);
            return sb.ToString();
        }
    }
}
