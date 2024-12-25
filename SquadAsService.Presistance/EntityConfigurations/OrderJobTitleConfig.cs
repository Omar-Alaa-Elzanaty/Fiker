using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fiker.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Presistance.EntityConfigurations
{
    internal class OrderJobTitleConfig : IEntityTypeConfiguration<OrderJobTitle>
    {
        public void Configure(EntityTypeBuilder<OrderJobTitle> builder)
        {
            builder.HasKey(x => new { x.OrderId, x.JobTitle });

            builder.ToTable(x => x.HasCheckConstraint("Quantity_Constrain", "[Quantity] <= 21"));
        }
    }
}
