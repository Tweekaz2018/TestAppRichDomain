using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestAppRichDomain.Core.Entries;

namespace TestAppRichDomain.Infrastructure.Config
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.Property(x => x.Title)
                   .IsRequired(true)
                   .HasMaxLength(50);

            builder.Property(x => x.Price)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Image)
                .IsRequired(true);

            builder.Property(x => x.Description)
                .IsRequired(true)
                .HasMaxLength(1000);
        }
    }
}