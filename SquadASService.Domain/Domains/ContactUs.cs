using Fiker.Domain.Bases;

namespace Fiker.Domain.Domains
{
    public class ContactUs : BaseEntity
    {
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string Telphone { get; set; }
        public string Company { get; set; }
        public string? Question { get; set; }
    }
}
