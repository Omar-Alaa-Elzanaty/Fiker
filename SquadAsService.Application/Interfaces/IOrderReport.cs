using Fiker.Application.Comman.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Application.Interfaces
{
    public interface IOrderReport
    {
        Task<string> GetReportAsync(OrderReportDto order);
    }
}
