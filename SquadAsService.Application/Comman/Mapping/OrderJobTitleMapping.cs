using Mapster;
using Fiker.Application.Features.Orders.Queries.GetById;
using Fiker.Domain.Domains;

namespace Fiker.Application.Comman.Mapping
{
    public class OrderJobTitleMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<OrderJobTitle, OrderProfileDto>()
                .Map(dest => dest.JobTitle, src => src.JobTitle);
        }
    }
}
