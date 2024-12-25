using Mapster;
using Fiker.Application.Features.Areas.Queries.GetById;
using Fiker.Application.Features.Technologies.Queries.GetById;
using Fiker.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Application.Comman.Mapping
{
    public class AreaTechnologyMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AreaTechonolgy, TechAreaDto>()
                .Map(dest => dest.Id, src => src.AreaId)
                .Map(dest => dest.Name, src => src.Area.Name);
        }
    }
}
