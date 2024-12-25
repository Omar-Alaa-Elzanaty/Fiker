using Mapster;
using MediatR;
using Fiker.Application.Extensions;
using Fiker.Application.Interfaces.Repo;
using Fiker.Domain.Bases;
using Fiker.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Application.Features.Contacts.Queries.GetAllWithPagination
{
    public record GetAllContactUsWithPaginationQuery : PaginationRequest, IRequest<PaginatedResponse<GetAllContactUsWithPaginationQueryDto>>;

    internal class GetAllContactUsWithPaginationQueryHandler : IRequestHandler<GetAllContactUsWithPaginationQuery, PaginatedResponse<GetAllContactUsWithPaginationQueryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllContactUsWithPaginationQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResponse<GetAllContactUsWithPaginationQueryDto>> Handle(GetAllContactUsWithPaginationQuery query, CancellationToken cancellationToken)
        {
            var contacts = await _unitOfWork.Repository<ContactUs>().Entities
                .ProjectToType<GetAllContactUsWithPaginationQueryDto>()
                .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);

            return contacts;
        }
    }
}
