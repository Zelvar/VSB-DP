using System;
using System.Diagnostics;

namespace StaticAnalysisProject.Console
{
    class Program
    {
        static void Main(string[] args)
            {
            if (args.Length >= 1)
            {
                #region Console report
                try
                {
                    CmdHandler.RunCommand(args);
                } catch( Exception e)
                {
                    Debug.WriteLine(e.Message.ToString());
                }
                #endregion
            }
            else
            {
                #region Interactive console Window
                #region Console welcome
                for (int i = 0; i < 20; i++) System.Console.Write("#");
                System.Console.WriteLine();
                System.Console.WriteLine("## Welcome!");
                for (int i = 0; i < 20; i++) System.Console.Write("#");
                System.Console.WriteLine();
                #endregion
                #region Console process
                while (true)
                {
                    System.Console.Write("# ");
                    string cmd = System.Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(cmd))
                        continue;
                    else
                        CmdHandler.ExecuteCommand(cmd);
                }
                #endregion
                #endregion
            }
        }
    }
}
