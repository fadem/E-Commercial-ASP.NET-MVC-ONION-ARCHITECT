using Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Map
{
   public class CoreMap<T> : EntityTypeConfiguration<T> where T : CoreEntity
    {
        public CoreMap()
        {
            Property(m => m.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(m => m.CreatedDate).IsOptional();
            Property(m => m.CreatedIP).IsOptional();
            Property(m => m.ModifiedDate).IsOptional();
            Property(m => m.ModifiedIP).IsOptional();
            Property(m => m.Status).IsOptional();



        }
    }
}
