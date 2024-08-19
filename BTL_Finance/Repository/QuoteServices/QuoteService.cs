using BTL_Finance.Models;
using BTL_Finance.Models.Context;
using BTL_Finance.Repository.DeliveryNoteServices;
using Repository.BaseService;

namespace BTL_Finance.Repository.QuoteServices
{
    public class QuoteService : BaseService<Quote>, IQuoteService
    {
        private readonly ApplicationDbContext dContext;

        public QuoteService(ApplicationDbContext dContext) : base(dContext)
        {
            this.dContext = dContext;
        }

        public List<Quote> GetAllQuote()
        {
            return dContext.Quotes.Include(a => a.RequestForm).ToList();

        }
    }
}
