using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
   public class Campaing : CoreEntity
    {//tamam
        public Guid ProductID { get; set; }
        public float? Discount { get; set; }
        public string Description { get; set; }

        public virtual Product Product { get; set; }
        public virtual List<ProductDetail> ProductDetails { get; set; }


    }
}
