using VJ.InvoiceBuilder.Entity;

namespace VJ.InvoiceBuilder.Interface
{
    public interface IRepository
    {
        Task<int> AddInvoice(Invoice entity);
        IQueryable<Invoice> GetAll();
    }
}
