using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Parameters;
using TaskManager.DataAccess.Repositories;

namespace TaskManager.DataAccess.Infrastructure
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public IRepository<T> Create<T>(TaskManagerDbContext context) where T : class, IEntity
        {
            var repository = WebApiApplication.Container.Get<IRepository<T>>(new ConstructorArgument("context", context));
            return repository;
        }
    }
}
