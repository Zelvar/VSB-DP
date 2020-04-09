using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using StaticAnalysisProject.Helpers;

namespace StaticAnalysisProject.Modules
{
    public class Strings : IModul
    {
        /// <summary>
        /// Analyse strings
        /// - URL
        /// - IP
        /// - EMAIL
        /// - File names (USE MIMES - https://gist.github.com/AshHeskes/6038140)
        /// - CSC / .NET
        /// - Antidbg / Apialert / Mutex / msvcrt (Use these list https://github.com/Cisco-Talos/BASS/blob/master/bass/files/json/api_db.json)
        /// - Filter non asci characters - https://gist.githubusercontent.com/splorp/5177956/raw/84486959671c265ce25cf9ed6fb6da1a10302e88/ascii.txt
        /// </summary>

        private const uint MIN_LENGTH = 3;
        private const string READABLE_ASCII_CHARS = "!\"#$%&()*+'-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~€‚ƒ„…†‡ˆ‰Š‹ŒŽ“”•–—˜™š›œžŸ¡¢£¤¥¦§¨©ª«¬®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿ\t\n";

        private byte[] _rawFile = null;
        private byte[][] _rawFileSplitedByZero = null;

        private string _filename = "";

        public string GetModulDescription() => "Loads and analyze strings of executable file";

        public string GetModulName() => "Strings";

        public override string ToString()
        {
            return this.ConvertToString(this._rawFile);
        }

        private string ConvertToString(byte[] file)
        {
            return string.Format("{0}", Encoding.UTF8.GetString(file, 0, file.Length));
        }

        /// <summary>
        /// Loads file by filename
        /// </summary>
        public Strings(string filename) 
            : this(File.ReadAllBytes(filename))
        {
            this._filename = filename;
        }

        /// <summary>
        /// Loads file from array of bytes
        /// </summary>
        public Strings(byte[] fileArray)
        {
            this._rawFile = fileArray;

            List<byte[]> separetedFile = new List<byte[]>();

            //TODO CLEAR UNWANTED CHARACTERS
            foreach(var item in _rawFile.Split(new byte[] { 0 }))
            {
                if(item.Length > MIN_LENGTH)
                {
                    bool add = true;
                    foreach(var c in item)
                    {
                        if(!READABLE_ASCII_CHARS.Contains((char)c))
                        {
                            add = false;
                            break;
                        }
                    }

                    if (add)
                    {
                        separetedFile.Add(item);
                        Console.WriteLine(ConvertToString(item));
                    }
                }
            }

            this._rawFileSplitedByZero = separetedFile.ToArray();
        }
    }
}
