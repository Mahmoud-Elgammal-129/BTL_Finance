using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BTL_Finance.Models.GenericModels;

namespace BTL_Finance.Models
{
    public class DeliveryNote:BaseEntity
    {
        //[Required(ErrorMessage = "Type is required.")]
        //public string Type { get; set; }
     
        //[Required(ErrorMessage = "CompanyName is required.")]
        //public string CompanyName { get; set; }
        //[Required(ErrorMessage = "Channel is required.")]
        //public string Channel { get; set; }
        [Required(ErrorMessage = "DNNumber is required.")]
        [StringLength(20, ErrorMessage = "DNNumber cannot be longer than 20 characters.")]
        public string DNNumber { get; set; }
        [Required(ErrorMessage = "AttachmentPath is required.")]
        public string AttachmentPath { get; set; }
        
        // Foreign Key
        [Required(ErrorMessage = "RequestFormId is required.")]
        public Guid RequestFormId { get; set; }
        // Navigation property
        [ForeignKey("RequestFormId")]
        public virtual Request RequestForm { get; set; }
    }
}
