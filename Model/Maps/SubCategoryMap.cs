using Core.Map;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Maps
{
   public class SubCategoryMap : CoreMap<SubCategory>
    {
        public SubCategoryMap()
        {
            ToTable("dbo.SubCategories");
            Property(m => m.SubCategoryName).HasMaxLength(20).IsRequired();
            Property(m => m.Description).IsOptional();
            Property(m => m.ImagePath).HasMaxLength(60).IsOptional();
            Property(m => m.CategoryID).IsOptional();

        }
    }
}
