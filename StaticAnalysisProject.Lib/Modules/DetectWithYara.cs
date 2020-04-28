//using dnYara;
using libyaraNET;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace StaticAnalysisProject.Modules
{
    public class DetectWithYara : IModul
    {
        //Rules https://github.com/Yara-Rules/rules
        //Cmake yara https://github.com/airbus-cert/yara/tree/cmake

        #region DATA
        private string _filename = null;
        private byte[] _file = null;

        private string _pathToRules = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\Yara\");
        private string[] _yaraRules = null;

        //YaraContext ctx = new YaraContext();
        //private Rules _compiledRules = null;
        private IList<string> _results = new List<string>();
        #endregion
        #region Default props
        public string GetModulDescription() => "";

        public string GetModulName() => "Yara deep analysis";
        #endregion
        #region Getters
        /// <summary>
        /// Returns results of tests
        /// </summary>
        public IList<string> GetResults() => this._results;
        #endregion
        #region Constructors
        public DetectWithYara(string filename) 
            : this(File.ReadAllBytes(filename), filename) {
            this._filename = filename;
        }

        private DetectWithYara(byte[] file, string filename = "") {
            this._file = file;
            this._filename = filename;

            LoadYaraRules();
            //ScanFile();
        }

        /*~DetectWithYara()
        {
            if(_compiledRules != null)
                _compiledRules.Dispose();
        }*/
        #endregion
        #region Methods
        /// <summary>
        /// Loads yara rules and run tests
        /// </summary>
        private void LoadYaraRules()
        {
            //Load rules from files
            this._yaraRules = Directory.GetFiles(this._pathToRules, "*.yara", SearchOption.AllDirectories).ToArray();
            this._results.Clear();

            try
            {
                using (var ctx = new YaraContext())
                {
                    Rules rules = null;

                    // Rules and Compiler objects must be disposed.
                    using (var compiler = new Compiler())
                    {
                        //string path = Path.Combine(this._pathToRules, "packer.yara");

                        foreach (var path in _yaraRules)
                        {
                            //Console.WriteLine(path);
                            compiler.AddRuleFile(path);
                        }

                        rules = compiler.GetRules();
                    }

                    // Scanner and ScanResults do not need to be disposed.
                    var scanner = new Scanner();
                    var results = scanner.ScanFile(this._filename, rules);

                    foreach (var a in results)
                    {
                        //Console.WriteLine(a.MatchingRule.Identifier);
                        this._results.Add(a.MatchingRule.Identifier.ToString());
                    }
                    // Rules and Compiler objects must be disposed.
                    if (rules != null) rules.Dispose();
                }
            }
            catch( Exception e )
            {
                Debug.WriteLine(e.ToString());
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Behavior test with Yara results:");
            sb.AppendLine(string.Join(", ", this.GetResults().ToArray()));

            return sb.ToString();
        }
        #endregion
    }
}
