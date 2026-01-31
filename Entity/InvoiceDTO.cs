namespace VJ.InvoiceBuilder.Entity
{
    public class InvoiceDTO
    {
        public int customerId { get; set; }
        public int SenderId { get; set; }
        public string? CreatedBy { get; set; }
        public List<InvoiceItems>? Items { get; set; }

    }
    public class InvoiceItems
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
    public class InvoiceResponse
    {
        public int InvoiceId { get; set; }
        public CustomerData? Customer { get; set; }
        public SenderData? Sender { get; set; }
        public List<ItemListData>? Items { get; set; }
        public int TotalAmount { get; set; }
    }
    public class CustomerData
    {
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Fax { get; set; }
        public string? Website { get; set; }
    }
    public class SenderData
    {
        public int SenderId { get; set; }
        public string? SenderName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Fax { get; set; }
        public string? Website { get; set; }
    }
    public class ItemListData
    {
        public int InvoiceItemId { get; set; }
        public int InvoiceId { get; set; }
        public int ItemId { get; set; }
        public string? Description { get; set; }
        public int Rate { get; set; }
        public int Quantity { get; set; }
        public int Amount { get; set; }
    }
}
