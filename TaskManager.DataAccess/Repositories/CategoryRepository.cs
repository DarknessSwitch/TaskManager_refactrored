using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataAccess.Infrastructure;
using TaskManager.Models;

namespace TaskManager.DataAccess.Repositories
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(DbContext context):base(context)
        {
            
        }
    }
}
