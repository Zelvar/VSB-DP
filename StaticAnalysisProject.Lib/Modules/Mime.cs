using HeyRed.Mime;
using System.IO;

namespace StaticAnalysisProject.Modules
{
    class Mime : IModul
    {
        #region Default props
        public string GetModulName()
        {
            return "MIME";
        }

        public string GetModulDescription()
        {
            return "Read MIME type of file based on magic number.";
        }
        #endregion
        #region DATA
        private byte[] _file;
        private string _filePath;

        private FileType _fileType;
        #endregion
        #region Getters
        /// <summary>
        /// Returns MIME
        /// </summary>
        public string GetMime() => this._fileType.MimeType.ToString();

        /// <summary>
        /// Returns instance of FileType
        /// </summary>
        public FileType GetFileType() => this._fileType;

        /// <summary>
        /// Return extension of MIME
        /// </summary>
        public string GetExtension() => this._fileType.Extension.ToString();
        #endregion
        #region Constructors
        /// <summary>
        /// Constructor that load file
        /// </summary>
        /// <param name="filename">Path to file</param>
        public Mime(string filename)
            : this(File.ReadAllBytes(filename), filename)
        {}

        /// <summary>
        /// Constructor that load byte array and get MIME
        /// </summary>
        private Mime(byte[] file, string filename = "")
        {
            this._file = file;
            this._filePath = filename;

            this._fileType = MimeGuesser.GuessFileType(this._filePath);
        }
        #endregion
        #region Modul code
        public override string ToString()
        {
            return this._fileType.MimeType.ToString();
        }
        #endregion
    }
}
