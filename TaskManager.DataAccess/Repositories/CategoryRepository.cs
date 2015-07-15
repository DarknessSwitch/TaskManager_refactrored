using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataAccess.Infrastructure;
using Category = TaskManager.DataAccess.Entities.Category;

namespace TaskManager.DataAccess.Repositories
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(TaskManagerDbContext context) : base(context)
        {
            
        }
    }
}
