using Clean.Infrastructure.Datas.Seeds;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infrastructure.Datas.DBContexts
{
    public class CleanIdentityContext : IdentityDbContext<Models.User, Models.Role, string>
    {
        public CleanIdentityContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(CleanIdentityContext).Assembly);

            builder.Seed();
        }
    }
}
