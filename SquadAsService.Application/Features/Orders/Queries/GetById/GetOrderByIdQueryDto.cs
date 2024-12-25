using Fiker.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Application.Features.Orders.Queries.GetById
{
    public class GetOrderByIdQueryDto
    {
        public string? Question { get; set; }
        public string Area { get; set; }
        public string Market { get; set; }
        public string Technology { get; set; }
        public List<string> OrderProfiles { get; set; }
    }

    public class OrderProfileDto
    {
        public string JobTitle { get; set; }
        public int Quantity { get; set; }
    }
}
