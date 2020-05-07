using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using StaticAnalysisProject;
using StaticAnalysisProject.Modules;

using System.Text.Json;
using System.Text.Json.Serialization;

using StaticAnalysisProject.Helpers;
using StaticAnalysisProject.ML;
using System.Linq;

namespace DP.Tests
{
    class Program
    {
        class A
        {
            public string Name;

            public override int GetHashCode()
            {
                return BitConverter.ToInt32(
                    SHA256.Create().ComputeHash(
                        Encoding.UTF8.GetBytes(this.Name)
                        ), 
                    0);
            }

            public override bool Equals(object obj) 
            {
                return this.Name.Equals(((A)obj).Name);
            }
        }

        static void Main(string[] args)
        {
            #region TEST
            //var a = new AFile(@"C:\Users\Zelvar\source\repos\KeyLoggerVSB\KeyLoggerVSB\bin\Release\App.exe");
            //Console.Write(new Hashes(File.ReadAllBytes(@"C:\Users\Zelvar\source\repos\KeyLoggerVSB\KeyLoggerVSB\bin\Release\App.exe")).ToString());
            //Console.WriteLine();
            //Console.Write(new PE(@"D:\SW\Arduino\Arduino.exe").ToString());
            //Console.WriteLine();
            //Console.Write(new PE(@"E:\SW\RadioDJv2\bass.dll").ToString());

            /*HashSet<A> abc = new HashSet<A>();
            var a = new A();
            var b = new A();
            a.Name = "123";
            b.Name = "123";

            abc.Add(a);
            abc.Add(b);

            Console.WriteLine(abc.Count);

            foreach (var obj in abc) {

                Console.WriteLine("{0} {1}", obj.GetHashCode(), obj.Name);
            
            }*/

            //////////////////////////////////////////////

            //new Strings(@"D:\SW\Arduino\Arduino.exe");
            //var a = new Strings(@"C:\Users\Zelvar\source\repos\KeyLoggerVSB\KeyLoggerVSB\bin\Release\App.exe");
            //var a = new Strings(@"D:\SW\Arduino\Arduino.exe");

            //Console.WriteLine(a.ContainsIP("10.0.0.1"));
            //Console.WriteLine(a.ContainsIP("FFFF::"));
            //Console.WriteLine(a.ContainsIP("10.A.0.1"));
            //Console.WriteLine(a.ContainsIP("10.0.0.1"));
            //Console.WriteLine(a.ContainsIP("FFFF::"));

            //Console.WriteLine(a.ContainsURL("http://10.A.0.1"));
            //Console.WriteLine(a.ContainsURL("http://blabla.com"));

            //Console.WriteLine(a.ContainsEmail("tralala zelvar@zelvar.cz"));

            //Console.WriteLine(a.ContainsFileName("soubor.dll"));
            //Console.WriteLine(a.ContainsFileName("Tralala soubor.rar"));
            //Console.WriteLine(a.ContainsFileName("Tralala"));

            /*
            foreach (var ip in a.ExtractIP())
            {
                Console.WriteLine("IP: {0}", ip.ToString());
            }
            
            foreach (var mail in a.ExtractMail())
            {
                Console.WriteLine("Mail: {0}", mail.ToString());
            }
            
            foreach (var url in a.ExtractURL())
            {
                Console.WriteLine("Url: {0}", url.ToString());
            }

            foreach (var file in a.ExtractFiles())
            {
                Console.WriteLine("File: {0}", file.ToString());
            }

            foreach (var file in a.ExtractKnownMethods())
            {
                Console.WriteLine("Known methods: {0}", file.ToString());
            }*/

            //Console.WriteLine(new VirusTotal(@"C:\Users\Zelvar\source\repos\KeyLoggerVSB\KeyLoggerVSB\bin\Release\App.exe").ToString());
            //Console.WriteLine(new Strings(@"C:\Users\Zelvar\source\repos\KeyLoggerVSB\KeyLoggerVSB\bin\Release\App.exe"));

            //Console.WriteLine("%%%%%% Keylogger");
            //new DetectWithYara(@"KeyLogger.ps1");
            //Console.WriteLine("%%%%%% Ransom");
            //new DetectWithYara(@"Ransomware_Encrypt.ps1");
            //Console.WriteLine("%%%%%% Botnet");
            //new DetectWithYara(@"Botnet_Slave.ps1");3
            //new DetectWithYara(@"E:\Steam\steamapps\common\Grand Theft Auto V\GTA5.exe");
            #endregion

            //IFileReport a = new FileReport(@"E:\Steam\steamapps\common\Grand Theft Auto V\GTA5.exe");
            //IFileReport a = new FileReport(@"E:\Steam\steamapps\common\Grand Theft Auto V\GTA5.exe");
            //IFileReport a = new FileReport(@"E:\Steam\steamapps\common\Grand Theft Auto V\GTA5.exe");
            //IFileReport a = new FileReport(@"C:\Users\Zelvar\source\repos\KeyLoggerVSB\KeyLoggerVSB\bin\Release\HookLibrary.dll", "malware");
            //string json = JsonSerializer.Serialize(a);

            //Console.WriteLine(json);

            //byte[] a = File.ReadAllBytes(@"E:\Steam\steamapps\common\Grand Theft Auto V\GTA5.exe");

            //Console.WriteLine(a.Entropy());


            //Build model
            /*
             var ent = new MachineLearning();

            var data = Directory.GetFiles(@"W:\DP-Vystup\malware\malware", "*", SearchOption.AllDirectories).ToArray();

            foreach(var file in data)
            {
                Console.WriteLine("{1} is malware: {0}", ent.Predict(file).IsMalware, file);
            }

            //Console.WriteLine("Is malware: {0}", ent.Predict(@"W:\DP-Vystup\malware\Adobe, Acrobat PDF Writer 3._exe").IsMalware);

            Console.WriteLine("Is malware: {0}", ent.Predict(@"C:\Users\Zelvar\source\repos\KeyLoggerVSB\KeyLoggerVSB\bin\Release\HookLibrary.dll").IsMalware);
            Console.WriteLine("Is malware: {0}", ent.Predict(@"C:\Users\Zelvar\source\repos\KeyLoggerVSB\KeyLoggerVSB\bin\Release\App.exe").IsMalware);
            Console.WriteLine("Is malware: {0}", ent.Predict(@"C:\Users\Zelvar\source\repos\FontCreator\FontCreator\bin\Debug\FontCreator.exe").IsMalware);
            
            Console.WriteLine("Is malware: {0}", ent.Predict(@"E:\Steam\steamapps\common\Grand Theft Auto V\GTA5.exe").IsMalware);
            
            Console.WriteLine("Is malware: {0}", ent.Predict(@"C:\Users\Zelvar\source\repos\DLLInjection\x64\Release\Dll-compresed.dll").IsMalware);
            Console.WriteLine("Is malware: {0}", ent.Predict(@"C:\Users\Zelvar\source\repos\DLLInjection\x64\Release\Dll.dll").IsMalware);
            Console.WriteLine("Is malware: {0}", ent.Predict(@"C:\Users\Zelvar\source\repos\KeyLoggerVSB\KeyLoggerVSB\bin\Release\HookLibrary.dll").IsMalware);
            Console.WriteLine("Is malware: {0}", ent.Predict(@"C:\Users\Zelvar\source\repos\KeyLoggerVSB\KeyLoggerVSB\bin\Release\HookLibrary.dll").IsMalware);

            //Console.WriteLine("Val: {0} Efc: {1}", ent.Value, ent.Efficiency);

            //Console.WriteLine(ent.ToString());

            //byte[] b = new byte[] { (byte)'A',(byte)'h',(byte)'o',(byte)'j',(byte)' ',(byte)'j',(byte)'a',(byte)'k',(byte)' ',(byte)'s',(byte)'e',(byte)' ',(byte)'m',(byte)'a',(byte)'s'};

            //Console.WriteLine(b.Entropy());


            /*var hs = new HashSet<string>();
            IList<IFileReport> _fileReports = new List<IFileReport>();

            IList<FileReportRecovered> list = ExtensionHelpers.ListFromJson(File.ReadAllText("malware.json"));
            //IList<FileReportRecovered> list = ExtensionHelpers.ListFromJson(File.ReadAllText("software.json"));
            _fileReports = _fileReports.Concat(list).ToList();
            //_fileReports = _fileReports.Concat(list2).ToList();

            foreach (var a in _fileReports) {

                if(a.Behavior.Contains(""))

                foreach (var b in a.Behavior)
                {
                    hs.Add(b.ToString());
                }
            }

            foreach(var a in hs)
            {
                Console.WriteLine(a);
            }*/


        }
    }
}
