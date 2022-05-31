using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueDataWeb.Core
{
    public class DB_Entities : DbContext
    {
        public DB_Entities() : base("BlueDataWebEntities") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DB_Entities>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}
