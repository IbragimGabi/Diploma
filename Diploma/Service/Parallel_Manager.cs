using Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Parallel_Manager : IParallel_Manager
    {
        private IParallel_Engine _MPIEngine { get; set; }
        public Parallel_Manager()
        {
            _MPIEngine = new MPI_Engine();
        }

        public void StartParallelProcess(string file, int threadCount)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            _MPIEngine.StartParallelProcess(file, threadCount);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
        }
    }
}
