using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AppDomainProject.Models
{
    public class AppDomainProjectContext : DbContext
    {
        public AppDomainProjectContext (DbContextOptions<AppDomainProjectContext> options)
            : base(options)
        {
        }

        public DbSet<LoginData> LoginData { get; set; }
    }
}
