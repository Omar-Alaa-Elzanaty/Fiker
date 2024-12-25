using MediatR;
using Fiker.Application.Interfaces.Repo;
using Fiker.Domain.Bases;
using Fiker.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Application.Features.Contacts.Command.Delete
{
    public record DeleteContactUsCommand:IRequest<BaseResponse<string>>
    {
        public int Id { get; set; }

        public DeleteContactUsCommand(int id)
        {
            Id = id;
        }
    }

    internal class DeleteContactUsCommandHandler : IRequestHandler<DeleteContactUsCommand, BaseResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteContactUsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<string>> Handle(DeleteContactUsCommand request, CancellationToken cancellationToken)
        {
            var contactUs = await _unitOfWork.Repository<ContactUs>().GetByIdAsync(request.Id);

            if (contactUs == null)
            {
                return BaseResponse<string>.Fail($"Contact info not found.", HttpStatusCode.NotFound);
            }

            _unitOfWork.Repository<ContactUs>().Delete(contactUs);
            await _unitOfWork.SaveAsync();

            return BaseResponse<string>.Success($"Contact deleted.");
        }
    }
}
