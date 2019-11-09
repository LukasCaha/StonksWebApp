using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stonks.Models.Persons;
using Stonks.Models;

namespace Stonks.Plugins.Database
{
    public class Database : DbContext
    {
        public Database(DbContextOptions<Database> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Stonks.Models.Stock> Stock { get; set; }
    }
}
