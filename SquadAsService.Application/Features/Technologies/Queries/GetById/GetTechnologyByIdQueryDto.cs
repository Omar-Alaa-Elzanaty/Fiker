using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadAsService.Application.Features.Technologies.Queries.GetById
{
    public class GetTechnologyByIdQueryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
    }

    public class TechAreaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
