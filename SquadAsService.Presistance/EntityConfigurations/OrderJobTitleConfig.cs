using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SquadASService.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadAsService.Presistance.EntityConfigurations
{
    internal class OrderJobTitleConfig : IEntityTypeConfiguration<OrderJobTitle>
    {
        public void Configure(EntityTypeBuilder<OrderJobTitle> builder)
        {
            builder.HasKey(x => new {x.OrderId, x.JobTitleId });

            builder.ToTable(x => x.HasCheckConstraint("Quantity_Constrain", "[Quantity] <= 21"));
        }
    }
}
