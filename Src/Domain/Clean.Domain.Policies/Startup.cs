using Clean.Domain.Contracts.Domains.Policies;
using Clean.Domain.Policies.Commons;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Domain.Policies
{
    public static class Startup
    {
        public static IServiceCollection AddDomainPolicies(this IServiceCollection services)
        {
            services.AddScoped<IApprovableEntityPolicies, ApprovableEntityPolicies>();

            services.AddScoped<ISoftDeletableEntityPolicies, SoftDeletableEntityPolicies>();

            return services;
        }
    }
}
