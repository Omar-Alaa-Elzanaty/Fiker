using Hangfire.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Application.Comman.Dtos
{
    public class OrderReportDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Market { get; set; }
        public string Area { get; set; }
        public string Technology { get; set; }
        public List<JobTitleDto> Jobs { get; set; } = [];
        public int TotalCost => Jobs.Sum(job => job.Quantity * job.Price);
    }

    public class JobTitleDto
    {
        public string Title { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
