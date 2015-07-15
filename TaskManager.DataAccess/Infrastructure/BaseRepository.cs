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
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        protected BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public T Get(int id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id);
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
        }
        public void AttachItem(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void DeleteItem(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
