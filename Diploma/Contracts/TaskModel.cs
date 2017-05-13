using System;
using System.Collections.Generic;

namespace Contracts
{
    public class TaskModel
    {
        public TaskModel()
        {
            Files = new List<string>();
        }
        public string TaskName { get; set; }
        public List<string> Files { get; set; }
    }
}
