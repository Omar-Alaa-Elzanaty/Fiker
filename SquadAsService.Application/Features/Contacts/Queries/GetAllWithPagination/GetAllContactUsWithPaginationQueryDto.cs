using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadAsService.Application.Features.Contacts.Queries.GetAllWithPagination
{
    public class GetAllContactUsWithPaginationQueryDto
    {
        public int Id { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string Telphone { get; set; }
        public string Company { get; set; }
    }
}
