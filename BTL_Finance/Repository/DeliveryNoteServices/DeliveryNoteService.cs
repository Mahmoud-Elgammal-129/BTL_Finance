using BTL_Finance.Models.Context;
using BTL_Finance.Repository.RequestServices;
using Repository.BaseService;
using BTL_Finance.Models;
using BTL_Finance.Models.Context;

namespace BTL_Finance.Repository.DeliveryNoteServices
{
    public class DeliveryNoteService : BaseService<DeliveryNote>, IDeliveryNoteService
    {
        private readonly ApplicationDbContext dContext;

        public DeliveryNoteService(ApplicationDbContext dContext) : base(dContext)
        {
            this.dContext = dContext;
        }

        public List<DeliveryNote> GetAllDeliveryNote()
        {
            return dContext.DeliveryNotes.Include(a => a.RequestForm).ToList();
        }
    }
}
