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
    internal class AreaTechnologyConfig : IEntityTypeConfiguration<AreaTechonolgy>
    {
        public void Configure(EntityTypeBuilder<AreaTechonolgy> builder)
        {
            builder.HasKey(x => new { x.AreaId, x.TechnologyId });
        }
    }
}
