using Core.Map;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Maps
{
   public class OrderDetailMap : CoreMap<OrderDetail>
    {
        public OrderDetailMap()
        {
            ToTable("OrderDetails");
            Property(m => m.Price).HasColumnType("money").IsOptional();
            Property(m => m.ProductID).IsOptional();
            Property(m => m.OrderID).IsOptional();
            Property(m => m.AppUserID).IsOptional();

        }
    }
}
