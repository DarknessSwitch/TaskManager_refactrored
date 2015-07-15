using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.DataAccess.Entities;
using TaskManager.DataAccess.Infrastructure;
using TaskManager.Services.Interfaces;

namespace TaskManager.Services.Concrete
{
    public class SubtaskService : ISubtaskService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public SubtaskService()
        {
            _unitOfWorkFactory = new UnitOfWorkFactory();
        }

        public void CheckSubtasks(int taskId)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var task = unitOfWork.Repository<Task>().GetOne(x => x.Id == taskId);
                if (AreSubtasksDone(task))
                {
                    task.IsDone = true;
                }
                else
                {
                    task.IsDone = false;
                }
                unitOfWork.SaveChanges();
            }
        }

        private bool AreSubtasksDone(Task task)
        {
            return task.Subtasks.Count() == task.Subtasks.Count(st => st.IsDone);
        }
    }
}