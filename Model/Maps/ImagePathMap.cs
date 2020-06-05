using Core.Map;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Maps
{
   public class ImagePathMap : CoreMap<ImagePath>
    {
        public ImagePathMap()
        {
            ToTable("dbo.ImagePaths");
            Property(m => m.ProductImage).HasMaxLength(60).IsOptional();
            Property(m => m.ProductID).IsOptional();
        }
    }
}
