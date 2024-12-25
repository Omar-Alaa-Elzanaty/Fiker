using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Domain.IBases
{
    public interface IClassification
    {
        public string Name { get; set; }
        public string IconUrl { get; set; }
    }
}
