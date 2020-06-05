using Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Product : CoreEntity
    {
        public Guid AppUserID { get; set; }
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int Point { get; set; }
        public Guid? CategoryID { get; set; }
        public Guid? SubCategoryID { get; set; }
        public Guid? ThirdCategoryID { get; set; }
        public Guid? BrandID { get; set; }
        public string Description { get; set; }
        



        public virtual AppUser AppUser { get; set; }
        public virtual Category Category { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual ThirdCategory ThirdCategory { get; set; }
        public virtual List<ImagePath> ImagePaths { get; set; }
        public virtual List<Campaing> Campaings { get; set; }
        public virtual Brand Brand{ get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
        public virtual List<WishList> WishLists { get; set; }
        public virtual List<ProductDetail> ProductDetails { get; set; }

    }
}
