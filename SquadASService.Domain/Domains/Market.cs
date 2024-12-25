using Fiker.Domain.Bases;
using Fiker.Domain.IBases;

namespace Fiker.Domain.Domains
{
    public class Market : BaseEntity, IClassification
    {
        public string Name { get; set; }
        public string IconUrl { get; set; }
    }
}
