using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Category = TaskManager.DataAccess.Entities.Category;
using Subtask = TaskManager.DataAccess.Entities.Subtask;
using Task = TaskManager.DataAccess.Entities.Task;

namespace TaskManager.DataAccess.Infrastructure
{
    public class TaskManagerDbContext : DbContext
    {
        public TaskManagerDbContext() : base("TaskManagerDb")
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Subtask> Subtasks { get; set; }
    }
}
