using Application.Models.DTO;
using MediatR;

namespace Application.Usecase.Login
{
    public class LoginCommand : IRequest<LoginResponseDTO>
    {
        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
