using Mapster;
using SquadAsService.Application.Features.Orders.Queries.GetById;
using SquadASService.Domain.Domains;

namespace SquadAsService.Application.Comman.Mapping
{
    public class OrderJobTitleMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<OrderJobTitle,OrderProfileDto>()
                .Map(dest => dest.JobTitle, src => src.JobTitle.Name);
        }
    }
}
