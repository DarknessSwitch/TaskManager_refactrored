using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Models;

namespace TaskManager.Services.Interfaces
{
    interface ISubtaskService
    {
        void CheckSubtasks(int taskId);
        bool AreSubtasksDone(Task task);
    }
}
