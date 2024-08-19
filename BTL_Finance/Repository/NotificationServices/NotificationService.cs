
using BTL_Finance.Models;
using BTL_Finance.Models.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository.BaseService;

namespace Business.Services.NotificationService;

public class NotificationService : BaseService<Notification>, INotificationService
{
    private readonly ApplicationDbContext dContext;
    public NotificationService(ApplicationDbContext dContext) : base(dContext)
    {   
        this.dContext = dContext;
    }
    public async Task<bool> SendRealTimeNotifications(List<Notification> notifications, bool saveToDB = false)
    {
        if (saveToDB)
        {
            dContext.Notifications.AddRange(notifications);
            await dContext.SaveChangesAsync();
        }

        throw new NotImplementedException();

        return true;
    }

    public async Task<List<Notification>> GetUserNotifications(string userId)
    {
        return await dContext.Notifications.Where(n => n.To == Guid.Parse(userId) || n.To == null).ToListAsync();
    }
    public async Task<Notification> GetRequestNotifications(string RequestId)
    {
        return await dContext.Notifications.Where(n => n.RequestId == Guid.Parse(RequestId)).FirstOrDefaultAsync();
    }
}