using Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class AppUser : CoreEntity
    {//tamam
        public string Name { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string EmailAddress { get; set; }
       
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsPageAdmin { get; set; }
        

        public virtual List<Product> Products { get; set; }
        public virtual List<Address> Addresses { get; set; }
        public virtual List<Order> Orders { get; set; }
        public virtual List<ShoppingCart> ShoppingCarts { get; set; }
        public virtual List<WishList> WishLists { get; set; }
        public virtual List<Campaing> Campaings { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<AppUser> AppUsers { get; set; }

    }
}
