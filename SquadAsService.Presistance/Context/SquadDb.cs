using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SquadAsService.Domain.Bases;
using SquadAsService.Domain.Domains;
using SquadAsService.Domain.Domains.Identity;
using SquadAsService.Presistance.Extensions;
using SquadASService.Domain.Domains;
using System.Reflection;

namespace SquadAsService.Presistance.Context
{
    public class SquadDb : IdentityDbContext<User>
    {
        public SquadDb(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users", "Accounts");
            builder.Entity<IdentityRole>().ToTable("Roles", "Accounts");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Accounts");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Accounts");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Accounts");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "Accounts");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Accounts");

            builder.ApplyGlobalFilters<BaseEntity>(x => !x.IsDeleted);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker
                .Entries()
                .Where(x => x.Entity is BaseEntity && x.State == EntityState.Added)
                .Select(x => x.Entity)
                .Cast<BaseEntity>())
            {
                entity.CreatedAt = DateTime.Now;
            }

            foreach (var entity in ChangeTracker
                .Entries()
                .Where(x => x.Entity is BaseEntity && x.State == EntityState.Deleted)
                .Select(x => x.Entity)
                .Cast<BaseEntity>())
            {
                entity.IsDeleted = true;
            }

            foreach (var entity in ChangeTracker
                .Entries()
                .Where(x => x.Entity is BaseEntity && x.State == EntityState.Deleted))
            {
                entity.State = EntityState.Modified;
                typeof(BaseEntity).GetProperty("IsDeleted")!.SetValue(entity.Entity, true);
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Area> Areas { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderJobTitle> OrdersJobTitles { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<AreaTechonolgy> AreaTechonolgies { get; set; }
        public DbSet<TechnologyJobTitle> TechnologyJobTitles { get; set; }
    }
}
