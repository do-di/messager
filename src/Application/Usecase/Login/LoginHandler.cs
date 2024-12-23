using Application.Models.Common;
using Application.Models.DTO;
using Microsoft.AspNetCore.Http;
using MediatR;
using Application.Common;
using Microsoft.Extensions.Options;
using Application.Models.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Usecase.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponseDTO>
    {
        private readonly IOptions<JwtSettings> _jwtSetting;

        public ICollection<string> ListUser = ["admin", "customer"];

        public LoginHandler(IOptions<JwtSettings> jwtSetting)
        {
            _jwtSetting = jwtSetting;
        }
        public async Task<LoginResponseDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if(!ListUser.Contains(request.UserName))
            {
                throw new BussinessException(StatusCodes.Status400BadRequest, ErrorCode.UsernameOrPassworkIncorrect, ErrorMessage.UsernameOrPassworkIncorrect);
            }
            var tokenClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, request.UserName),
            };
            var token = GenerateToken(tokenClaims);

            var response =  new LoginResponseDTO
            {
                AccessToken = token,
                RefreshToken = "refresh token"
            };
            return await Task.FromResult(response);
        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Value.Secret));
            var tokenExpiryTimeHour = Convert.ToDouble(_jwtSetting.Value.TokenExpiryTimeHour);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtSetting.Value.Issuer,
                Audience = _jwtSetting.Value.Audience,
                Expires = DateTime.UtcNow.AddHours(tokenExpiryTimeHour),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
