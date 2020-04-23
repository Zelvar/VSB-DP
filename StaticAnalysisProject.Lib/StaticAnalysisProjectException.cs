using System;
using System.Collections.Generic;
using System.Text;

namespace StaticAnalysisProject
{
    public class StaticAnalysisProjectException : Exception
    {
        private string _error = "";
        public StaticAnalysisProjectException(string error = "")
        {
            this._error = error;
        }

        public override string ToString()
        {
            return _error.ToString();
        }
    }
}
