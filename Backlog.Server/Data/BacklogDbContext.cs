namespace Backlog.Server.Data
{
    using Backlog.Server.Data.Models;
    using Backlog.Server.Features.ServiceProtocols.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using ServiceManager.Models;
    public class BacklogDbContext : IdentityDbContext<User>
    {
        public BacklogDbContext(DbContextOptions<BacklogDbContext> options)
            : base(options)
        {
        }

        public DbSet<IServiceProtocol> ServiceProtocols { get; set; }
        public DbSet<CompanyProfile> CompanyProfile { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ServiceProtocol>()
                .HasOne(s => s.User)
                .WithMany(s => s.ServiceProtocols)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
