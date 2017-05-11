using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public interface IMPI_Engine
    {
        TimeSpan? StartMPIProcess(string file, int threadCount);
    }
}
