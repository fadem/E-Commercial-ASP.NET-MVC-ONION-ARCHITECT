using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class OrderDetail : CoreEntity
    {
        public Guid? AppUserID { get; set; }
        public Guid? ProductID { get; set; }
        public Guid? OrderID { get; set; }
        public decimal? Price { get; set; }
        public short? Quantity { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
