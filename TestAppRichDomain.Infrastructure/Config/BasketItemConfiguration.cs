using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestAppRichDomain.Core.Entries;

namespace TestAppRichDomain.Infrastructure.Config
{
    public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.Property(x => x.Price)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");
        }
    }
}
