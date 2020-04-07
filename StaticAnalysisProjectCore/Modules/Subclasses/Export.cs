using PeNet.Header.Pe;
using System;
using System.Collections.Generic;
using System.Text;

namespace StaticAnalysisProject.Modules.Subclasses
{
    class Export : IPESubclass
    {
        private ExportFunction _export;

        public string Name { get; private set; }

        public uint Addr { get; set; }

        public Export(ExportFunction fce)
            :this(fce.Name, fce.Address)
        {
            this._export = fce;
        }

        public Export(string name, uint addr)
        {
            this.Name = name;
            this.Addr = addr;
        }

        public override string ToString()
        {
            return this.Name.ToString();
        }
    }
}
