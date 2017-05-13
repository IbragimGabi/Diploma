using System;

namespace Contracts
{
    public interface IMPI_Engine
    {
        TimeSpan? StartMPIProcess(string file, int threadCount);
    }
}
