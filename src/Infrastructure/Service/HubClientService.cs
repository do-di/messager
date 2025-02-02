using Application.Interface.Service;
using Infrastructure.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;

namespace Infrastructure.Service
{
    public class HubClientService : IHubClientService
    {
        private readonly IHubContext<ChatHub> _hubContext;
        public HubClientService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessageToAllUser(string userId, string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", userId, message).ConfigureAwait(false);
        }
    }
}
