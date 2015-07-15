using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DataAccess.Infrastructure
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class , IEntity
    {
        private readonly TaskManagerDbContext _context;
        private readonly DbSet<T> _dbSet;

        protected BaseRepository(TaskManagerDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }


        public T GetOne(Func<T, bool> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return _dbSet.ToList();
        }

        public void AddItem(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }
        public void AttachItem(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void DeleteItem(T entity)
        {
//            _dbSet.Remove(entity);
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
