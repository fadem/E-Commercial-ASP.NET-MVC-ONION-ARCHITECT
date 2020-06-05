using Core.Map;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Maps
{
   public class ShoppingCartMap : CoreMap<ShoppingCart>
    {
        public ShoppingCartMap()
        {
            ToTable("dbo.ShoppingCarts");
            Property(m => m.Name).HasMaxLength(50).IsOptional();
            Property(m => m.Price).HasColumnType("money").IsOptional();
            Property(m => m.Quantity).IsOptional();
            Property(m => m.AppUserID).IsOptional();
            Property(m => m.ProductID).IsOptional();
            Property(m => m.ProductDetailID).IsOptional();

        }
    }
}
