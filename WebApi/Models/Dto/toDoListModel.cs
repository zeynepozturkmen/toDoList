using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models.Dto
{
    public class toDoListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }

        //public int _id { get { return Id; } set { Id = value; } }
        //public string _name { get { return Name; } set { Name = value; } }
        //public DateTime _date { get { return Date; } set { Date = value; } }
    }
}