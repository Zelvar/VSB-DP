using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using StaticAnalysisProject;
using StaticAnalysisProject.Modules;

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

            Console.WriteLine("%%%%%% Keylogger");
            new DetectWithYara(@"KeyLogger.ps1");
            Console.WriteLine("%%%%%% Ransom");
            new DetectWithYara(@"Ransomware_Encrypt.ps1");
            Console.WriteLine("%%%%%% Botnet");
            new DetectWithYara(@"Botnet_Slave.ps1");
        }
    }
}
