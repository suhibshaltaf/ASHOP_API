using ASHOP.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.DAL.Data
{
    public class ApplicationDbContext :IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }


        public DbSet<Product>  products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<ProductTranslation> ProductTranslations { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) :base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var currentUserId= _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var entries = ChangeTracker.Entries<AuditableEntity>();
            foreach (var entry in entries) {

                if (entry.State == EntityState.Added)
                {
                    entry.Property(x => x.CreatedById).CurrentValue = currentUserId;
                    entry.Property(x => x.CreatedOn).CurrentValue = DateTime.UtcNow;

                }else if(entry.State == EntityState.Modified) {
                    entry.Property(x => x.UpdatedById).CurrentValue = currentUserId;
                    entry.Property(x => x.UpdatedOn).CurrentValue = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
