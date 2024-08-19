using BTL_Finance.Models;
using BTL_Finance.Models.Context;
using BTL_Finance.Repository.DeliveryNoteServices;
using Repository.BaseService;

namespace BTL_Finance.Repository.InvoiceServices
{
    public class InvoiceService : BaseService<Invoice>, IInvoiceService
    {
        private readonly ApplicationDbContext dContext;

        public InvoiceService(ApplicationDbContext dContext) : base(dContext)
        {
            this.dContext = dContext;
        }

        public List<Invoice> GetAllInvoice()
        {
            return dContext.Invoices.Include(a => a.RequestForm).ToList();

        }
    }
}
