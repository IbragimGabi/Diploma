using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Helpers
{
    public class ProcessHelper
    {
        public List<Process> FindProcess(List<string> files)
        {
            var exeFile = files.Find(_ => _.Contains("(EXECUTE)"));
            int index = exeFile.IndexOf("(EXECUTE)");
            var process = exeFile.Substring(index);
            process = process.Replace(".exe", "");

            return Process.GetProcesses().
                Where(_ => _.ProcessName.Contains(process)).
                ToList();
        }
    }
}
