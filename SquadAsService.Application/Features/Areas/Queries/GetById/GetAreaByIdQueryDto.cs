using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadAsService.Application.Features.Areas.Queries.GetById
{
    public class GetAreaByIdQueryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AreaTechonolgyDto> Techonolgies { get; set; }
    }

    public class AreaTechonolgyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
    }
}
