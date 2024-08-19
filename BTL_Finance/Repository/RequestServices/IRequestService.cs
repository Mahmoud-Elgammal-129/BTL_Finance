
using Repository.BaseService;

namespace BTL_Finance.Repository.RequestServices
{
    public interface IRequestService : IBaseService<Request>
    {
        //List<Request> GetAllRequest();
        Request GetRequestDetailsByName(string requestName);
    }
}
