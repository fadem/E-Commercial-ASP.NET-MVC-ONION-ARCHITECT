using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
   public class District : CoreEntity
    {//Tamam
        public Guid RegionToID { get; set; }
        public string Name { get; set; }
        public int? PostalCode { get; set; }

        public virtual RegionTo RegionTo { get; set; }
        public virtual List<Road> Roads { get; set; }

    }
}
