using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class UrunSepeti
    {
        public Guid ID { get; set; }
        public Guid ProductDetailID { get; set; }
        public string ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
        public short? Quantity { get; set; }
        public string PicImage { get; set; }
        public string ProductColor { get; set; }
        public string ProductSize { get; set; }

        public virtual List<ProductDetail> ProductDetails { get; set; }
    }

}