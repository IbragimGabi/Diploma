using System;
using System.Collections.Generic;

namespace Contracts
{
    public class TaskModel
    {
        string TaskName { get; set; }
        List<string> Files { get; set; }
    }
}
