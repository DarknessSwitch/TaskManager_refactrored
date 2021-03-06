﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DataAccess.Infrastructure
{
    public interface IRepositoryFactory
    {
        IRepository<T> Create<T>(TaskManagerDbContext context) where T : class ,IEntity;
    }
}
