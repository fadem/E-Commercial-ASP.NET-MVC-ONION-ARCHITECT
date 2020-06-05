using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
   public class BrandCategory : CoreEntity
    {
        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
    }
}
