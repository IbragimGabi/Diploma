using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Contracts;
using System.Linq;

namespace Service
{
    public class Process_Manager : IProcess_Manager
    {
        private IProcess_Engine _processEngine { get; set; }
        public Process_Manager()
        {
            _processEngine = new Process_Engine();
        }

        public bool IsRunningProcess(List<string> files)
        {
            return (_processEngine.FindProcess(files).Count > 0) ? true : false;
        }

        public void KillProcess(List<string> files)
        {
            _processEngine.KillProcess(_processEngine.FindProcess(files));
        }
    }
}