
using BTL_Finance.Models;
using BTL_Finance.Models.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository.BaseService;

namespace Business.Services.AuditTrailService;

public class AuditTrailService : BaseService<AuditTrail>, IAuditTrailService
{

    private readonly ApplicationDbContext dContext;

    public AuditTrailService(ApplicationDbContext dContext) : base(dContext)
    {
        this.dContext = dContext;
    }

    public async Task<List<AuditTrail>> GetAllAuditTrailsById(Guid Id)
    {
        return await dContext.AuditTrails
          .Where(i => i.SourceId == Id)
          .OrderBy(i => i.CreatedOn)
          .ToListAsync();
    }
}
