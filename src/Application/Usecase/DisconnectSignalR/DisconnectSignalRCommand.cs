using MediatR;

namespace Application.Usecase.DisconnectSignalR
{
    public class DisconnectSignalRCommand : IRequest
    {
        public string ConnectionId { get; set; }
    }
}
