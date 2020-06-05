using Core.Map;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Maps
{
   public class DistrictMap : CoreMap<District>
    {
        public DistrictMap()
        {
            ToTable("dbo.Districs");
            Property(m => m.Name).HasMaxLength(20).IsRequired();
            Property(m => m.PostalCode).IsOptional();
        }
    }
}
