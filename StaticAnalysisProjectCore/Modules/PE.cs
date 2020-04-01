//using StaticAnalysisProject.Classes; Depracated
using PeNet;

namespace StaticAnalysisProject.Modules
{
    public class PE : IModul
    {
        public string GetModulDescription() => "";

        public string GetModulName() => "PE Analysis";

        public void LoadFile(string filename)
        {

        }



        public string ToString()
        {
            throw new System.NotImplementedException();
        }
    }
}
