namespace Backlog.Server.Infrastructure
{
    using Backlog.Server.Data;
    using Backlog.Server.Data.Models;
    using Backlog.Server.Features.Company;
    using Backlog.Server.Features.Identity;
    using Backlog.Server.Features.ServiceProtocols;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using System.Text;

    public static class ServiceCollectionExtensions
    {
        public static AppSettings GetApplicationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsConfiguration = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsConfiguration);
            return appSettingsConfiguration.Get<AppSettings>();
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
            => services.AddDbContext<BacklogDbContext>(options => options
                .UseSqlServer(configuration.GetDefaultConnectionString()));
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
               .AddEntityFrameworkStores<BacklogDbContext>();
            return services;
        }

        public static IServiceCollection AddJWTAuthentication(this IServiceCollection services, AppSettings appSettings)
        {
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                });
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services
                .AddTransient<IIdentityService, IdentityService>()
                .AddTransient<IServiceProtocolService, ServiceProtocolService>()
                .AddTransient<ICompanyService, CompanyService>();

        public static IServiceCollection AddSwagger(this IServiceCollection services)
            => services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My Backlog API", Version = "v1" });
            });
    }
}
