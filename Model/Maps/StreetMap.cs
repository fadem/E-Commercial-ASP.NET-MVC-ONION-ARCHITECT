using Core.Map;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Maps
{
   public class StreetMap : CoreMap<Street>
    {
        public StreetMap()
        {
            ToTable("dbo.Streets");
            Property(m => m.Name).HasMaxLength(20).IsRequired();
        }
    }
}
