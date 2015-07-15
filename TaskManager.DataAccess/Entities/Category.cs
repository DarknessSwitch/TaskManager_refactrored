using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaskManager.DataAccess.Infrastructure;

namespace TaskManager.DataAccess.Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}