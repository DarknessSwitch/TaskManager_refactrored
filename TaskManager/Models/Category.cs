using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.DataAccess.Infrastructure;

namespace TaskManager.Models
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}