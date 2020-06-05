using Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class SubCategory : CoreEntity
    {//tamam
        public Guid CategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public virtual Category Category { get; set; }
        public virtual List<ThirdCategory> ThirdCategories { get; set; }
        public virtual List<Product> Products { get; set; }

    }
}
