using Core.Map;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Maps
{
   public class CategoryMap : CoreMap<Category>
    {
        public CategoryMap()
        {
            ToTable("dbo.Categories");
            Property(m => m.CategoryName).HasMaxLength(20).IsRequired();
            Property(m => m.Description).IsOptional();
            Property(m => m.ImagePath).HasMaxLength(60).IsOptional();

        }
    }
}
