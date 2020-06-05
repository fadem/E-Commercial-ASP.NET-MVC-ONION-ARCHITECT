using Core.Map;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Maps
{
   public class AppUserMap : CoreMap<AppUser>
    {
        public AppUserMap()
        {
            ToTable("dbo.AppUsers");
            Property(m => m.Name).HasMaxLength(100).IsRequired();
            Property(m => m.SurName).HasMaxLength(100).IsRequired();
            Property(m => m.UserName).HasMaxLength(50).IsRequired();
            Property(m => m.Password).HasMaxLength(20).IsRequired();
            Property(m => m.EmailAddress).HasMaxLength(50).IsRequired();
            Property(m => m.Phone).HasMaxLength(15).IsOptional();
            Property(m => m.BirthDate).HasColumnType("date").IsOptional();
            Property(m => m.IsAdministrator).IsOptional();
            Property(m => m.IsPageAdmin).IsOptional();

        }
    }
}
