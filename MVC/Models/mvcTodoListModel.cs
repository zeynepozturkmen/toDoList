using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class mvcTodoListModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public System.DateTime Date { get; set; }
    }
}