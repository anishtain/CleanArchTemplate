using Clean.Domain.Contracts.Infrastructures.ExternalServices;
using Clean.Infrastructure.ExternalServices.Rest;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infrastructure.ExternalServices
{
    public static class Startup
    {
        public static IServiceCollection AddInfrastructureExternalService(this IServiceCollection services)
        {
            services.AddScoped<IRestServiceClient, RestServiceClient>();

            return services;
        }
    }
}
