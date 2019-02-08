using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Trident.Models
{
    public class MemberContext : DbContext
    {
        //Empty Constructor
        public MemberContext()
        {

        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Character> Characters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //https://stackoverflow.com/questions/3600175/the-model-backing-the-database-context-has-changed-since-the-database-was-crea
            Database.SetInitializer<MemberContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}