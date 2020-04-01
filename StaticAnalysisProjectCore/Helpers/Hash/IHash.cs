using System;
using System.Collections.Generic;
using System.Text;

namespace StaticAnalysisProject.Helpers.Hash
{
    interface IHash{
        string GetHash(string filePath);
    }
}
