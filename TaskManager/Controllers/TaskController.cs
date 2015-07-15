using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManager.DataAccess.Entities;
using TaskManager.DataAccess.Infrastructure;
using TaskManager.Services.Concrete;
using TaskManager.Services.Interfaces;

namespace TaskManager.Controllers
{
    [RoutePrefix("api/task")]
    public class TaskController : ApiController
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        public TaskController()
        {
            _unitOfWorkFactory = new UnitOfWorkFactory();
        }

        // GET api/Task
        [HttpGet]
        public IEnumerable<Task> Get()
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                return unitOfWork.Repository<Task>().GetAll().AsEnumerable();
            }
        }

        // GET api/Task/5
        [HttpGet]
        [Route("{id:int}")]
        public Task Get(int id)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var task = unitOfWork.Repository<Task>().GetOne(x => x.Id == id);

                if (task == null)
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return task;
            }               
        }

        // POST api/Task
        [HttpPost]
        [Route("post", Name = "PostTask")]
        public HttpResponseMessage Post(Task task)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
                {
                     unitOfWork.Repository<Task>().AddItem(task);
                }
                
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, task);
//                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = task.Id }));
                response.Headers.Location = new Uri(Url.Link("PostTask", new { id = task.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // PUT api/Task/5
        [HttpPut]
        [Route("put/{id:int}")]
        public HttpResponseMessage Put(int id, Task task)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                if (ModelState.IsValid && id == task.Id)
                {
                    unitOfWork.Repository<Task>().AttachItem(task);

                    try
                    {
                        unitOfWork.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                    }

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
        }

        // DELETE api/Task/5
        [HttpDelete]
        [Route("delete/{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            using(var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                Task task = unitOfWork.Repository<Task>().GetOne(x=>x.Id==id);
                if (task == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                unitOfWork.Repository<Task>().DeleteItem(task);

                try
                {
                    unitOfWork.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, task);
            } 
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
