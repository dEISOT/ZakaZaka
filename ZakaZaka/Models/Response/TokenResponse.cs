namespace ZakaZaka.Models.Response
{
    public class TokenResponse
    {
        public TokenResponse()
        {
        }

        public TokenResponse(string token, string refreshToken)
        {
            Token = token;
            RefreshToken = refreshToken;
        }

        public string Token { get; set; }
        public string RefreshToken { get; set; }

    }
}
