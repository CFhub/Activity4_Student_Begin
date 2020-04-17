using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Activity4.Entities;
using System.Data.Entity;

namespace Activity4.DAL
{
    internal class Activity4Context : DbContext
    {
        public Activity4Context() : base("ActivityDB")
        {

        }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
