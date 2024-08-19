using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs;

public class NotificationsHub : Hub
{
    public async Task SendNotification()
    {
        await Clients.All.SendAsync("NewNotification");
    }
}
