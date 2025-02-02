using Application.Interface.Service;
using MediatR;

namespace Application.Usecase.SendMessageToAll
{
    public class SendMessageToAllHandler : IRequestHandler<SendMessageToAllCommand>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IHubClientService _hubClientService;
        public SendMessageToAllHandler(ICurrentUserService currentUserService, IHubClientService hubClientService)
        {
            _currentUserService = currentUserService;
            _hubClientService = hubClientService;
        }

        public async Task Handle(SendMessageToAllCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.GetUserId();
            await _hubClientService.SendMessageToAllUser(userId, request.Message).ConfigureAwait(false);
        }
    }
}
