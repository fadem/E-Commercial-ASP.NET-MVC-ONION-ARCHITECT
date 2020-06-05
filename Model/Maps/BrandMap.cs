using Core.Map;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Maps
{
   public class BrandMap : CoreMap<Brand>
    {
        public BrandMap()
        {
            ToTable("dbo.Brands");
            Property(m => m.Name).HasMaxLength(20).IsRequired();
            Property(m => m.Description).IsOptional();
            Property(m => m.ImagePath).HasMaxLength(60).IsOptional();

        }
    }
}
