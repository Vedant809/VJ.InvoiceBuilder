using System.ComponentModel.DataAnnotations.Schema;

namespace VJ.InvoiceBuilder.Entity
{
    [Table("Invoice")]
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public string? CreatedBy { get; set; }
        public int CustomerId { get; set; }
        public int SenderId { get; set; }

        [ForeignKey("CustomerId")]
        [InverseProperty("Invoice")]
        public virtual Customer? Customer { get; set; }
        [ForeignKey("SenderId")]
        [InverseProperty("Invoice")]
        public virtual Sender? Sender { get; set; }
        [InverseProperty("Invoice")]
        public virtual ICollection<InvoiceItem>? InvoiceItem { get; set; }

    }
}
