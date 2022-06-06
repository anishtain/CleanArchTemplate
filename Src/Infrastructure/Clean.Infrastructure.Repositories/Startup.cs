using Clean.Domain.Contracts.Infrastructures.Repositories;
using Clean.Infrastructure.Datas.Models;
using Clean.Infrastructure.Repositories.Identities;
using Clean.Infrastructure.Repositories.Repositories.Commons;
using Clean.Infrastructure.Repositories.UnitOfWorks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infrastructure.Repositories
{
    public static class Startup
    {
        public static IServiceCollection AddInfrastructureRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IAuthenticate), typeof(Authenticate));

            services.Configure<Identities.Models.TokenConfig>(cfg => 
            {
                cfg.Audience = configuration["Secret:TokenConfig:Audience"];
                cfg.Secret = configuration["Secret:TokenConfig:Secret"];
                cfg.Issuer = configuration["Secret:TokenConfig:Issuer"];
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services
                .AddIdentity<User, Role>()
                .AddEntityFrameworkStores<Datas.DBContexts.CleanIdentityContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(opt =>
                {
                    opt.SaveToken = true;
                    opt.RequireHttpsMetadata = false;
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = configuration["Security:TokenConfig:Audience"],
                        ValidIssuer = configuration["Security:TokenConfig:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Security:TokenConfig:Secret"]))
                    };

                });

            return services;
        }
    }
}
