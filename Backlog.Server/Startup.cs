namespace Backlog.Server
{
    using Backlog.Server.Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;


    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        =>  services
            .AddDatabase(this.Configuration)
            .AddIdentity()
            .AddJWTAuthentication(services.GetApplicationSettings(this.Configuration))
            .AddApplicationServices()
            .AddSwagger()
            .AddControllers();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(x => x
                   .AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader())
               .UseDeveloperExceptionPage()
               .UseSwaggerUI()
               .UseRouting()
               .UseAuthentication()
               .UseAuthorization()
               .UseEndpoints(endpoints =>
               {
                   endpoints.MapControllers();
               });
               //.ApplyMigrations();
        }
    }
}
