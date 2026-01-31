using System.ComponentModel.DataAnnotations.Schema;

namespace VJ.InvoiceBuilder.Entity
{
    [Table("Items")]
    public class Item
    {
        public int ItemId { get; set; }
        public string? Description { get; set; }
        public int Rate { get; set; }
        [InverseProperty("ItemList")]
        public virtual ICollection<InvoiceItem>? InvoiceItem { get; set; }
    }
}
