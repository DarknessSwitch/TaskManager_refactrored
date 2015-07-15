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
    [RoutePrefix("api/subtask")]
    public class SubtaskController : ApiController
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly ISubtaskService _subtaskService;
        public SubtaskController()
        {
            _unitOfWorkFactory = new UnitOfWorkFactory();
            _subtaskService = new SubtaskService();
        }

        // GET api/Subtask
        [HttpGet]
        public IEnumerable<Subtask> Get()
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                return unitOfWork.Repository<Subtask>().GetAll().AsEnumerable();
            }
        }

        // GET api/Subtask/5
        [HttpGet]
        [Route("{id:int}")]
        public Subtask Get(int id)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                var subtask = unitOfWork.Repository<Subtask>().GetOne(x => x.Id == id);

                if (subtask == null)
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }
                return subtask;
            }
        }

        // POST api/Subtask
        [HttpPost]
        [Route("post", Name = "PostSubtask")]
        public HttpResponseMessage Post(Subtask subtask)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
                {
                     unitOfWork.Repository<Subtask>().AddItem(subtask);
                }

                _subtaskService.CheckSubtasks(subtask.TaskId);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, subtask);
//                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = subtask.Id }));
                response.Headers.Location = new Uri(Url.Link("PostSubtask", new { id = subtask.Id }));
                
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // PUT api/Subtask/5
        [HttpPut]
        [Route("put/{id:int}")]
        public HttpResponseMessage Put(int id, Subtask subtask)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                if (ModelState.IsValid && id == subtask.Id)
                {
                    unitOfWork.Repository<Subtask>().AttachItem(subtask);

                    try
                    {
                        unitOfWork.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                    }

                    _subtaskService.CheckSubtasks(subtask.TaskId);

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
        }

        // DELETE api/Subtask/5
        [HttpDelete]
        [Route("delete/{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            using(var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                Subtask subtask = unitOfWork.Repository<Subtask>().GetOne(x=>x.Id==id);
                if (subtask == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                unitOfWork.Repository<Subtask>().DeleteItem(subtask);

                try
                {
                    unitOfWork.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                _subtaskService.CheckSubtasks(subtask.TaskId);

                return Request.CreateResponse(HttpStatusCode.OK, subtask);
            } 
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
