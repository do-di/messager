using Application.Interface.Service;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Application.Usecase.ConnectSignalR
{
    public class ConnectSignalRHandler : IRequestHandler<ConnectSignalRCommand>
    {
        private readonly IHubClientService _hubClientService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IRedisCacheService _redisCacheService;

        public ConnectSignalRHandler(IHubClientService hubClientService, ICurrentUserService currentUserService, IRedisCacheService redisCacheService)
        {
            _hubClientService = hubClientService;
            _currentUserService = currentUserService;
            _redisCacheService = redisCacheService;
        }

        public async Task Handle(ConnectSignalRCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.GetUserId();
            await _redisCacheService.AddConnectionIdAsync(userId, request.ConnectionId).ConfigureAwait(false);      
        }
    }
}
