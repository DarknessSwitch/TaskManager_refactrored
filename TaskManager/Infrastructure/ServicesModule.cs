using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using TaskManager.Services.Concrete;
using TaskManager.Services.Interfaces;

namespace TaskManager.Infrastructure
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICategoryService>().To<CategoryService>();
            Bind<ITaskService>().To<TaskService>();
            Bind<ISubtaskService>().To<SubtaskService>();
        }
    }
}