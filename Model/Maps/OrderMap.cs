using Core.Map;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Maps
{
   public class OrderMap : CoreMap<Order>
    {
        public OrderMap()
        {
            ToTable("dbo.Orders");
            Property(m => m.OrderDate).HasColumnType("date").IsOptional();
            Property(m => m.RequiredDate).HasColumnType("date").IsOptional();
            Property(m => m.ShippedDate).HasColumnType("date").IsOptional();
            Property(m => m.Freight).HasColumnType("money").IsOptional();
            Property(m => m.Name).HasMaxLength(50).IsRequired();
            Property(m => m.SurName).HasMaxLength(50).IsRequired();
            Property(m => m.Phone).HasMaxLength(15).IsRequired();
            Property(m => m.EmailAddress).HasMaxLength(50).IsRequired();
            Property(m => m.ShipAddress).IsOptional();
            Property(m => m.ProvinceID).IsOptional();
            Property(m => m.RegionToID).IsOptional();
            Property(m => m.DistrictID).IsOptional();
            Property(m => m.RoadID).IsOptional();
            Property(m => m.BuildID).IsOptional();
            Property(m => m.AppUserID).IsOptional();





        }
    }
}
