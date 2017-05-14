using System;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IParallel_Manager
    {
        void StartParallelProcess(string file, int threadCount);
    }
}
