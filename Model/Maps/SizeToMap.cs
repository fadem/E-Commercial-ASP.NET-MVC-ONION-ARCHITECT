﻿using Core.Map;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Maps
{
   public class SizeToMap : CoreMap<SizeTo>
    {
        public SizeToMap()
        {
            ToTable("dbo.Sizes");
            Property(m => m.ProductSize).IsRequired();

        }
    }
}
