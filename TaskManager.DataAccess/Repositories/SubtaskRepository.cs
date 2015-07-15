using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataAccess.Infrastructure;
using Subtask = TaskManager.DataAccess.Entities.Subtask;

namespace TaskManager.DataAccess.Repositories
{
    public class SubtaskRepository : BaseRepository<Subtask>
    {
        public SubtaskRepository(TaskManagerDbContext context) : base(context)
        {
        }
    }
}
