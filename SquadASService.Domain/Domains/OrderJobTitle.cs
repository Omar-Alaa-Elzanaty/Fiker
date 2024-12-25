using Fiker.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Domain.Domains
{
    public class OrderJobTitle
    {
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public string JobTitle { get; set; }
        public int Quantity { get; set; }
    }
}
