using Core.Map;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Maps
{
   public class CustomerNonMemberMap : CoreMap<CustomerNonMember>
    {
        public CustomerNonMemberMap()
        {
            ToTable("dbo.Customers");
            Property(m => m.Name).HasMaxLength(20).IsOptional();
            Property(m => m.SurName).HasMaxLength(20).IsOptional();
            Property(m => m.Phone).HasMaxLength(20).IsOptional();
            Property(m => m.EmailAddress).HasMaxLength(20).IsOptional();

        }
    }
}
