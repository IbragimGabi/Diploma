using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Contracts
{
    public interface IProcess_Manager
    {
        bool IsRunningProcess(List<string> files);
        void KillProcess(List<string> files);
    }
}
