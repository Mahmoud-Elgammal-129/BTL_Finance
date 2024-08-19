using BTL_Finance.Models.GenericModels;

namespace Data.Models;

public class AuditTrail : BaseEntity
{
    public string? Description { get; set; }
    public Guid? SourceId { get; set; }
    public string Source { get; set; }
    public string? JsonData { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public string User { get; set; }
    public string? Type { get; set; }
}
