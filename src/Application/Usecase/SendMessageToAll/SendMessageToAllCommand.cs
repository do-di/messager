using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Application.Usecase.SendMessageToAll
{
    public class SendMessageToAllCommand : IRequest
    {
        public string Message { get; set; }
    }
}
