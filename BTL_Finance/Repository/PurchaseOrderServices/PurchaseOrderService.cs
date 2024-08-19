using BTL_Finance.Models;
using BTL_Finance.Models.Context;
using BTL_Finance.Repository.RequestServices;
using Repository.BaseService;

namespace BTL_Finance.Repository.PurchaseOrderServices
{
    public class PurchaseOrderService : BaseService<PurchaseOrder>, IPurchaseOrderService
    {
        private readonly ApplicationDbContext dContext;

        public PurchaseOrderService(ApplicationDbContext dContext) : base(dContext)
        {
            this.dContext = dContext;
        }

        public List<PurchaseOrder> GetAllPurchaseOrder()
        {
            return dContext.PurchaseOrders.Include(a => a.RequestForm).ToList();

        }
    }
}
