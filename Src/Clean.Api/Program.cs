using Clean.Api.Middlewares;
using Clean.Application.UseCases;
using Clean.Domain.Policies;
using Clean.Infrastructure.Datas;
using Clean.Infrastructure.ExternalServices;
using Clean.Infrastructure.Repositories;
using Clean.Infrastructure.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Description = "Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanArch.Src.Api", Version = "v1" });
    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new [] {"Bearer"} }
                });

});
builder.Services.AddDomainPolicies();
builder.Services.AddInfrastructureDatabase(builder.Configuration);
builder.Services.AddInfrastructureExternalService();
builder.Services.AddInfrastructureRepository(builder.Configuration);
builder.Services.AddInfrastructureUtilities();
builder.Services.AddApplicationUseCase();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<CleanLogMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseResponseCaching();

app.UseStaticFiles();
app.MapControllers();

app.Run();
