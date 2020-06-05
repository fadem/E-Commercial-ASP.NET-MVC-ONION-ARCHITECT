using Core.Map;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Maps
{
   public class CampaingMap : CoreMap<Campaing>
    {
        public CampaingMap()
        {
            ToTable("dbo.Campaings");
            Property(m => m.Discount).IsOptional();
            Property(m => m.Description).IsOptional();
            Property(m => m.ProductID).IsOptional();
        }
    }
}
