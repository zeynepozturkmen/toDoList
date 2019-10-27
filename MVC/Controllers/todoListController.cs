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

        public ActionResult GetDateToday()
        {
            IEnumerable<mvcTodoListModel> list;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("todoList?" + "date=1").Result;
            list = response.Content.ReadAsAsync<IEnumerable<mvcTodoListModel>>().Result;
            return View("Index", list);
        }


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
            string s = model.Date.ToString().Substring(0, 10);
            s = s.Replace('-', '.');

            string[] array = new string[3];
            array[0] = s.Substring(8, 2);
            array[1] = s.Substring(4, 4);
            array[2] = s.Substring(0, 4);
            string sum = array[0].ToString() + array[1].ToString() + array[2].ToString();

            if (model.Id == 0)
            {
                //var x = DateTime.Now.ToString("dd/MM/yyyy"); //27.10.2019
                model.Date =sum;
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("todoList", model).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                model.Date = sum;
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