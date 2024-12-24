using SquadAsService.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadASService.Domain.Domains
{
    public class OrderJobTitle
    {
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public  int JobTitleId { get; set; }
        public virtual JobTitle JobTitle { get; set; }
        public int Quantity { get; set; }
    }
}
