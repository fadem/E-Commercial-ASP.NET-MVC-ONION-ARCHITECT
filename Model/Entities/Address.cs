using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
   public class Address : CoreEntity
    {
        public Guid AppUserID { get; set; }
        public Guid? ProvinceID { get; set; }
        public Guid? RegionToID { get; set; }
        public Guid? DistrictID { get; set; }
        public Guid? RoadID { get; set; }
        public Guid? StreetID { get; set; }
        public Guid? BuildID { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual Province Province { get; set; }
        public virtual RegionTo RegionTo { get; set; }
        public virtual District District { get; set; }
        public virtual Road Road { get; set; }
        public virtual Street Street { get; set; }
        public virtual Build Build { get; set; }


    }
}
