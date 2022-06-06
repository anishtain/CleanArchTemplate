using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infrastructure.Datas
{
    public static class Startup
    {
        public static IServiceCollection AddInfrastructureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DBContexts.CleanIdentityContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Default")));

            return services;
        }
    }
}
