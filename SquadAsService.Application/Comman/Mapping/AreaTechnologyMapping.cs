using Mapster;
using SquadAsService.Application.Features.Areas.Queries.GetById;
using SquadAsService.Application.Features.Technologies.Queries.GetById;
using SquadAsService.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadAsService.Application.Comman.Mapping
{
    public class AreaTechnologyMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AreaTechonolgy, TechAreaDto>()
                .Map(dest => dest.Id, src => src.AreaId)
                .Map(dest => dest.Name, src => src.Area.Name);

            config.NewConfig<AreaTechonolgy, AreaTechonolgyDto>()
                .Map(dest => dest, src => src.Technology);
        }
    }
}
