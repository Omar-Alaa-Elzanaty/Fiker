using Fiker.Application.Comman.Dtos;
using Fiker.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SelectPdf;
using System.Text;

namespace Fiker.Infrastructure.Services.Report.Order
{
    public class OrderReport:IOrderReport
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _host;
        private readonly IRazorRendering _razorRendering;

        public OrderReport(
            IConfiguration configuration,
            IWebHostEnvironment host,
            IRazorRendering razorRendering)
        {
            _configuration = configuration;
            _host = host;
            _razorRendering = razorRendering;
        }

        public async Task<string> GetReportAsync(OrderReportDto order)
        {
            var content = File.ReadAllText(_host.WebRootPath + _configuration["ReportTemplates:OrderReport"]);

            var pdfView = new HtmlToPdf();

            pdfView.Options.PdfPageSize = PdfPageSize.A4;

            var renderContent = await _razorRendering.RenderToStringAsync(content, order);

            var pdf = pdfView.ConvertHtmlString(renderContent);

            var pdfAsByte = pdf.Save();

            return Encoding.UTF8.GetString(pdfAsByte);
        }
    }
}
