namespace Application.Interface.Service
{
    public interface IHubClientService
    {
        Task SendMessageToAllUser(string userId, string message);
    }
}
