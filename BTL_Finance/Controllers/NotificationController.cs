using BTL_Finance.Models.Identity;
using Business.Services.NotificationService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BTL_Finance.Controllers
{
    public class NotificationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotificationService _notificationService;

        public NotificationController(UserManager<ApplicationUser> userManager, INotificationService notificationService)
        {
            _userManager = userManager;
            _notificationService = notificationService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }

            var notifications = await _notificationService.GetUserNotifications(user.Id);
            return View(notifications);
        }
    }
}
