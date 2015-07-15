using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.DataAccess.Infrastructure;

namespace TaskManager.Models
{
    public class Subtask : IEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
        public virtual int TaskId { get; set; }
        public virtual Task Task { get; set; }
    }
}