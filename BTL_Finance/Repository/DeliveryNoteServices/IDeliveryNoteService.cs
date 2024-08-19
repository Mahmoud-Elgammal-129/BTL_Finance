using BTL_Finance.Models;
using Repository.BaseService;

namespace BTL_Finance.Repository.DeliveryNoteServices
{
    public interface IDeliveryNoteService : IBaseService<DeliveryNote>
    {
        List<DeliveryNote> GetAllDeliveryNote();
    }
}
