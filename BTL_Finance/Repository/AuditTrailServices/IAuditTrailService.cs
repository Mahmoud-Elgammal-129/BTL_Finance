using Data.Models;
using Repository.BaseService;

namespace Business.Services.AuditTrailService;

public interface IAuditTrailService : IBaseService<AuditTrail>
{
    Task<List<AuditTrail>> GetAllAuditTrailsById(Guid Id);

}
