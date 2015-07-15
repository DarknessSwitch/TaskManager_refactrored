using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.DataAccess.Infrastructure;
using TaskManager.Models;
using TaskManager.Services.Interfaces;

namespace TaskManager.Services.Concrete
{
    public class SubtaskService : ISubtaskService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public SubtaskService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
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

        public bool AreSubtasksDone(Task task)
        {
            return task.Subtasks.Count() == task.Subtasks.Count(st => st.IsDone);
        }
    }
}