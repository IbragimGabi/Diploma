using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class MPI_Manager : IMPI_Manager
    {
        private IMPI_Engine _MPIEngine { get; set; }
        public MPI_Manager()
        {
            _MPIEngine = new MPI_Engine();
        }

        public TimeSpan? StartMPIProcess(string file, int threadCount)
        {
            return _MPIEngine.StartMPIProcess(file, threadCount);
        }

        public bool IsRunning(string file)
        {
            throw new NotImplementedException();
        }
    }
}
