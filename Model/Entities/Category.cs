﻿using Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Category : CoreEntity
    {//tamam
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public virtual List<Product> Products { get; set; }
        public virtual List<SubCategory> SubCategories { get; set; }
        public virtual List<BrandCategory> BrandCategories { get; set; }


    }
}
