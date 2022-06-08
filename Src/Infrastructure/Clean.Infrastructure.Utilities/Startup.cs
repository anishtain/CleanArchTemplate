using Clean.Domain.Contracts.Infrastructures.Utilities;
using Clean.Infrastructure.Utilities.FileUtilities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infrastructure.Utilities
{
    public static class Startup
    {
        public static IServiceCollection AddInfrastructureUtilities(this IServiceCollection services)
        {
            services.AddScoped<IFileUtility, FileUtility>();
            services.AddScoped<IFileUpload, ImageFileUtility>();
            services.AddScoped<IFileUpload, VideoFileUtility>();
            return services;
        }
    }
}
