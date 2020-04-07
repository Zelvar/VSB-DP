using PeNet.Header.Pe;
using StaticAnalysisProject.Modules.Subclasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StaticAnalysisProject.Helpers
{
    static class ExtensionHelpers
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
    }
}
