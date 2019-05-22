using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MySql.Data.Entity;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace Trident.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MemberContext : DbContext
    {
        //Constructor
        public MemberContext() : base("Server=192.185.6.31; Database=lilxprince_trident; Uid=lilxp_jing; Pwd=h70bnrw8wvc")
        {

        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Character> Characters { get; set; }

        public DbSet<Team> Teams { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //https://stackoverflow.com/questions/3600175/the-model-backing-the-database-context-has-changed-since-the-database-was-crea
            Database.SetInitializer<MemberContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}