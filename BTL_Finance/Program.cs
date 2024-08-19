using BTL_Finance.Models.Context;
using BTL_Finance.Models.Identity;
using BTL_Finance.Repository.DeliveryNoteServices;
using BTL_Finance.Repository.InvoiceServices;
using BTL_Finance.Repository.OrderSheetServices;
using BTL_Finance.Repository.PurchaseOrderServices;
using BTL_Finance.Repository.QuoteServices;
using BTL_Finance.Repository.RequestServices;
using Business.Services.AuditTrailService;
using Business.Services.NotificationService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BTL_Finance
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IAuditTrailService, AuditTrailService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IRequestService, RequestService>();
            builder.Services.AddScoped<IDeliveryNoteService, DeliveryNoteService>();
            builder.Services.AddScoped<IInvoiceService, InvoiceService>();
            builder.Services.AddScoped<IOrderSheetService, OrderSheetService>();
            builder.Services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
            builder.Services.AddScoped<IQuoteService, QuoteService>();
            


            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication();
            var app = builder.Build();

            
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Users}/{action=login}/{id?}");
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                await SeedData.Initialize(services, userManager);
            }

            app.Run();
        }
    }
}