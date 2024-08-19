using BTL_Finance.Models;
using Repository.BaseService;

namespace BTL_Finance.Repository.QuoteServices
{
    public interface IQuoteService : IBaseService<Quote>
    {
        List<Quote> GetAllQuote();
    }
}
