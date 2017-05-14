using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Contracts;
using System.Linq;

namespace Service
{
    public class Process_Engine : IProcess_Engine
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

        public void KillProcess(List<Process> files)
        {
            files.ForEach(_ => _.Kill());
        }
    }
}