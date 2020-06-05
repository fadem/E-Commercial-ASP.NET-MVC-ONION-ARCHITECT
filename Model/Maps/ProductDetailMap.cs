using Core.Map;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Maps
{
    public class ProductDetailMap : CoreMap<ProductDetail>
    {
        public ProductDetailMap()
        {
            ToTable("ProductDetails");
            Property(m => m.UnitInStock).IsOptional();
            Property(m => m.Description).IsOptional();
            Property(m => m.ProductID).IsOptional();
            Property(m => m.CampaingID).IsOptional();
            Property(m => m.ColourID).IsOptional();
            Property(m => m.SizeToID).IsOptional();


        }
    }
}
