using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Numerics;

namespace VJ.InvoiceBuilder.Entity
{
    [Table("Customer")]
    public class Customer
    {
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Fax { get; set; }
        public string? Website { get; set; }
        [InverseProperty("Customer")]
        public virtual Invoice? Invoice { get; set; }
    }
}
