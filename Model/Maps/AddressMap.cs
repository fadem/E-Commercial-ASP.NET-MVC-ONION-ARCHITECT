using Core.Map;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Maps
{
   public class AddressMap : CoreMap<Address>
    {
        public AddressMap()
        {
            ToTable("Addresses");
            Property(m => m.AppUserID).IsOptional();
            Property(m => m.ProvinceID).IsOptional();
            Property(m => m.RegionToID).IsOptional();
            Property(m => m.DistrictID).IsOptional();
            Property(m => m.RoadID).IsOptional();
            Property(m => m.StreetID).IsOptional();
            Property(m => m.BuildID).IsOptional();


        }
    }
}
