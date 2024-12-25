using Fiker.Domain.Bases;
using Fiker.Domain.IBases;

namespace Fiker.Domain.Domains
{
    public class Area : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<AreaTechonolgy> Techonolgies { get; set; }
    }
}
