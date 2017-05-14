using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Contracts
{
    public interface IProcess_Engine
    {
        List<Process> FindProcess(List<string> files);
        void KillProcess(List<Process> files);
    }
}
