using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
   public class Message : CoreEntity
    {
        public Guid? AppUserID { get; set; }
        public string MemberMessage { get; set; }
        public string EmailAddress { get; set; }

        public virtual AppUser AppUser { get; set; }
    }
}
