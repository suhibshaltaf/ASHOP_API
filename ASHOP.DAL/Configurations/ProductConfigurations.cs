using ASHOP.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASHOP.DAL.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(c => c.CreatedBy)
                .WithMany()
                .HasForeignKey(ct => ct.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.UpdatedBy)
                .WithMany()
                .HasForeignKey(ct => ct.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Translations)
                .WithOne(t => t.Product)
                .HasForeignKey(t => t.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
