using Core.Map;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Maps
{
   public class WishListMap : CoreMap<WishList>
    {
        public WishListMap()
        {
            ToTable("dbo.WishLists");
            Property(m => m.ProductName).HasMaxLength(50).IsOptional();
            Property(m => m.Price).HasColumnType("money").IsOptional();
            Property(m => m.ImagePath).HasMaxLength(100).IsOptional();
            Property(m => m.AppUserID).IsOptional();
            Property(m => m.ProductID).IsOptional();

        }
    }
}
