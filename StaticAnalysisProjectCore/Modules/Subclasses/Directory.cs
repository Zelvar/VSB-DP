using PeNet.Header.Pe;
using System;
using System.Collections.Generic;
using System.Text;

namespace StaticAnalysisProject.Modules.Subclasses
{
    class Directory : IPESubclass
    {
        private ImageDataDirectory _directory = null;
        private DataDirectoryType _type;

        public uint VirtualAddr { get; private set; }

        public uint VirtualSize { get; private set; }

        public bool IsEmpty => VirtualSize == 0;

        public string Name => !this.IsEmpty ? Enum.GetName(typeof(DataDirectoryType), _type) : "";

        #region Constructors
        public Directory(ImageDataDirectory dir, DataDirectoryType type)
            : this(type, dir.VirtualAddress, dir.Size)
        {
            this._directory = dir;
        }

        public Directory(DataDirectoryType type, uint addr, uint size)
        {
            this._type = type;
            this.VirtualAddr = addr;
            this.VirtualSize = size;
        }

        public Directory(string name, uint addr, uint size)
        {
            this._type = (DataDirectoryType)Enum.Parse(typeof(DataDirectoryType), name);
            this.VirtualAddr = addr;
            this.VirtualSize = size;
        }
        #endregion

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
