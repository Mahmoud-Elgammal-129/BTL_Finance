using BTL_Finance.Models;
using BTL_Finance.Models.Context;
using BTL_Finance.Repository.DeliveryNoteServices;
using Repository.BaseService;

namespace BTL_Finance.Repository.OrderSheetServices
{
    public class OrderSheetService : BaseService<OrderSheet>, IOrderSheetService
    {
        private readonly ApplicationDbContext dContext;

        public OrderSheetService(ApplicationDbContext dContext) : base(dContext)
        {
            this.dContext = dContext;
        }

        public List<OrderSheet> GetAllOrderSheet()
        {
            return dContext.OrderSheets.Include(a => a.RequestForm).ToList();

        }
    }
}
