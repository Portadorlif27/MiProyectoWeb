using Microsoft.AspNetCore.SignalR;

namespace FinanTech.Hubs;

public class NotificationHub : Hub
{
    public async Task Subscribe(string accountId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, accountId);
    }

    public async Task Unsubscribe(string accountId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, accountId);
    }
}
