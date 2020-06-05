using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
   public class RegionTo : CoreEntity
    {//tamam
        public Guid ProvinceID { get; set; }
        public string Name { get; set; }

        public virtual Province Province { get; set; }
        public virtual List<District> Districs { get; set; }


    }
}
