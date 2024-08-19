using BTL_Finance.Models.GenericModels;
using System.ComponentModel.DataAnnotations;

namespace BTL_Finance.Models
{
    public class Request: BaseEntity
    {
        public string? Request_Name { get; set; }
        [Required(ErrorMessage = "process is required.")]
        public int process { set; get; }
        [Required(ErrorMessage = "Type is required.")]
        public string Type { get; set; }
        [Required(ErrorMessage = "ClientName is required.")]
        public string ClientName { get; set; }
        [Required(ErrorMessage = "CompanyName is required.")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mobile is required.")]

        public int Mobile { get; set; }
        [Required(ErrorMessage = "Channel is required.")]
        public string Channel { get; set; }
        [Required(ErrorMessage = "AttachmentPath is required.")]
        public string AttachmentPath { get; set; }

        public bool ?Status_Request { get; set; } = false;
        public bool ?Status_order_sheet { get; set; } = false;
        public bool? Status_Delivery_Note { get; set; } = false;
        public bool ?Status_PO { get; set; } = false;
        public bool ?Status_Quote { get; set; } = false;
        public bool ?Status_Invoice { get; set; } = false;





        public virtual ICollection<OrderSheet>? OrderSheets { get; set; }
        public virtual ICollection<Quote>? Quotes { get; set; }
        public virtual ICollection<PurchaseOrder>? PurchaseOrders { get; set; }
        public virtual ICollection<DeliveryNote>? DeliveryNotes { get; set; }
        public virtual ICollection<Invoice>? Invoices { get; set; }
    }
}
