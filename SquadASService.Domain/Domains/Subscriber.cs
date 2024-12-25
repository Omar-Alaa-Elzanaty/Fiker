using Fiker.Domain.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Domain.Domains
{
    public class Subscriber
    {
        public int Id { get; set; }
        public string ContactEmail { get; set; }
        public string ContactName { get; set; }
    }
}
