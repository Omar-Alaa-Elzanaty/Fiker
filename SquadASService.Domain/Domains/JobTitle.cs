using Fiker.Domain.Bases;
using Fiker.Domain.Domains;

namespace Fiker.Domain.Domains
{
    public class JobTitle : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<TechnologyJobTitle> Technologies { get; set; }
    }
}
