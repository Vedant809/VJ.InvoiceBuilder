using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VJ.InvoiceBuilder.Entity;
using VJ.InvoiceBuilder.Interface;

namespace VJ.InvoiceBuilder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost("CreateInvoice")]
        public async Task<int> AddInvoice(InvoiceDTO request)
        {
            return await _invoiceService.CreateInvoice(request);
        }
        [HttpGet("ViewInvoice")]
        public InvoiceResponse ViewInvoice(int invoiceId)
        {
            return _invoiceService.ViewInvoice(invoiceId);
        }
        [HttpGet("invoices/{id}/download")]
        public IActionResult DownloadInvoicePdf(int id)
        {
            // 1️⃣ Get invoice data
            var invoice = _invoiceService.ViewInvoice(id);

            // 2️⃣ Build HTML (your StringBuilder method)
            string html = _invoiceService.BuildInvoiceHtml(invoice);

            // 3️⃣ Convert to PDF
            byte[] pdfBytes = _invoiceService.GeneratePdfFromHtml(html);

            // 4️⃣ Return file
            return File(
                pdfBytes,
                "application/pdf",
                $"Invoice_{invoice.InvoiceId}.pdf"
            );
        }

    }
}
