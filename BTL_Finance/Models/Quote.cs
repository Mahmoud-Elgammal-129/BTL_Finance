using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BTL_Finance.Models.GenericModels;

namespace BTL_Finance.Models
{
    public class Quote:BaseEntity
    {
        //[Required(ErrorMessage = "Type is required.")]
        //public string Type { get; set; }

        //[Required(ErrorMessage = "CompanyName is required.")]
        //public string CompanyName { get; set; }
        //[Required(ErrorMessage = "Channel is required.")]
        //public string Channel { get; set; }
        [Required(ErrorMessage = "QNumber is required.")]
        public string QNumber { get; set; }
        [Required(ErrorMessage = "AttachmentPath is required.")]
        public string AttachmentPath { get; set; }
        // Foreign Key
        public Guid RequestFormId { get; set; }
        // Navigation property
        [ForeignKey("RequestFormId")]
        public virtual Request RequestForm { get; set; }
    }
}
