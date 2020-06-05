using Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
   public class Comment : CoreEntity
    {// Tamam
        public Guid AppUserID { get; set; }
        public Guid ProductID { get; set; }
        public string MemberComment { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual Product Product { get; set; }


    }
}
