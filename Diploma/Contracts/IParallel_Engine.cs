using System;
using System.Diagnostics;

namespace Contracts
{
    public interface IParallel_Engine
    {
        Process StartParallelProcess(string file, int threadCount);
    }
}
