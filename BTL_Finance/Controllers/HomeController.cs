using BTL_Finance.Repository.DeliveryNoteServices;
using BTL_Finance.Repository.InvoiceServices;
using BTL_Finance.Repository.OrderSheetServices;
using BTL_Finance.Repository.PurchaseOrderServices;
using BTL_Finance.Repository.QuoteServices;
using BTL_Finance.Repository.RequestServices;
using Microsoft.AspNetCore.Mvc;

namespace BTL_Finance.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRequestService Request_Service;
        private readonly IOrderSheetService OrderSheetService;
        private readonly IPurchaseOrderService PurchaseOrderService;
        private readonly IQuoteService QuoteService;
        private readonly IDeliveryNoteService deliveryNoteService;
        private readonly IInvoiceService invoiceService;


        public HomeController(IRequestService request_Service, IOrderSheetService orderSheetService, IPurchaseOrderService purchaseOrderService, IQuoteService quoteService, IDeliveryNoteService deliveryNoteService, IInvoiceService invoiceService)
        {
            Request_Service = request_Service;
            OrderSheetService = orderSheetService;
            PurchaseOrderService = purchaseOrderService;
            QuoteService = quoteService;
            this.deliveryNoteService = deliveryNoteService;
            this.invoiceService = invoiceService;
        }

        public async Task<IActionResult> Home()
        {
            List<ProgressDTo> progresses = new List<ProgressDTo>();
            var result = await Request_Service.GetAll();
            if (result.Count > 0)
            {


                
                foreach (var item in result)
                {
                    // we will gets all status based on request Id and return the Status of it 
                    var progress = new ProgressDTo();


                    progress.Name_progress = item.ClientName;
                    progress.RequestName = item.Request_Name;
                    progress.CompanyName = item.CompanyName;
                    progress.Reqest_Status = true;

                    var Order_Sheet = await OrderSheetService.GetById(item.ID);
                    if (Order_Sheet != null)
                    {
                        progress.Order_Sheet_Status = true;
                    }
                    // quote
                    var Quote_sheet = await QuoteService.GetById(item.ID);
                    if (Quote_sheet != null)
                    {
                        progress.Quote_Status = true;
                    }
                    //po
                    var po_sheet = await PurchaseOrderService.GetById(item.ID);
                    if (po_sheet != null)
                    {
                        progress.PO_Status = true;
                    }
                    //delvir
                    var Delivery_Sheet = await deliveryNoteService.GetById(item.ID);
                    if (Delivery_Sheet != null)
                    {
                        progress.Delivery_Note_Status = true;
                    }
                    //invoice
                    var invoice_status = await invoiceService.GetById(item.ID);
                    if (invoice_status != null)
                    {
                        progress.Invoice_Status = true;
                    }
                    progresses.Add(progress);
                }
                
            }
            return View(progresses);
            //return View(result);
        }
        public IActionResult Unauthorized()
        {
            return View();
        }
    }
}
