using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadAsService.Domain.Domains
{
    public class TechnologyJobTitle
    {
        public int TechnologyId { get; set; }
        public virtual Technology Technology { get; set; }
        public int JobTitleId { get; set; }
        public virtual JobTitle JobTitle { get; set; }
    }
}
