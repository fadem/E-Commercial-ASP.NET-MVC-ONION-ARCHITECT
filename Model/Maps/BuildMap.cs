using Core.Map;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Maps
{
   public class BuildMap : CoreMap<Build>
    {
        public BuildMap()
        {
            ToTable("dbo.Builds");
            Property(m => m.Name).HasMaxLength(50).IsOptional();
        }
    }
}
