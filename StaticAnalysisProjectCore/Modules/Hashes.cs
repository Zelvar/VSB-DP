using StaticAnalysisProject.Helpers.Hash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StaticAnalysisProject.Modules
{
    public class Hashes : IModul
    {
        public string GetModulDescription()
        {
            throw new NotImplementedException();
        }

        public string GetModulName()
        {
            throw new NotImplementedException();
        }

        public Hashes(byte[] file)
        {
            var hashType = typeof(IHash);
            var types = AppDomain.CurrentDomain.
                GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(a => hashType.IsAssignableFrom(a));
            
            foreach(var a in types)
            {
                Console.WriteLine(a.ToString());
            }
        }
    }
}
