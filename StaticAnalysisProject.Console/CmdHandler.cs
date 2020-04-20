using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace StaticAnalysisProject.Console
{
    internal static class CmdHandler
    {
        #region DATA
        private static IList<MethodInfo> _listCommands = new List<MethodInfo>();
        public static string[] commands => _listCommands
            .Select(
                x => x.Name.Replace("Cmd", "").Trim().ToLower()
            ).ToArray();
        #endregion

        #region Main
        /// <summary>
        /// Main handler that run commands
        /// </summary>
        internal static void ExecuteCommand(string cmd)
        {
            string[] command = cmd.Split(" ");

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

        }
        #endregion
    }
}
