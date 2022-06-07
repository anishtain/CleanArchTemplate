using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Application.UseCases
{
    public static class Startup
    {
        public static IServiceCollection AddApplicationUseCase(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining(typeof(Startup)));

            services.AddMediatR(typeof(Startup).Assembly);

            return services;
        }
    }
}
