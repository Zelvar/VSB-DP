using System;

namespace StaticAnalysisProject.Console
{
    class Program
    {
        static void Main(string[] args)
        {
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
        }
    }
}
