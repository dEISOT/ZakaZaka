namespace ZakaZaka.Models.Response
{
    public class AuthResponse
    {
        public AuthResponse(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
