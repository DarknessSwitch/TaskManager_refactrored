using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DataAccess.Infrastructure
{
    public interface IRepository<T> where T : IEntity
    {
        T Get(int id);
        T GetOne(Func<T, Boolean> predicate);
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(Func<T, bool> predicate);
        void AddItem(T item);
        void AttachItem(T item);
        void DeleteItem(T item);
    }
}
