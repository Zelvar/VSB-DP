using dnYara;
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
        CompiledRules _compiledRules = null;
        #endregion
        #region Default props
        public string GetModulDescription() => "";

        public string GetModulName() => "Yara deep analysis";
        #endregion

        public DetectWithYara(string filename) 
            : this(File.ReadAllBytes(filename)) {
            this._filename = filename;
        }

        public DetectWithYara(byte[] file) {
            this._file = file;

            LoadYaraRules();
        }

        private void LoadYaraRules()
        {
            //Load rules from files
            this._yaraRules = Directory.GetFiles(this._pathToRules, ".yara", SearchOption.AllDirectories).ToArray();

            using (Compiler compiler = new Compiler())
            {
                foreach(var rule in _yaraRules)
                {
                    compiler.AddRuleFile(rule);
                }

                _compiledRules = compiler.Compile();
            }

            Debug.WriteLine("* Compiled");
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
