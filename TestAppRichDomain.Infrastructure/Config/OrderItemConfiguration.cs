using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestAppRichDomain.Core.Entries;

namespace TestAppRichDomain.Infrastructure.Config
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(x => x.ItemOrdered)
                .IsRequired(true);

            builder.Property(x => x.Quantity)
                .IsRequired(true);

            builder.Property(x => x.Title)
                .IsRequired(true);

            builder.Property(x => x.Price)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");
        }
    }
}
