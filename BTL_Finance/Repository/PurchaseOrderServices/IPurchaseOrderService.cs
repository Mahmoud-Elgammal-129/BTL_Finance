using BTL_Finance.Models;
using Repository.BaseService;

namespace BTL_Finance.Repository.PurchaseOrderServices
{
    public interface IPurchaseOrderService : IBaseService<PurchaseOrder>
    {
        List<PurchaseOrder> GetAllPurchaseOrder();
    }
}
