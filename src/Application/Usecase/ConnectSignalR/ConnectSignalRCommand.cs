using MediatR;

namespace Application.Usecase.ConnectSignalR
{
    public class ConnectSignalRCommand : IRequest
    {
        public string ConnectionId { get; set; }
    }
}
