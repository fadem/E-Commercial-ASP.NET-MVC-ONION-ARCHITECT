using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
   public class Build : CoreEntity
    {//Tamam
       
        public Guid StreetID { get; set; }
        public string Name { get; set; }


        public virtual Street Street { get; set; }

    }
}

