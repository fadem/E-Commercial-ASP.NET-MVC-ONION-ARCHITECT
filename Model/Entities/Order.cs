using Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Order : CoreEntity
    {
        public Guid? AppUserID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public Guid? ShipID { get; set; }
        public decimal? Freight { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
  public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public string ShipAddress { get; set; }
        public Guid? ProvinceID { get; set; }
        public Guid? RegionToID { get; set; }
        public Guid? DistrictID { get; set; }
        public Guid? RoadID { get; set; }
        public Guid? StreetID { get; set; }
        public Guid? BuildID { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual Ship Ship { get; set; }
        public virtual Province Province { get; set; }
        public virtual RegionTo RegionTo { get; set; }
        public virtual District District { get; set; }
        public virtual Road Road { get; set; }
        public virtual Street Street { get; set; }
        public virtual Build Build { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }

    }
}
