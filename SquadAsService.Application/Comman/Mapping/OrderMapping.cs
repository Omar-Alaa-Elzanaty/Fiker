using Mapster;
using Fiker.Application.Features.Orders.Queries.GetById;
using Fiker.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Application.Comman.Mapping
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
