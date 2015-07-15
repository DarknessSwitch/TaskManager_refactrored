using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TaskManager.DataAccess.Infrastructure;
using Task = TaskManager.DataAccess.Entities.Task;

namespace TaskManager.DataAccess.Repositories
{
    public class TaskRepository : BaseRepository<Task>
    {
        public TaskRepository(TaskManagerDbContext context) : base(context)
        {
        }
    }
}
