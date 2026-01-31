using System.ComponentModel.DataAnnotations.Schema;

namespace VJ.InvoiceBuilder.Entity
{
    public class Sender
    {
        public int SenderId { get; set; }
        public string? SenderName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Fax { get; set; }
        public string? Website { get; set; }
        [InverseProperty("Sender")]
        public virtual Invoice? Invoice { get; set; }
    }
}
