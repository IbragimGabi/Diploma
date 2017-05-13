using System;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IMPI_Manager
    {
        void StartMPIProcess(string file, int threadCount);
        bool IsRunning(string file);
    }
}
