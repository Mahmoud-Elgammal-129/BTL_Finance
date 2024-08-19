using System.ComponentModel.DataAnnotations;

namespace BTL_Finance.Models.GenericModels
{
    public class BaseEntity
    {
        [Key]
        public Guid ID { get; set; } = Guid.NewGuid();
       
        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public Guid CreatedBy { get; set; }
        public DateTime LastModifiedOn { get; set; } = DateTime.Now;
        public Guid LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
