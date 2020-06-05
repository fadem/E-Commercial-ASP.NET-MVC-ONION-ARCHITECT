using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
   public class ImagePath : CoreEntity
    {
        public Guid ProductID { get; set; }
        public string ProductImage { get; set; }

        public virtual Product Product{ get; set; }

    }
}
