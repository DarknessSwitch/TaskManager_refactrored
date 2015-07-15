using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManager.DataAccess.Repositories;
using TaskManager.DataAccess.Infrastructure;
using TaskManager.Models;
using TaskManager.Services.Concrete;
using TaskManager.Services.Interfaces;

namespace TaskManager.Controllers
{
    public class CategoryController : ApiController
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public CategoryController(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public CategoryController()
        {
            
        }

        // GET api/category
        public IEnumerable<Category> Get()
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                return unitOfWork.Repository<Category>().GetAll();
            }
        }

        // GET api/category/5
        public Category Get(int id)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                return unitOfWork.Repository<Category>().GetOne(x => x.Id == id);
            }
        }

        // POST api/category
        public HttpResponseMessage Post(Category category)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
                {
                     unitOfWork.Repository<Category>().AddItem(category);
                }

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, category);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = category.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // PUT api/category/5
        public HttpResponseMessage Put(int id, Category category)
        {
            using (var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                if (ModelState.IsValid && id == category.Id)
                {
                    unitOfWork.Repository<Category>().AttachItem(category);

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

        // DELETE api/category/5
        public HttpResponseMessage Delete(int id)
        {
            using(var unitOfWork = _unitOfWorkFactory.CreateUnitOfWork())
            {
                Category category = unitOfWork.Repository<Category>().GetOne(x=>x.Id==id);
                if (category == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                unitOfWork.Repository<Category>().DeleteItem(category);

                try
                {
                    unitOfWork.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                return Request.CreateResponse(HttpStatusCode.OK, category);
            } 
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
