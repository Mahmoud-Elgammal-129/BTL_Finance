using BTL_Finance.Models;
using Repository.BaseService;

namespace BTL_Finance.Repository.OrderSheetServices
{
    public interface IOrderSheetService : IBaseService<OrderSheet>
    {
        List<OrderSheet> GetAllOrderSheet();
    }
}
