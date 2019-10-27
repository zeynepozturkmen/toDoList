using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models.Dto;
using WebApi.Models.Orm;

namespace WebApi.Controllers
{
    public class todoListController : ApiController
    {
        private todoListDBEntities db = new todoListDBEntities();

        // GET: api/todoList
        //public List<toDoListModel> GettodoList()
        //{
        //   List<toDoListModel> list = db.todoList.Select(x=> new toDoListModel {
        //       Id=x.Id,
        //       Name=x.Name,
        //       Date=x.Date,
        //   }).ToList();
        //    return list;
        //}

        //public IQueryable<toDoListModel> GettodoList()
        //{

        //    return db.todoList.Select(x => new toDoListModel
        //    {
        //        Id = x.Id,
        //        Name = x.Name,
        //        Date = x.Date
        //    });
        //}

        public HttpResponseMessage GettodoList(string date="0")
        {
            string s = DateTime.Now.ToString("dd/MM/yyyy");
            switch (date)
            {
                case "0":
                    return Request.CreateResponse(HttpStatusCode.OK, db.todoList.Select(x => new toDoListModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Date = x.Date
                    }).ToList());
           
                case "1":
                    return Request
                        .CreateResponse(HttpStatusCode.OK, db.todoList.Where(b => b.Date == s).Select(x => new toDoListModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Date = x.Date
                        }).ToList());
                   
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Not found");
                   


            }
            

        }


        //[Route("date/{getDateToday:datetime}")]
        //[Route("{date}")]
        //public IHttpActionResult GetDate(string date)
        //{
        //    string s = DateTime.Now.ToString("dd/MM/yyyy");
        //    List<toDoListModel> list = db.todoList.Where(b => b.Date == date).Select(x => new toDoListModel
        //    {
        //        Id = x.Id,
        //        Name = x.Name,
        //        Date = x.Date
        //    }).ToList();

        //    if (list == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(list);
        //}


        //GET: api/todoList/5
        [ResponseType(typeof(todoList))]
        public IHttpActionResult GettodoList(int id)
        {
            toDoListModel findtodoList = db.todoList.Where(x=>x.Id==id).Select(x => new toDoListModel
            {
                Id = x.Id,
                Name = x.Name,
                Date = x.Date
            }).FirstOrDefault();

            if (findtodoList == null)
            {
                return NotFound();
            }

            return Ok(findtodoList);
        }

        // PUT: api/todoList/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttodoList(int id, toDoListModel dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dto.Id)
            {
                return BadRequest();
            }
            todoList newtodoList = new todoList
            {
                Id = dto.Id,
                Name = dto.Name,
                Date = dto.Date,
            };
            db.Entry(newtodoList).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!todoListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/todoList
        [ResponseType(typeof(todoList))]
        public IHttpActionResult PosttodoList(toDoListModel model)
        {
            todoList dto = new todoList {
                //Id = model.Id,
                Name= model.Name,
                Date=model.Date,
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.todoList.Add(dto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dto.Id },db.todoList);
        }

        // DELETE: api/todoList/5
        [ResponseType(typeof(todoList))]
        public IHttpActionResult DeletetodoList(int id)
        {
            todoList findtodoList = db.todoList.Find(id);
            if (findtodoList == null)
            {
                return NotFound();
            }

            db.todoList.Remove(findtodoList);
            db.SaveChanges();

            return Ok(findtodoList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool todoListExists(int id)
        {
            return db.todoList.Count(e => e.Id == id) > 0;
        }
    }
}