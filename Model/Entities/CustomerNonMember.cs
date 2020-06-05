using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
   public class CustomerNonMember : CoreEntity
    {//tamam
        // bu tablo verileri sipariş işlemi gerçekleştiğinde otomatik olarak girilecek.
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }

    }
}
