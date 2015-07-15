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
    public class SubtaskRepository : BaseRepository<Subtask>
    {
        public SubtaskRepository(DbContext context) : base(context)
        {
            
        }
    }
}
