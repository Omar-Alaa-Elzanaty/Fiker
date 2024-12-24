using Mapster;
using MediatR;
using SquadAsService.Application.Interfaces.Repo;
using SquadAsService.Domain.Bases;
using SquadASService.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SquadAsService.Application.Features.Orders.Queries.GetById
{
    public class GetOrderByIdQuery:IRequest<BaseResponse<GetOrderByIdQueryDto>>
    {
        public int Id { get; set; }

        public GetOrderByIdQuery(int id)
        {
            Id = id;
        }
    }

    internal class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, BaseResponse<GetOrderByIdQueryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetOrderByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<GetOrderByIdQueryDto>> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Repository<Order>().GetByIdAsync(query.Id);

            if (entity == null)
            {
                return BaseResponse<GetOrderByIdQueryDto>.Fail("Order not found.",HttpStatusCode.NotFound);
            }

            var order = entity.Adapt<GetOrderByIdQueryDto>();

            return BaseResponse<GetOrderByIdQueryDto>.Success(order);
        }
    }
}