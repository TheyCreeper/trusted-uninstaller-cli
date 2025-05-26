using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas_Wizard.Utils
{
    public class CommandPromptHelper
    { 
       /// <summary>
       /// Runs any given command in parameters
       /// </summary>
       /// <param name="command">command</param>
       /// <param name="noWindow">True by default</param>
        public static void RunCommand(string command, bool noWindow = true, bool waitForExit = true)
        {
            Process commandPrompt = new Process();
            commandPrompt.StartInfo.FileName = "cmd.exe";
            commandPrompt.StartInfo.Arguments = $"/c {command}";
            commandPrompt.StartInfo.CreateNoWindow = noWindow;
            commandPrompt.StartInfo.UseShellExecute = false;

            commandPrompt.Start();
            if (waitForExit) commandPrompt.WaitForExit();
        }
    }
}
