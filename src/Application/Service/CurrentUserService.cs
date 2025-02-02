using Application.Interface.Service;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Service
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetUserId()
        {
            var user = _contextAccessor.HttpContext?.User;
            if (user == null)
            {
                return string.Empty;
            }
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId ?? string.Empty;
        }
    }
}
