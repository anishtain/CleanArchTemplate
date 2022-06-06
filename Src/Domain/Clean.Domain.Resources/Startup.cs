using Clean.Domain.Contracts.Domains;
using Clean.Domain.Resources.Permissions.Commons;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Domain.Resources
{
    public static class Startup
    {
        public static IServiceCollection AddDomainResource(this IServiceCollection services)
        {
            services.AddSingleton<IPermission, AllPermissions>();

            return services;
        }
    }
}
