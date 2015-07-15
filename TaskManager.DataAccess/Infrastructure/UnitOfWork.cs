using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DataAccess.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskManagerDbContext _context;
        private readonly IRepositoryFactory _repositoryFactory;

//        public UnitOfWork(DbContext context, IRepositoryFactory repositoryFactory)
//        {
//            _context = context;
//            _repositoryFactory = repositoryFactory;
//        }
        public UnitOfWork()
        {
            _context = new TaskManagerDbContext();
            _repositoryFactory = new RepositoryFactory();
        }
        public IRepository<T> Repository<T>() where T : class, IEntity
        {
            return _repositoryFactory.Create<T>(_context);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
