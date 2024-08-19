using BTL_Finance.Models.GenericModels;

namespace BTL_Finance.Models
{
    public class Notification : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public Guid? To { get; set; }
        public Guid? RequestId {  get; set; }
    }
}
