using BTL_Finance.Models.Identity;
using Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BTL_Finance.Models.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
        {
        }
        public virtual DbSet<AuditTrail> AuditTrails { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public DbSet<OrderSheet> OrderSheets { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<DeliveryNote> DeliveryNotes { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
    }
}
