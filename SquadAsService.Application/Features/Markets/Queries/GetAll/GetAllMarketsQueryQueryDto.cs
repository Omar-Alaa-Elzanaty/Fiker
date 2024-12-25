using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Application.Features.Markets.Queries.GetAll
{
    public class GetAllMarketsQueryQueryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
    }
}
