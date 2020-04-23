using PeNet.Header.Pe;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace StaticAnalysisProject.Modules.Subclasses
{
    class Section : IPESubclass
    {
        public string Name { get; private set; }
        public uint VirtualSize { get; private set; }
        public uint VirtualAddr { get; private set; }
        public uint PhysicalSize { get; private set; }
        public uint PhysicalAddr { get; private set; }

        public IList<string> Characteristcs { get; private set; }

        public bool IsEmpty => VirtualSize == 0;

        #region Constructors
        public Section(
            ImageSectionHeader section
        ) : this(
            section.Name, 
            section.VirtualSize, 
            section.VirtualAddress, 
            section.SizeOfRawData, 
            section.PointerToRawData,
            section.CharacteristicsResolved
        ){}

        public Section(
            string name, 
            uint vSize, 
            uint vAddr,
            uint rSize,
            uint rAddr,
            IList<string> characteristics
         ){
            this.Name = name;
            this.VirtualSize = vSize;
            this.VirtualAddr = vAddr;
            this.PhysicalAddr = rAddr;
            this.PhysicalSize = rSize;
            this.Characteristcs = characteristics;
        }
        #endregion
        #region Overrided methods
        /// <summary>
        /// Override default method GetHashCode
        /// </summary>
        public override int GetHashCode()
        {
            return Convert.ToInt32(this.VirtualAddr);
        }

        /// <summary>
        /// Check if library exists
        /// </summary>
        public override bool Equals(object obj)
        {
            return this.VirtualAddr.Equals(((Section)obj).VirtualAddr);
        }
        #endregion
    }
}
