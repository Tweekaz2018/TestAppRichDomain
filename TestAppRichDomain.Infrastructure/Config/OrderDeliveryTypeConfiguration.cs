using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestAppRichDomain.Core.Entries;

namespace TestAppRichDomain.Infrastructure.Config
{
    public class OrderDeliveryTypeConfiguration : IEntityTypeConfiguration<OrderDevileryType>
    {
        public void Configure(EntityTypeBuilder<OrderDevileryType> builder)
        {
            builder.Property(x => x.name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
