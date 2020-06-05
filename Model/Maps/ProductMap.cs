using Core.Map;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Maps
{
   public class ProductMap : CoreMap<Product>
    {
        public ProductMap()
        {
            ToTable("dbo.Products");
            Property(m => m.ProductName).HasMaxLength(100).IsRequired();
            Property(m => m.UnitPrice).HasColumnType("money").IsRequired();
            Property(m => m.Point).IsOptional();
            Property(m => m.SubCategoryID).IsOptional();
            Property(m => m.ThirdCategoryID).IsOptional();
            Property(m => m.AppUserID).IsOptional();
            Property(m => m.BrandID).IsOptional();
            Property(m => m.CategoryID).IsOptional();




        }
    }
}
