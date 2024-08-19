using BTL_Finance.Models;
using Data.Models;
using Repository.BaseService;

namespace Business.Services.NotificationService;

public interface INotificationService : IBaseService<Notification>
{
    public Task<List<Notification>> GetUserNotifications(string userId);
    public Task<Notification> GetRequestNotifications(string userId);
    public Task<bool> SendRealTimeNotifications(List<Notification> notifications, bool saveToDB = false);
}