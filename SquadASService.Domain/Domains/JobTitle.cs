using SquadAsService.Domain.Bases;
using SquadASService.Domain.Domains;

namespace SquadAsService.Domain.Domains
{
    public class JobTitle : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<TechnologyJobTitle> Technologies { get; set; }
        public virtual List<OrderJobTitle> JobTitles { get; set; }
    }
}
