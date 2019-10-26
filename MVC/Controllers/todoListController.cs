using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class todoListController : Controller
    {
        // GET: todoList
        public ActionResult Index()
        {
            IEnumerable<mvcTodoListModel> list;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("todoList").Result;
            list = response.Content.ReadAsAsync<IEnumerable<mvcTodoListModel>>().Result;
            return View(list);
        }

        //public ActionResult GetDateToday()
        //{
        //    IEnumerable<mvcTodoListModel> list;
        //    HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("todoList").Result;
        //    list = response.Content.ReadAsAsync<IEnumerable<mvcTodoListModel>>().Result;
        //    return RedirectToAction("Index",list);
        //}


        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcTodoListModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("todoList/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcTodoListModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(mvcTodoListModel model)
        {
            if (model.Id == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("todoList", model).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("todoList/" + model.Id, model).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("todoList/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }

    }
}