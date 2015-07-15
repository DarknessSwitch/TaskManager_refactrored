using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using TaskManager.DataAccess.Infrastructure;

namespace TaskManager.DataAccess.Entities
{
//    [KnownType(typeof(Subtask))]
    public class Subtask : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public bool IsDone { get; set; }
        public virtual int TaskId { get; set; }
        public virtual Task Task { get; set; }
    }
}