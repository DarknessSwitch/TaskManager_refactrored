using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.DataAccess.Entities;

namespace TaskManager.Services.Interfaces
{
    interface ISubtaskService
    {
        void CheckSubtasks(int taskId);
    }
}
