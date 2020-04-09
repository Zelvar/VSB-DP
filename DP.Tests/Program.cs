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
            new Strings(@"C:\Users\Zelvar\source\repos\KeyLoggerVSB\KeyLoggerVSB\bin\Release\App.exe");

        }
    }
}
