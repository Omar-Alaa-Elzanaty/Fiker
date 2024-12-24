using Mapster;
using SquadAsService.Application.Features.Orders.Queries.GetById;
using SquadASService.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadAsService.Application.Comman.Mapping
{
    public class OrderMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Order, GetOrderByIdQueryDto>()
                .Map(dest => dest.Area, src => src.Area.Name)
                .Map(dest => dest.Market, src => src.Market.Name)
                .Map(dest => dest.Technology, src => src.Technology.Name);
        }
    }
}
