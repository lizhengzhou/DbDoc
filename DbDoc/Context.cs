using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbDoc.Models;
using Microsoft.EntityFrameworkCore;

namespace DbDoc
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) {
            Database.EnsureCreated();
        }
        public DbSet<Db> Dbs { get; set; }
    }
}
