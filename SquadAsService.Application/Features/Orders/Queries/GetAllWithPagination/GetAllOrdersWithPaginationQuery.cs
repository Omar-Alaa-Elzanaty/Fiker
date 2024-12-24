using Mapster;
using MediatR;
using SquadAsService.Application.Extensions;
using SquadAsService.Application.Interfaces.Repo;
using SquadAsService.Domain.Bases;
using SquadASService.Domain.Domains;

namespace SquadAsService.Application.Features.Orders.Queries.GetAllWithPagination
{
    public record GetAllOrdersWithPaginationQuery : PaginationRequest, IRequest<BaseResponse<List<GetAllOrdersWithPaginationQueryDto>>>;

    internal class GetAllOrderWithPaginationQueryHandler : IRequestHandler<GetAllOrdersWithPaginationQuery, BaseResponse<List<GetAllOrdersWithPaginationQueryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllOrderWithPaginationQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<GetAllOrdersWithPaginationQueryDto>>> Handle(GetAllOrdersWithPaginationQuery query, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Repository<Order>().Entities
                        .ProjectToType<GetAllOrdersWithPaginationQueryDto>()
                        .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);

            return orders;
        }
    }
}