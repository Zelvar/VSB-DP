using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StaticAnalysisProject.Modules;

namespace StaticAnalysisProject.Console
{
    internal static class CmdHandler
    {
        #region DATA
        private static IList<MethodInfo> _listCommands = new List<MethodInfo>();
        public static string[] commands => _listCommands
            .Select(
                x => x.Name.ConvertMethodName()
            ).ToArray();
        #endregion

        #region Main
        /// <summary>
        /// Main handler that run commands
        /// </summary>
        internal static void ExecuteCommand(string cmd)
        {
            string[] command = cmd.Trim().Split(" ");

            if(commands.Length == 0)
                LoadCommands();

            //If command exists run command
            if (commands.Contains(command.First().ToLower()))
            {
                RunCommand(command);
            }
            else
            {
                System.Console.WriteLine("Error! Unknown command.");
            }
        }

        internal static string ConvertMethodName(this string method) => method.Replace("Cmd", "").Trim().ToLower();

        /// <summary>
        /// Loads list of commands
        /// </summary>
        internal static void LoadCommands()
        {
            Type type = typeof(CmdHandler);
            var methods = type.GetMethods()
                .Where(x => x.Name.StartsWith("Cmd"))
                .Select(x => x);

            _listCommands = methods.ToList();
        }

        /// <summary>
        /// Executor that run commands
        /// </summary>
        internal static void RunCommand(string[] command)
        {
            MethodInfo? cmd = _listCommands
                .Where(x => x.Name.ToLower().Equals(string.Format("{0}{1}", "cmd", command.First())))
                .Where(x => x.GetParameters().Length == command.Length - 1)
                .Select(x => x)
                .FirstOrDefault();

            //IF NULL?!
            if(cmd == null)
                System.Console.WriteLine("Error! Wrong number of parameters.");
            else if(cmd.GetParameters().Length == command.Length - 1)
            {
                IList<object> obj = new List<object>();
                foreach(var str in command.Skip(1).ToArray())   //Prepare parameters
                {
                    obj.Add(str);
                }

                cmd.Invoke(null, obj.ToArray());    //Call method
            }
        }
        #endregion
        #region Commands
        /// <summary>
        /// Clear command that cleans console
        /// </summary>
        public static void CmdClear()
        {
            System.Console.Clear();
        }

        /// <summary>
        /// Exit command that close console
        /// </summary>
        public static void CmdExit()
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Help command
        /// </summary>
        public static void CmdHelp()
        {
            System.Console.WriteLine("## Avaible commands");
            System.Console.WriteLine("# command is followed by its parameters");

            foreach(var method in _listCommands)
            {
                System.Console.WriteLine("> {0} {1}", method.Name.ConvertMethodName(), string.Join(" ", method.GetParameters().Select(x => x.Name)));
            }
        }

        /// <summary>
        /// Extract strings and check for known parameters of strings
        /// </summary>
        public static void CmdStrings(string filePath) {
            System.Console.WriteLine(new Strings(filePath).ToString());
        }

        /// <summary>
        /// Check PE file details
        /// </summary>
        public static void CmdPE(string filePath)
        {
            System.Console.WriteLine(new PE(filePath).ToString());
        }

        /// <summary>
        /// Test on virus total
        /// </summary>
        public static void CmdVirusTotal(string filePath)
        {
            System.Console.WriteLine(new VirusTotal(filePath).ToString());
        }
        #endregion
    }
}
