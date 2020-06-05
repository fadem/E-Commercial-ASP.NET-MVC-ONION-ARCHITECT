using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
   public class Road : CoreEntity
    {//tamam
        
        public Guid DistrictID { get; set; }
        public string Name { get; set; }


        public virtual District District { get; set; }

        public virtual List<Street> Streets { get; set; }

    }
}
