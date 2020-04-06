using System;
using System.IO;
using StaticAnalysisProject;
using StaticAnalysisProject.Modules;

namespace DP.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            //var a = new AFile(@"C:\Users\Zelvar\source\repos\KeyLoggerVSB\KeyLoggerVSB\bin\Release\App.exe");
            Console.Write(new Hashes(File.ReadAllBytes(@"C:\Users\Zelvar\source\repos\KeyLoggerVSB\KeyLoggerVSB\bin\Release\App.exe")).ToString());
            Console.WriteLine();
            Console.Write(new PE(@"D:\SW\Arduino\Arduino.exe").ToString());
            Console.Write(new PE(@"E:\SW\RadioDJv2\bass.dll").ToString());

        }
    }
}
