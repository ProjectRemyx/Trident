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
    }
}