using Core.Map;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Maps
{
  public  class MessageMap : CoreMap<Message>
    {
        public MessageMap()
        {
            ToTable("Messages");
            Property(m => m.AppUserID).IsOptional();
            Property(m => m.EmailAddress).HasColumnType("nvarchar").HasMaxLength(100).IsOptional();
            Property(m => m.MemberMessage).HasColumnType("nvarchar").HasMaxLength(1000).IsOptional();
        }
    }
}
