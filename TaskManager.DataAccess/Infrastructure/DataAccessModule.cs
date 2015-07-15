using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Ninject.Modules;
using Ninject.Extensions.Factory;
using TaskManager.DataAccess.Repositories;
using TaskManager.Models;

namespace TaskManager.DataAccess.Infrastructure
{
    public class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWorkFactory>().ToFactory();
            Bind<IRepositoryFactory>().ToFactory();
            Bind<IRepository<Category>>().To<CategoryRepository>();
            Bind<IRepository<Task>>().To<TaskRepository>();
            Bind<IRepository<Subtask>>().To<SubtaskRepository>();
        }
    }
}
