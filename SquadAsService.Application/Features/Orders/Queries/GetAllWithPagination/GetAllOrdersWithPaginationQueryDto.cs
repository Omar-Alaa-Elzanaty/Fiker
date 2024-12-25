using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Application.Features.Orders.Queries.GetAllWithPagination
{
    public class GetAllOrdersWithPaginationQueryDto
    {
        public int Id { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string Telephone { get; set; }
        public string? Company { get; set; }
    }
}
