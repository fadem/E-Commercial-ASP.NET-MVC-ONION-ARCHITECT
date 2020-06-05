using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
   public class ShoppingCart : CoreEntity
    {//tamam
        public Guid AppUserID { get; set; }
        public Guid ProductID { get; set; }
        public Guid? ProductDetailID { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public short? Quantity { get; set; }
        public string PicImage { get; set; }
        public string ProductColour { get; set; }
        public string ProductSize { get; set; }


        public virtual AppUser AppUser { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductDetail ProductDetail { get; set; }


    }
}
