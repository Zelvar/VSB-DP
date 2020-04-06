using PeNet.Header.Pe;
using System;
using System.Collections.Generic;
using System.Text;

namespace StaticAnalysisProject.Modules.Subclasses
{
    class Directory
    {
        private ImageDataDirectory _directory = null;
        private DataDirectoryType _type;

        public uint VirtualAddr { get; private set; }

        public uint VirtualSize { get; private set; }

        public bool IsEmpty => VirtualSize == 0;
        public string Name => this.IsEmpty ? _type.ToString() : "";

        public Directory(ImageDataDirectory dir, DataDirectoryType type)
        {
            this._directory = dir;
            this._type = type;

            this.VirtualAddr = dir.VirtualAddress;
            this.VirtualSize = dir.Size;
        }

        public Directory(string name, uint addr, uint size)
        {
            this._type = (DataDirectoryType)Enum.Parse(typeof(DataDirectoryType), name);
            this.VirtualAddr = addr;
            this.VirtualSize = size;
        }
    }
}
