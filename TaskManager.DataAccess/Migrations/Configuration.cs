
using Category = TaskManager.DataAccess.Entities.Category;
using Subtask = TaskManager.DataAccess.Entities.Subtask;
using Task = TaskManager.DataAccess.Entities.Task;

namespace TaskManager.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TaskManager.DataAccess.Infrastructure.TaskManagerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TaskManager.DataAccess.Infrastructure.TaskManagerDbContext context)
        {
            context.Categories.AddOrUpdate(x => x.Id,
                new Category { Id = 1, Text = "Studying" },
                new Category { Id = 2, Text = "Housekeeping" });
            context.Tasks.AddOrUpdate(x => x.Id,
                new Task
                {
                    Id = 1,
                    CategoryId = 1,
                    Date = new DateTime(2015, 06, 02),
                    IsDone = false,
                    Text = "Make a task manager application",
                },
                    new Task
                    {
                        Id = 2,
                        CategoryId = 2,
                        Date = new DateTime(2015, 06, 03),
                        IsDone = false,
                        Text = "Cook dessert for tomorrow dinner",
                    });
            context.Subtasks.AddOrUpdate(x => x.Id,
                new Subtask { Id = 1, Text = "Read tutorials", IsDone = false, TaskId = 1 },
                new Subtask { Id = 2, Text = "Write code", IsDone = false, TaskId = 1 },
                new Subtask { Id = 3, Text = "Peel potatoes", IsDone = false, TaskId = 2 },
                new Subtask { Id = 4, Text = "Boil popatoes", IsDone = false, TaskId = 2 });       
        }
    }
}
