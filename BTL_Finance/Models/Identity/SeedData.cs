using Microsoft.AspNetCore.Identity;

namespace BTL_Finance.Models.Identity
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = { "Admin", "OrderUser", "POUser", "InvoiceUser" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            var adminUser = new ApplicationUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                EmailConfirmed = true
            };
            string adminPassword = "Admin@123";
            var user = await userManager.FindByEmailAsync(adminUser.Email);

            if (user == null)
            {
                var createPowerUser = await userManager.CreateAsync(adminUser, adminPassword);
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
            var OrderUser = new ApplicationUser
            {
                UserName = "Mahmoud@gmail.com",
                Email = "Mahmoud@gmail.com",
                EmailConfirmed = true
            };
            string OrderPassword = "Order@123";
            var Order_user = await userManager.FindByEmailAsync(OrderUser.Email);

            if (Order_user == null)
            {
                var createPowerUser = await userManager.CreateAsync(OrderUser, OrderPassword);
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(OrderUser, "OrderUser");
                }
            }
            var POUser = new ApplicationUser
            {
                UserName = "Omar@Omar.com",
                Email = "Omar@Omar.com",
                EmailConfirmed = true
            };
            string POPassword = "POQuote@123";
            var PO_user = await userManager.FindByEmailAsync(POUser.Email);

            if (PO_user == null)
            {
                var createPowerUser = await userManager.CreateAsync(POUser, POPassword);
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(POUser, "POUser");
                }
            }
            var InvoiceUser = new ApplicationUser
            {
                UserName = "Mostafa@Invoice.com",
                Email = "Mostafa@Invoice.com",
                EmailConfirmed = true
            };
            string InvoicePassword = "Invoice@123";
            var Invoice_user = await userManager.FindByEmailAsync(InvoiceUser.Email);

            if (Invoice_user == null)
            {
                var createInvoicewerUser = await userManager.CreateAsync(InvoiceUser, InvoicePassword);
                if (createInvoicewerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(InvoiceUser, "InvoiceUser");
                }
            }
        }
    }
}
