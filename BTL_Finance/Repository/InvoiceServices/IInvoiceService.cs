using BTL_Finance.Models;
using Repository.BaseService;

namespace BTL_Finance.Repository.InvoiceServices
{
    public interface IInvoiceService : IBaseService<Invoice>
    {
        List<Invoice> GetAllInvoice();
    }
}
