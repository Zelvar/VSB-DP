using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using StaticAnalysisProject.Modules;

namespace StaticAnalysisProject.Console
{
    internal static class CmdHandler
    {
        #region DATA
        private static IList<MethodInfo> _listCommands = new List<MethodInfo>();

        private static string _getHelpInfo(string cmd)
        {
            string ret = "";
            switch (cmd)
            {
                case "strings(2)": //Strings options
                    ret = "filePath (type[all|urls|files|ips])"; 
                    break;
                case "hashes(2)":
                    ret = "filePath (type[md5|sha1|sha384|sha256|sha512])";
                    break;
                default:
                    break;
            }

            return ret;
        }

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
            //https://stackoverflow.com/questions/14655023/split-a-string-that-has-white-spaces-unless-they-are-enclosed-within-quotes
            string[] command = Regex.Matches(cmd.Trim(), @"[\""].+?[\""]|[^ ]+")
                .Cast<Match>()
                .Select(m => m.Value.Replace("\"", ""))
                .ToArray();

            if (commands.Length == 0)
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
            MethodInfo cmd = _listCommands
                .Where(x => x.Name.ToLower().Equals(string.Format("{0}{1}", "cmd", command.First())))
                .Where(x => x.GetParameters().Length == command.Length - 1)
                .Select(x => x)
                .FirstOrDefault();

            //IF NULL?!
            if (cmd == null)
                System.Console.WriteLine("Error! Wrong number of parameters.");
            else if (cmd.GetParameters().Length == command.Length - 1)
            {
                IList<object> obj = new List<object>();
                foreach (var str in command.Skip(1).ToArray())   //Prepare parameters
                {
                    obj.Add(str);
                }

                try {
                    cmd.Invoke(null, obj.ToArray());    //Call method
                } 
                catch( StaticAnalysisProjectException e )
                {
                    System.Console.WriteLine(e.ToString());
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("Error! Unknown exception.");
                    Debug.WriteLine(e.ToString());
                }
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
                string output = _getHelpInfo(string.Format("{0}({1})", method.Name.ConvertMethodName(), method.GetParameters().Length));

                if (output == "") {
                    System.Console.WriteLine("> {0} {1}",
                            method.Name.ConvertMethodName(),
                            string.Join(" ", method.GetParameters().Select(x => x.Name))
                        );
                } else {
                    System.Console.WriteLine("> {0} {1}",
                            method.Name.ConvertMethodName(),
                            output
                        );
                }
            }
        }

        /// <summary>
        /// Extract strings and check for known parameters of strings
        /// </summary>
        public static void CmdStrings(string filePath)
        {
            System.Console.WriteLine(new Strings(filePath).ToString());
        }

        public static void CmdStrings(string filePath, string type)
        {
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

        /// <summary>
        /// Get hashes
        /// </summary>
        public static void CmdHashes(string filePath)
        {
            System.Console.WriteLine(new Hashes(filePath).ToString());
        }

        /// <summary>
        /// Get hashes
        /// </summary>
        public static void CmdHashes(string filePath, string type)
        {
            System.Console.WriteLine(new Hashes(filePath).ToString(type));
        }

        /// <summary>
        /// Get full report
        /// </summary>
        public static void CmdFullReport(string filePath)
        {
            System.Console.WriteLine(new FileReport(filePath).ToString());
        }

        public static void CmdBehavior(string filePath)
        {
            System.Console.WriteLine(new DetectWithYara(filePath).ToString());
        }
        #endregion
    }
}
