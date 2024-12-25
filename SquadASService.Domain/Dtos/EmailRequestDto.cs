using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Domain.Dtos
{
    public class EmailRequestDto
    {
        public List<string> To { get; set; }
        public string CC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public object BodyData { get; set; }
        public string From { get; set; }
        public string Email { get; set; }
    }
}
