
using BTL_Finance.Models.Context;
using Repository.BaseService;

namespace BTL_Finance.Repository.RequestServices
{
    public class RequestService: BaseService<Request>, IRequestService
    {
        private readonly ApplicationDbContext dContext;

        public RequestService(ApplicationDbContext dContext) : base(dContext)
        {
            this.dContext = dContext;
        }

        public Request GetRequestDetailsByName(string requestName)
        {
            var request = dContext.Requests
                                 .Include(r => r.OrderSheets)
                                 .Include(r => r.Quotes)
                                 .Include(r => r.PurchaseOrders)
                                 .Include(r => r.DeliveryNotes)
                                 .Include(r => r.Invoices)
                                 .Where(r => r.Request_Name == requestName)
                                 .Select(r => new Request
                                 {
                                     Request_Name = r.Request_Name,
                                     process = r.process,
                                     Type = r.Type,
                                     ClientName = r.ClientName,
                                     CompanyName = r.CompanyName,
                                     Email = r.Email,
                                     Mobile = r.Mobile,
                                     Channel = r.Channel,
                                     AttachmentPath = r.AttachmentPath,
                                     Status_Request = r.Status_Request,
                                     Status_order_sheet = r.Status_order_sheet,
                                     Status_Delivery_Note = r.Status_Delivery_Note,
                                     Status_PO = r.Status_PO,
                                     Status_Quote = r.Status_Quote,
                                     Status_Invoice = r.Status_Invoice,
                                     OrderSheets = r.OrderSheets.ToList(),
                                     Quotes = r.Quotes.ToList(),
                                     PurchaseOrders = r.PurchaseOrders.ToList(),
                                     DeliveryNotes = r.DeliveryNotes.ToList(),
                                     Invoices = r.Invoices.ToList()
                                 }).FirstOrDefault();

            return request;
        }
    }
}
