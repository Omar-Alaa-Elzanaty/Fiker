using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Application.Features.Technologies.Queries.GetById
{
    public class GetTechnologyByIdQueryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
    }
}
