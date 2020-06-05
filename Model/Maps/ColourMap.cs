using Core.Map;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Maps
{
   public class ColourMap : CoreMap<Colour>
    {
        public ColourMap()
        {
            ToTable("dbo.Colors");
            Property(m => m.ProductColour).IsRequired();
            
        }
    }
}
