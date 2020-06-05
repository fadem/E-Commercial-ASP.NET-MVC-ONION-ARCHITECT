using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
   public class Street : CoreEntity
    {//tamam
     
        public Guid RoadID { get; set; }
        public string Name { get; set; }


        public virtual Road Road { get; set; }
        public virtual List<Build> Builds { get; set; }

    }
}
