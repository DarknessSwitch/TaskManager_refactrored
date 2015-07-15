using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaskManager.DataAccess.Infrastructure;

namespace TaskManager.DataAccess.Entities
{
    public class Task : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public bool IsDone { get; set; }
        public virtual ICollection<Subtask> Subtasks { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}