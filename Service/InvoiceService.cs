using System.Text;
using VJ.InvoiceBuilder.Entity;
using VJ.InvoiceBuilder.Interface;
using PdfSharpCore.Pdf;
using DinkToPdf;


namespace VJ.InvoiceBuilder.Service
{
    public class InvoiceService: IInvoiceService
    {
        private readonly IRepository _repository;
        public InvoiceService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateInvoice(InvoiceDTO request)
        {
            Invoice entity = new Invoice()
            {
                CreatedBy = request.CreatedBy,
                CustomerId = request.customerId,
                SenderId = request.SenderId,
                InvoiceItem = request?.Items?.Select(x=>new InvoiceItem()
                {
                    ItemId = x.ItemId,
                    Quantity = x.Quantity
                }).ToList()
            };
            var result = await _repository.AddInvoice(entity);
            //var invoice = ViewInvoice(result);

            return result;
        }
        public InvoiceResponse ViewInvoice(int invoiceId)
        {
            InvoiceResponse response = new();
            var entities = _repository.GetAll()
                .Where(x => x.InvoiceId == invoiceId)
                .FirstOrDefault();
            response.InvoiceId = entities.InvoiceId;
            response.Customer = new CustomerData()
            {
                CustomerId = entities.CustomerId,
                CustomerName = entities.Customer!=null ? entities.Customer.CustomerName:string.Empty,
                PhoneNumber = entities.Customer != null ? entities.Customer.PhoneNumber : string.Empty,
                Fax = entities.Customer != null ? entities.Customer.Fax: string.Empty,
                Address = entities.Customer != null ? entities.Customer.Address :string.Empty,
                Website = entities.Customer != null ? entities.Customer.Website :string.Empty
            };
            response.Sender = new SenderData()
            {
                SenderId = entities.SenderId,
                SenderName = entities.Sender != null ? entities.Sender.SenderName :string.Empty,
                PhoneNumber = entities.Sender != null ? entities.Sender.PhoneNumber : string.Empty,
                Fax = entities.Sender != null ? entities.Sender.Fax : string.Empty,
                Address = entities.Sender != null ? entities.Sender.Address : string.Empty,
                Website = entities.Sender != null ? entities.Sender.Website : string.Empty
            };
            response.Items = entities?.InvoiceItem?.Select(x => new ItemListData()
            {
                InvoiceItemId = x.Id,
                ItemId = x.ItemId,
                InvoiceId = x.InvoiceId,
                Description = x.ItemList!=null ? x.ItemList.Description:string.Empty,
                Quantity = x.Quantity,
                Rate = x.ItemList != null ? x.ItemList.Rate:0,
                Amount = x.ItemList!=null ? ((x.ItemList.Rate) * (x.Quantity)):0
            }).ToList();
            var total = 0;
            foreach(var item in response.Items)
            {
                total = total + item.Amount;
            }
            response.TotalAmount = total;
            return response;
        }


        public byte[] GeneratePdfFromHtml(string html)
        {
            var renderer = new ChromePdfRenderer();

            // Optional settings
            renderer.RenderingOptions.PaperSize = IronPdf.Rendering.PdfPaperSize.A4;
            renderer.RenderingOptions.MarginTop = 20;
            renderer.RenderingOptions.MarginBottom = 20;

            var pdf = renderer.RenderHtmlAsPdf(html);

            return pdf.BinaryData;
        }
        public string BuildInvoiceHtml(InvoiceResponse invoice)
        {
            var sb = new StringBuilder();

            sb.Append(@"
                        <!DOCTYPE html>
                        <html>
                        <head>
                            <meta charset='utf-8'>
                            <title>Invoice</title>
                        </head>
                        <body style='font-family: Arial, sans-serif;'>

                            <h2>INVOICE</h2>

                            <p>
                                <strong>Invoice ID:</strong> " + invoice.InvoiceId + @"
                            </p>

                            <hr />

                            <h3>From (Sender)</h3>
                            <p>
                                <strong>" + invoice?.Sender?.SenderName + @"</strong><br />
                                " + invoice?.Sender?.Address + @"<br />
                                Phone: " + invoice?.Sender?.PhoneNumber + @"<br />
                                Fax: " + invoice?.Sender?.Fax + @"<br />
                                Website: " + invoice?.Sender?.Website + @"
                            </p>

                            <hr />

                            <h3>Bill To (Customer)</h3>
                            <p>
                                <strong>" + invoice?.Customer?.CustomerName + @"</strong><br />
                                " + invoice?.Customer?.Address + @"<br />
                                Phone: " + invoice?.Customer?.PhoneNumber + @"<br />
                                Fax: " + invoice?.Customer?.Fax + @"<br />
                                Website: " + invoice?.Customer?.Website + @"
                            </p>

                            <hr />

                            <h3>Items</h3>

                            <table border='1' width='100%' cellpadding='5' cellspacing='0'>
                                <thead>
                                    <tr>
                                        <th>Description</th>
                                        <th>Rate</th>
                                        <th>Quantity</th>
                                        <th>Amount</th>
                                    </tr>
                                </thead>
                                <tbody>
                        ");

                                    // 🔁 Item Loop
                                    foreach (var item in invoice.Items)
                                    {
                                        sb.Append(@"
                                    <tr>
                                        <td>" + item.Description + @"</td>
                                        <td>" + item.Rate + @"</td>
                                        <td>" + item.Quantity + @"</td>
                                        <td>" + item.Amount + @"</td>
                                    </tr>
                                ");
                                    }

                                    sb.Append(@"
                                </tbody>
                            </table>

                            <br />

                            <h3>Total Amount: " + invoice.TotalAmount + @"</h3>

                            <hr />

                            <p>Thank you for your business.</p>

                        </body>
                        </html>
                        ");
            return sb.ToString();
        }

    }
}
