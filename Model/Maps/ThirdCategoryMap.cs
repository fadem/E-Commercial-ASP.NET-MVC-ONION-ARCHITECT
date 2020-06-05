using Core.Map;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Maps
{
   public class ThirdCategoryMap : CoreMap<ThirdCategory>
    {
        public ThirdCategoryMap()
        {
            ToTable("dbo.ThirdCategories");
            Property(m => m.ThirdCategoryName).HasMaxLength(50).IsRequired();
            Property(m => m.Description).IsOptional();
            Property(m => m.ImagePath).HasMaxLength(60).IsOptional();
            Property(m => m.SubCategoryID).IsOptional();

        }
    }
}
