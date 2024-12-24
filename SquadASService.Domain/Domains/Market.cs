using SquadAsService.Domain.Bases;
using SquadAsService.Domain.IBases;

namespace SquadAsService.Domain.Domains
{
    public class Market : BaseEntity, IClassification
    {
        public string Name { get; set; }
        public string IconUrl { get; set; }
    }
}
