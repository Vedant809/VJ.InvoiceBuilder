using VJ.InvoiceBuilder.Entity;

namespace VJ.InvoiceBuilder.Interface
{
    public interface IInvoiceService
    {
        Task<int> CreateInvoice(InvoiceDTO request);
        InvoiceResponse ViewInvoice(int invoiceId);
        string BuildInvoiceHtml(InvoiceResponse invoice);
        byte[] GeneratePdfFromHtml(string html);
    }
}
