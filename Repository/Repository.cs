using Microsoft.EntityFrameworkCore;
using VJ.InvoiceBuilder.Entity;
using VJ.InvoiceBuilder.Interface;

namespace VJ.InvoiceBuilder.Repository
{
    public class Repository: IRepository
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddInvoice(Invoice entity)
        {
            _context.Invoice.Add(entity);
            await _context.SaveChangesAsync();
            return entity.InvoiceId;
        }
        public IQueryable<Invoice> GetAll()
        {
            return _context.Invoice
                .Include(x => x.Customer)
                .Include(x => x.Sender)
                .Include(x => x.InvoiceItem)
                .ThenInclude(x => x.ItemList);
        }
    }
}
