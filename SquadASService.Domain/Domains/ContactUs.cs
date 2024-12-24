using SquadAsService.Domain.Bases;

namespace SquadAsService.Domain.Domains
{
    public class ContactUs : BaseEntity
    {
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string Telphone { get; set; }
        public string Company { get; set; }
    }
}
