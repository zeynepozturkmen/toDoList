using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models.Dto
{
    public class toDoListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }

    }
}