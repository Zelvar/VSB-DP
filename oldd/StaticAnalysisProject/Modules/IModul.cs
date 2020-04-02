using System;
using StaticAnalysisProject.Output;

namespace StaticAnalysisProject.Modules
{
    interface IModul
    {
        string GetModulName();
        string GetModulDescription();
        string ToString();
    }
}
