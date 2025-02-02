using Application.Interface.Service;
using MediatR;

namespace Application.Usecase.DisconnectSignalR
{
    public class DisconnectSignalRHandler : IRequestHandler<DisconnectSignalRCommand>
    {
        private readonly IRedisCacheService _redisCacheService;
        private readonly ICurrentUserService _currentUserService;

        public DisconnectSignalRHandler(IRedisCacheService redisCacheService, ICurrentUserService currentUserService)
        {
            _redisCacheService = redisCacheService;
            _currentUserService = currentUserService;
        }

        public async Task Handle(DisconnectSignalRCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.GetUserId();
            await _redisCacheService.RemoveConnectionIdAsync(userId, request.ConnectionId).ConfigureAwait(false);
        }
    }
}
