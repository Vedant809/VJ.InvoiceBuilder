using System.ComponentModel.DataAnnotations.Schema;

namespace VJ.InvoiceBuilder.Entity
{
    [Table("InvoiceItem")]
    public class InvoiceItem
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("ItemId")]
        [InverseProperty("InvoiceItem")]
        public virtual Item? ItemList { get; set; }
        [ForeignKey("InvoiceId")]
        [InverseProperty("InvoiceItem")]
        public virtual Invoice? Invoice { get; set; }
    }
}
