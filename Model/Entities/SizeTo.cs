using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
   public class SizeTo : CoreEntity
    {//tamam

        public string ProductSize { get; set; }

        public virtual List<ProductDetail> ProductDetails { get; set; }


    }
}
