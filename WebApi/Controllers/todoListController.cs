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
using WebApi.Models.Orm;

namespace WebApi.Controllers
{
    public class todoListController : ApiController
    {
        private todoListDBEntities db = new todoListDBEntities();

        // GET: api/todoList
        public IQueryable<todoList> GettodoList()
        {
            return db.todoList;
        }

        //public IQueryable<todoList> GettodoListDateNow()
        //{
        //    return db.todoList.Where(x => x.Date == DateTime.Today);
        //}

        // GET: api/todoList/5
        [ResponseType(typeof(todoList))]
        public IHttpActionResult GettodoList(int id)
        {
            todoList todoList = db.todoList.Find(id);
            if (todoList == null)
            {
                return NotFound();
            }

            return Ok(todoList);
        }

        // PUT: api/todoList/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttodoList(int id, todoList todoList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != todoList.Id)
            {
                return BadRequest();
            }

            db.Entry(todoList).State = EntityState.Modified;

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
        public IHttpActionResult PosttodoList(todoList todoList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.todoList.Add(todoList);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = todoList.Id }, todoList);
        }

        // DELETE: api/todoList/5
        [ResponseType(typeof(todoList))]
        public IHttpActionResult DeletetodoList(int id)
        {
            todoList todoList = db.todoList.Find(id);
            if (todoList == null)
            {
                return NotFound();
            }

            db.todoList.Remove(todoList);
            db.SaveChanges();

            return Ok(todoList);
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