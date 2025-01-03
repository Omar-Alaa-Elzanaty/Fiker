﻿using Fiker.Domain.Bases;

namespace Fiker.Domain.Domains
{
    public class Order : BaseEntity
    {
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string Telephone { get; set; }
        public string? Company { get; set; }
        public string? Question { get; set; }
        public int AreaId { get; set; }
        public virtual Area Area { get; set; }
        public int MarketId { get; set; }
        public virtual Market Market { get; set; }
        public bool IsDeleted { get; set; }
        public int TechnologyId { get; set; }
        public int Cost { get; set; }
        public int MonthsCount { get; set; }
        public virtual Technology Technology { get; set; }
        public virtual List<OrderJobTitle> Profiles { get; set; }
    }
}
