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
        private Rules _compiledRules = null;
        #endregion
        #region Default props
        public string GetModulDescription() => "";

        public string GetModulName() => "Yara deep analysis";
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

        ~DetectWithYara()
        {
            _compiledRules.Dispose();
        }
        #endregion

        private void LoadYaraRules()
        {
            //Load rules from files
            this._yaraRules = Directory.GetFiles(this._pathToRules, "*.yara", SearchOption.AllDirectories).ToArray();
            /*using (YaraContext ctx = new YaraContext())
            {
                using (Compiler compiler = new Compiler())
                {
                    foreach (var rule in _yaraRules)
                    {
                        compiler.AddRuleFile(rule);
                    }

                    _compiledRules = compiler.Compile();
                }
            }*/

            /*using (var ctx = new YaraContext())
            {
                    // Rules and Compiler objects must be disposed.
                using (var compiler = new Compiler())
                {
                    foreach (var rule in _yaraRules)
                    {
                        compiler.AddRuleFile(rule);
                    }
                    _compiledRules = compiler.GetRules();
                }
            }*/

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

                foreach(var a in results)
                {
                    Console.WriteLine(a.MatchingRule.Identifier);
                }
                // Rules and Compiler objects must be disposed.
                if (rules != null) rules.Dispose();
            }

            Debug.WriteLine("* Compiled");
        }

        private void ScanFile()
        {
            /*if (_compiledRules != null)
            {
                var scanner = new Scanner();
                List<ScanResult> scanResults = scanner.ScanFile(@"C:\Users\Zelvar\source\repos\KeyLoggerVSB\KeyLoggerVSB\bin\Release\App.exe", _compiledRules);
                
                foreach(var a in scanResults)
                {
                    Console.WriteLine(a.ToString());
                }
            }*/

            // Scanner and ScanResults do not need to be disposed.
            var scanner = new Scanner();
            var results = scanner.ScanFile(this._filename, _compiledRules);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
