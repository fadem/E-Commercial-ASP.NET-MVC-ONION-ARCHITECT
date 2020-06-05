using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
   public class Province : CoreEntity
    {//Tamam
        public string Name { get; set; }

        public virtual List<RegionTo> Regions { get; set; }
        public virtual List<Order> Orders { get; set; }

    }
}
