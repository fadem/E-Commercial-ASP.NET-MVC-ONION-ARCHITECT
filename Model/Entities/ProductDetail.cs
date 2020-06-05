using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
   public class ProductDetail : CoreEntity
    {
        public virtual Product Product { get; set; }
        public virtual Campaing Campaing { get; set; }
        public virtual SizeTo SizeTo { get; set; }
        public virtual Colour Colour { get; set; }
        public short? UnitInStock { get; set; }
        public string Description { get; set; }
        public Guid ProductID { get; set; }
        public Guid? SizeToID { get; set; }
        public Guid? ColourID { get; set; }
        public Guid? CampaingID { get; set; }




    }
}
