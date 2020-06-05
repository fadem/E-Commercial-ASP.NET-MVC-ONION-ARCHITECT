﻿using Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
   public class ThirdCategory : CoreEntity
    {//tamam
        public Guid SubCategoryID { get; set; }
        public string ThirdCategoryName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public virtual SubCategory SubCategory { get; set; }
        public virtual List<Product> Products { get; set; }


    }
}
