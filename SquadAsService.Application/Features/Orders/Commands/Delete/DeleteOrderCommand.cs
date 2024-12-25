using MediatR;
using Fiker.Application.Interfaces.Repo;
using Fiker.Domain.Bases;
using Fiker.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Application.Features.Orders.Commands.Delete
{
    public record DeleteOrderCommand:IRequest<BaseResponse<string>>
    {
        public int Id { get; set; }

        public DeleteOrderCommand(int id)
        {
            Id = id;
        }
    }

    internal class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, BaseResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<string>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(request.Id);

            if (order == null)
            {
                return BaseResponse<string>.Fail("Order not found");
            }

            order.IsDeleted = true;

            _unitOfWork.Repository<Order>().UpdateAsync(order);
            await _unitOfWork.SaveAsync();

            return BaseResponse<string>.Success("Order deleted.");
        }
    }
}
