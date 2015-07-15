using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Ninject.Modules;
using Ninject.Extensions.Factory;
using TaskManager.DataAccess.Repositories;
using Category = TaskManager.DataAccess.Entities.Category;
using Subtask = TaskManager.DataAccess.Entities.Subtask;
using Task = TaskManager.DataAccess.Entities.Task;

namespace TaskManager.DataAccess.Infrastructure
{
    public class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepositoryFactory>().To<RepositoryFactory>();
            Bind<IUnitOfWorkFactory>().To<UnitOfWorkFactory>();
            Bind<IRepository<Category>>().To<CategoryRepository>();
            Bind<IRepository<Task>>().To<TaskRepository>();
            Bind<IRepository<Subtask>>().To<SubtaskRepository>();
        }
    }
}
