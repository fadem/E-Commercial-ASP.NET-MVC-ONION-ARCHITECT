using Core.Map;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Maps
{
  public  class CommentMap : CoreMap<Comment>
    {
        public CommentMap()
        {
            ToTable("dbo.Comments");
            Property(m => m.MemberComment).HasMaxLength(500).IsRequired();
            Property(m => m.AppUserID).IsOptional();
            Property(m => m.ProductID).IsOptional();
        }
    }
}
