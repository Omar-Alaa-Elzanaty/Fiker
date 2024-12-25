using Mapster;
using MediatR;
using Fiker.Application.Extensions;
using Fiker.Application.Interfaces.Repo;
using Fiker.Domain.Bases;
using Fiker.Domain.Domains;

namespace Fiker.Application.Features.Orders.Queries.GetAllWithPagination
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