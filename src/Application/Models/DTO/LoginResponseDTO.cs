namespace Application.Models.DTO
{
    public class LoginResponseDTO
    {
        // The access token.
        public string AccessToken { get; set; } = string.Empty;

        // The Refresh Token
        public string RefreshToken { get; set; } = string.Empty;
    }
}
