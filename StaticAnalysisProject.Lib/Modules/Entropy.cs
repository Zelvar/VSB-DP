using StaticAnalysisProject.Helpers;
using System.IO;
using System.Text;

namespace StaticAnalysisProject.Modules
{
    public class Entropy : IModul
    {
        //Testing value result is ~3.0
        //41 48 4F 4A  4A 20 4A 41 4B 20 53 45 20 4D 41 53
        //https://asecuritysite.com/encryption/ent
        #region DATA
        private int _fileSize = 0;
        #endregion

        #region Getters
        /// <summary>
        /// Returns value of calculated shannon entropy
        /// </summary>
        public double Value { get; private set; }

        /// <summary>
        /// Calculate percentage of compression efficiency
        /// </summary>
        public double Efficiency => TheoreticalByteSize / this._fileSize;

        /// <summary>
        /// Minimal possible theoretical file size
        /// </summary>
        public double TheoreticalByteSize => ((this.Value * this._fileSize) / 8);

        #endregion
        #region Constructors
        /// <summary>
        /// Constructor that load file
        /// </summary>
        public Entropy(string filename)
            : this(File.ReadAllBytes(filename), filename)
        { }

        /// <summary>
        /// Constructor that load byte array and calc shannon entropy
        /// </summary>
        private Entropy(byte[] file, string filename = "")
        {
            _fileSize = file.Length;
            this.Value = file.Entropy();
        }
        #endregion
        #region Default props
        public string GetModulDescription()
        {
            return "Returns shannon entropy of file";
        }

        public string GetModulName()
        {
            return "Shannon entropy";
        }
        #endregion
        #region Basic methods
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Shannon entropy of file:");
            sb.AppendLine(string.Format("{0:n2}", this.Value));

            sb.AppendLine("Theoretical minimal file size:");
            sb.AppendLine(string.Format("{0:n2} bytes", this.TheoreticalByteSize));

            sb.AppendLine("Compression efficiency:");
            sb.AppendLine(string.Format("{0:n2}", this.Efficiency));

            return sb.ToString();
        }
        #endregion
    }
}
