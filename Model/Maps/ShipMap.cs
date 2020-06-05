using Core.Map;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Maps
{
   public class ShipMap : CoreMap<Ship>
    {
        public ShipMap()
        {
            ToTable("dbo.Shippers");
            Property(m => m.CompanyName).HasMaxLength(40).IsRequired();
            Property(m => m.Phone).HasMaxLength(15).IsOptional();
        }
    }
}
