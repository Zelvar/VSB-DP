using System;
using System.Collections.Generic;
using System.Text;

namespace StaticAnalysisProject.Modules
{
    class Info : IModul
    {
        public string GetModulDescription() => "Basic info about executable file";
        public string GetModulName() => "Info";

        public override string ToString() {
            throw new NotImplementedException();
        }
    }
}
