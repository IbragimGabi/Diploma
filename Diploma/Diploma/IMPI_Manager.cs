using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    interface IMPI_Manager
    {
        TimeSpan? StartMPIProcess(string file, int threadCount);
        bool IsRunning(string file);
    }
}
