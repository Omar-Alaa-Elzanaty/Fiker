using SquadAsService.Domain.Bases;
using SquadAsService.Domain.IBases;

namespace SquadAsService.Domain.Domains
{
    public class Technology : BaseEntity, IClassification
    {
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public virtual List<AreaTechonolgy> Areas { get; set; }
        public virtual List<TechnologyJobTitle> JobTitles { get; set; }
    }
}