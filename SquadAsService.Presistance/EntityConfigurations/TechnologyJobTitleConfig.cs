﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SquadAsService.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadAsService.Presistance.EntityConfigurations
{
    public class TechnologyJobTitleConfig : IEntityTypeConfiguration<TechnologyJobTitle>
    {
        public void Configure(EntityTypeBuilder<TechnologyJobTitle> builder)
        {
            builder.HasKey(x => new {x.TechnologyId, x.JobTitleId });
        }
    }
}
