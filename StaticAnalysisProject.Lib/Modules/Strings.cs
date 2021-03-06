﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StaticAnalysisProject.Helpers;

namespace StaticAnalysisProject.Modules
{
    #region Regex attributes for String analysis
    //Static data holding regex attributes
    internal static class StringRegex
    {
        //IP addresses
        //IPV4 regex source: https://ipregex.com/ https://stackoverflow.com/questions/4890789/regex-for-an-ip-address
        //IPV6 regex source: https://github.com/richb-intermapper/IPv6-Regex/blob/master/ipv6validator.js
        public const string IPv4 = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b";
        public const string IPv6 = @"((([0-9A-Fa-f]{1,4}:){7}([0-9A-Fa-f]{1,4}|:))|(([0-9A-Fa-f]{1,4}:){6}(:[0-9A-Fa-f]{1,4}|((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){5}(((:[0-9A-Fa-f]{1,4}){1,2})|:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){4}(((:[0-9A-Fa-f]{1,4}){1,3})|((:[0-9A-Fa-f]{1,4})?:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){3}(((:[0-9A-Fa-f]{1,4}){1,4})|((:[0-9A-Fa-f]{1,4}){0,2}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){2}(((:[0-9A-Fa-f]{1,4}){1,5})|((:[0-9A-Fa-f]{1,4}){0,3}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){1}(((:[0-9A-Fa-f]{1,4}){1,6})|((:[0-9A-Fa-f]{1,4}){0,4}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(:(((:[0-9A-Fa-f]{1,4}){1,7})|((:[0-9A-Fa-f]{1,4}){0,5}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:)))";

        //Mail regex source https://emailregex.com/
        public const string Email = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

        //URL regex source: https://urlregex.com/
        public const string Url = @"((smb)|(ssh)|(file)|(ht|f)tp(s?))\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?";
    }
    #endregion
    public class Strings : IModul
    {
        /// <summary>
        /// Analyse strings
        /// - URL           - DONE
        /// - IP            - DONE
        /// - EMAIL         - DONE
        /// - File names (USE MIMES - https://gist.github.com/AshHeskes/6038140) - DONE
        /// - Antidbg / Apialert / Mutex / msvcrt (Use these list https://github.com/Cisco-Talos/BASS/blob/master/bass/files/json/api_db.json) - DONE
        /// - Filter non asci characters - https://gist.githubusercontent.com/splorp/5177956/raw/84486959671c265ce25cf9ed6fb6da1a10302e88/ascii.txt - DONE
        /// </summary>

        #region DATA
        //Settings
        private const uint MIN_LENGTH = 3;
        private const string READABLE_ASCII_CHARS = "!\"#$%&()*+'-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~€‚ƒ„…†‡ˆ‰Š‹ŒŽ“”•–—˜™š›œžŸ¡¢£¤¥¦§¨©ª«¬®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿ\t\n\r ";

        //Loaded and splited data
        private byte[] _rawFile = null;
        private byte[][] _rawFileSplitedByZero = null;
        private string _rawFileString = "";

        //Hold filename
        private string _filename = "";

        //Prepare for analysis
        private IList<string> _fileExtensions = null;
        //private IList<Regex> _fileExtensionsRegexs = new List<Regex>();
        private IDictionary<string, IList<string>> _knownMethods = null;

        private Regex _fileextensionsRegex = null;

        //Hold data
        private IList<string> _ips = null;
        private IList<string> _urls = null;
        private IList<string> _mails = null;
        private IList<string> _files = null;
        private IDictionary<string, IList<string>> _knownmethods = null;
        private IList<string> _knownmethodslist = null;
        private string _filteredstring = "";
        #endregion
        #region Default props
        public string GetModulDescription() => "Loads and filter strings of executable file";

        public string GetModulName() => "Strings";
        #endregion
        #region Helpers
        /// <summary>
        /// Converts byte array to UTF8 string
        /// </summary>
        private string ConvertToString(byte[] file)
        {
            return string.Format("{0}", Encoding.UTF8.GetString(file, 0, file.Length));
        }
        #endregion
        #region Search for important content
        /// <summary>
        /// Loads file extensions from json file
        /// Prepare file regexs
        /// </summary>
        private void LoadFileExtensions()
        {
            using (StreamReader sr = new StreamReader(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), DataHelper.DataFileTypesPath)))
            {
                string json = sr.ReadToEnd();
                var data = ((JObject)JsonConvert.DeserializeObject(json)).Properties().Select(x => x.Name).ToList();
                _fileExtensions = data;
            }

            //Prepare regex
            //_fileExtensionsRegexs = new List<Regex>();
            //foreach (var fileext in _fileExtensions)
            //{
            //var extRegex = @"([\w\d\-.]+\." + fileext.Replace(".", "") + @")(\b|\s)";
            //_fileExtensionsRegexs.Add(new Regex(extRegex));
            //}

            string fileExtensions = string.Join("|",
                        _fileExtensions
                            .Select(x => string.Format("{0}", x.Replace(".", ""))).ToList()
                );

            _fileextensionsRegex = new Regex(@"([\w\d\-.]+\.(" + fileExtensions + @"))(\b|\s)");
        }
        /// <summary>
        /// Loads known keywords from json file
        /// </summary>
        private void LoadKnownMethods()
        {
            var dic = new Dictionary<string, IList<string>>();
            using(StreamReader sr = new StreamReader(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), DataHelper.DataKnownMethodsPath))) 
            {
                string json = sr.ReadToEnd();
                var data = (JObject)JsonConvert.DeserializeObject(json);

                foreach(var method in data.Properties())
                {
                    dic.Add(method.Name, method.Value.Select(x => x.ToString()).ToList());
                }
            }

            _knownMethods = dic;
        }
        /// <summary>
        /// Check if string contains IP (ipv6 / ipv4)
        /// </summary>
        public bool ContainsIP(string ip)
        {
            return Regex.Match(ip, StringRegex.IPv4).Success || Regex.Match(ip, StringRegex.IPv6).Success;
        }
        /// <summary>
        /// Check if string contains filename
        /// </summary>
        public bool ContainsFileName(string filename)
        {
            if(_fileExtensions == null)
                LoadFileExtensions(); //Load all filetypes

            /*foreach (var fileext in _fileExtensions) 
            {
                var extSearch = @"([\w\d\-.]+\." + fileext.Replace(".", "") + @")(\b|\s)";
                if (Regex.Match(filename, extSearch).Success) {
                    return true; 
                }

            }*/

            /*foreach( var regex in _fileExtensionsRegexs)
            {
                if (regex.Match(filename).Success)
                {
                    return true;
                }
            }*/

            if (_fileextensionsRegex.Match(filename).Success) return true;

            return false;
        }
        /// <summary>
        /// Check for EMAIL in string
        /// </summary>
        public bool ContainsEmail(string mail)
        {
            return Regex.Match(mail, StringRegex.Email).Success;
        }
        /// <summary>
        /// Check for URL in string
        /// </summary>
        public bool ContainsURL(string url)
        {
            return Regex.Match(url, StringRegex.Url).Success; 
        }

        /// <summary>
        /// Extracting all IPs from strings
        /// </summary>
        public IList<string> ExtractIP()
        {
            var ipv4 = Regex.Matches(_rawFileString, StringRegex.IPv4)       //IPv4 list
                .Cast<Match>()
                .Select(m => m.Value)
                .ToList();

            var ipv6 = Regex.Matches(_rawFileString, StringRegex.IPv6)       //IPv6 list
                .Cast<Match>()
                .Select(m => m.Value)
                .ToList();

            return ipv4.Concat(ipv6).Distinct().ToList();
        }

        /// <summary>
        /// Extracting all files from strings
        /// </summary>
        public IList<string> ExtractFiles()
        {
            string file = _rawFileString;
            IList<string> list = new List<string>();

            if (_fileExtensions == null) LoadFileExtensions();

            /*foreach (var regex in _fileExtensionsRegexs)
            {
                var output = regex.Matches(file)
                    .Cast<Match>()
                    .Select(m => m.Value)
                    .ToList();

                list = list.Concat(output).ToList();
            }*/

            var output = _fileextensionsRegex.Matches(file)
                    .Cast<Match>()
                    .Select(m => m.Value)
                    .Distinct()
                    .ToList();

            return output;
        }

        /// <summary>
        /// Extracts all URLS from strings
        /// </summary>
        public IList<string> ExtractURL()
        {
            var urls = Regex.Matches(_rawFileString, StringRegex.Url)
                .Cast<Match>()
                .Select(m => m.Value)
                .Distinct()
                .ToList();

            return urls;
        }

        /// <summary>
        /// Extracts all MAILs from strings
        /// </summary>
        /// <returns></returns>
        public IList<string> ExtractMail()
        {
            var mails = Regex.Matches(_rawFileString, StringRegex.Email)
                .Cast<Match>()
                .Select(m => m.Value)
                .Distinct()
                .ToList();

            return mails;
        }
        
        /// <summary>
        /// Extracts all known methods to list
        /// </summary>
        public IDictionary<string, IList<string>> ExtractKnownMethods()
        {
            if (_knownMethods == null) LoadKnownMethods();

            IDictionary<string, IList<string>> methods = new Dictionary<string, IList<string>>();

            foreach (var item in _knownMethods)
            {
                foreach(var method in item.Value)
                {
                    if (_rawFileString.Contains(method))
                    {
                        //methods.Add(string.Format("{0}: {1}", item.Key, method));
                        methods.AddToListValue(item.Key, method);
                    }
                }
            }

            return methods;
        }
        #endregion
        #region Constructors
        /// <summary>
        /// Loads file by filename
        /// </summary>
        public Strings(string filename) 
            : this(File.ReadAllBytes(filename), filename)
        {
            this._filename = filename;
        }

        /// <summary>
        /// Loads file from array of bytes
        /// </summary>
        public Strings(byte[] fileArray, string filename = "")
        {
            this._rawFile = fileArray;
            this._filename = filename;

            List<byte[]> separetedFile = new List<byte[]>();
            _rawFileString = ConvertToString(_rawFile);
            StringBuilder sb = new StringBuilder();

            object myLock = new object();
            //First basic analysis
            foreach (var item in _rawFile.Split(new byte[] { 0 }))
            //Parallel.ForEach(_rawFile.Split(new byte[] { 0 }), (item) =>
            {
                if (item.Length > MIN_LENGTH)
                {
                    bool add = true;
                    foreach (var c in item)
                    {
                        if (
                            //!this.ContainsFileName(ConvertToString(item)) && //this operation is too slow
                            //!this.ContainsEmail(ConvertToString(item)) && //this operation can take tooo long
                            //!this.ContainsURL(ConvertToString(item)) &&
                            //!this.ContainsIP(ConvertToString(item)) &&
                            !READABLE_ASCII_CHARS.Contains((char)c)
                           )
                        {
                            add = false;
                            break;
                        }
                    }

                    if (add)
                    {
                        lock (myLock)
                        {
                            separetedFile.Add(item);
                            sb.AppendLine(ConvertToString(item));
                        }
                    }
                }
            }//);
            this._rawFileSplitedByZero = separetedFile.ToArray();

            //Second part load known stuffs
            this._files = this.ExtractFiles();
            this._ips = this.ExtractIP();
            this._mails = this.ExtractMail();
            this._urls = this.ExtractURL();
            this._knownmethods = this.ExtractKnownMethods();
            this._filteredstring = sb.ToString();
        }
        #endregion
        #region Getters
        /// <summary>
        /// Getter for list of found IPs
        /// </summary>
        /// <returns></returns>
        public IList<string> GetIPs()
        {
            if (_ips == null) _ips = this.ExtractIP();

            return this._ips;
        }

        /// <summary>
        /// Getter for list of found URL addresses
        /// </summary>
        public IList<string> GetURLs()
        {
            if (_urls == null) _urls = this.ExtractURL();

            return this._urls;
        }

        /// <summary>
        /// Getter for list of found mails
        /// </summary>
        public IList<string> GetMails()
        {
            if (_mails == null) _mails = this.ExtractMail();

            return this._mails;
        }

        /// <summary>
        /// Getter for list of found files
        /// </summary>
        public IList<string> GetFiles()
        {
            if (_files == null) _files = this.ExtractFiles();

            return this._files;

        }

        /// <summary>
        /// Getter for list of used known methods in list
        /// </summary>
        public IList<string> GetKnownMethods()
        {
            if (_knownmethods == null) _knownmethods = this.ExtractKnownMethods();
            if (_knownmethodslist == null)
            {
                _knownmethodslist = new List<string>();
                foreach (var key in this._knownmethods)
                {
                    _knownmethodslist.Add(string.Format("{0}: {1}", key.Key, string.Join(", ", key.Value.ToArray())));
                }
            }

            return this._knownmethodslist;
        }

        /// <summary>
        /// Getter for formated methods list, similliar to GetKnownMethods()
        /// </summary>
        public IDictionary<string, IList<string>> GetKnownMethodsInDictionary() 
        {
            if (_knownmethods == null) _knownmethods = this.ExtractKnownMethods();

            return _knownmethods; 
        }

        /// <summary>
        /// Getter for filtred string
        /// </summary>
        public string GetFilteredString() => _filteredstring;
        #endregion
        #region Basic methods
        /// <summary>
        /// Overrided ToString method to get report simply
        /// </summary>
        public string ToString(bool filtredStrings = true)
        {
            StringBuilder sb = new StringBuilder();

            //Filtered string
            if (filtredStrings && this.GetFilteredString() != "")
            {
                sb.AppendLine("Filtered string:");
                sb.AppendLine(string.Format("{0}", this.GetFilteredString()));
            }

            //List of IPs
            if (this.GetIPs() != null 
                && this.GetIPs().Count > 0)
            {
                sb.AppendLine("Found IP addreses:");
                foreach (var ip in this.GetIPs())
                {
                    sb.AppendLine(string.Format("IP: {0}", ip.ToString()));
                }
            }

            //List of Mails
            if (this.GetMails() != null 
                && this.GetMails().Count > 0)
            {
                sb.AppendLine("Found mail addreses:");
                foreach (var mail in this.GetMails())
                {
                    sb.AppendLine(string.Format("Mail: {0}", mail.ToString()));
                }
            }

            //List of URLs
            if (this.GetURLs() != null 
                && this.GetURLs().Count > 0)
            {
                sb.AppendLine("Found url addreses:");
                foreach (var url in this.GetURLs())
                {
                    sb.AppendLine(string.Format("Url: {0}", url.ToString()));
                }
            }

            //List of files
            if (this.GetFiles() != null 
                && this.GetFiles().Count > 0)
            {
                sb.AppendLine("Found files:");
                foreach (var file in this.GetFiles())
                {
                    sb.AppendLine(string.Format("File: {0}", file.ToString()));
                }
            }

            //List of known methhods
            if (this.GetKnownMethods() != null 
                && this.GetKnownMethods().Count > 0)
            {
                sb.AppendLine("Found known methods:");
                foreach (var file in this.GetKnownMethods())
                {
                    sb.AppendLine(string.Format("Known methods: {0}", file));
                }
            }

            return sb.ToString();//this.ConvertToString(this._rawFile);
        }

        public override string ToString()
        {
            return this.ToString(true);
        }
        #endregion
    }
}
