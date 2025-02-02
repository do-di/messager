using Application.Usecase.ConnectSignalR;
using Application.Usecase.SendMessageToAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            var command = new ConnectSignalRCommand
            {
                ConnectionId = connectionId,
            };
            await _mediator.Send(command).ConfigureAwait(false);
            await base.OnConnectedAsync();
        }

        public async Task SendMessage(string message)
        {
            var command = new SendMessageToAllCommand
            {
                Message = message
            };
            await _mediator.Send(command).ConfigureAwait(false);
        }
    }
}
