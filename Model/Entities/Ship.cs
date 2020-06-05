using Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Ship : CoreEntity
    {//tamam
        public string CompanyName { get; set; }
     
        public string Phone { get; set; }

        public virtual List<Order> Orders { get; set; }

    }
}
