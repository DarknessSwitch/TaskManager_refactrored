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
        private readonly DbContext _context;
        private readonly IRepositoryFactory _repositoryFactory;

        public UnitOfWork(DbContext context, IRepositoryFactory repositoryFactory)
        {
            _context = context;
            _repositoryFactory = repositoryFactory;
        }
        public IRepository<T> Repository<T>() where T : class, IEntity
        {
            return _repositoryFactory.Resolve<T>(_context);
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
