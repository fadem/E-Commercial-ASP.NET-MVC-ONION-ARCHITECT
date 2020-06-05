using Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{//tamam
   public class Colour: CoreEntity
    {

        public string ProductColour { get; set; }


        public virtual List<ProductDetail> ProductDetails { get; set; }


    }
}
