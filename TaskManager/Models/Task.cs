using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.DataAccess.Infrastructure;

namespace TaskManager.Models
{
    public class Task : IEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public bool IsDone { get; set; }
        public virtual ICollection<Subtask> Subtasks { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}