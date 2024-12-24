using SquadAsService.Domain.Bases;
using SquadAsService.Domain.IBases;

namespace SquadAsService.Domain.Domains
{
    public class Area : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<AreaTechonolgy> Techonolgies { get; set; }
    }
}
