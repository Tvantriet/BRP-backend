using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data.Entities;

namespace Data
{
    public class DefaultContext : DbContext
    {
        public DbSet<Route> Routes { get; set; }
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }
    }
}
