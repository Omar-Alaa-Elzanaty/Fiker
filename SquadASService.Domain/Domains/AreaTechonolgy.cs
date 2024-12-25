namespace Fiker.Domain.Domains
{
    public class AreaTechonolgy
    {
        public int AreaId { get; set; }
        public virtual Area Area { get; set; }
        public int TechnologyId { get; set; }
        public virtual Technology Technology { get; set; }
    }
}
