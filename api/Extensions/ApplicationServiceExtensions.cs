using System.Text;
using api.Data;
using api.Interface;
using api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace api.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // Interface Services
            services.AddScoped<ITokenService,TokenService>();

            // Add DbContext Service/ConnectionString
            services.AddDbContext<DataContext> (options => {
                options.UseSqlite (config.GetConnectionString("DefaultConnection"));
            });
        

            return services;
        }
    }
}