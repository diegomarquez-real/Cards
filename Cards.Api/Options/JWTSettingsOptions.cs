namespace Cards.Api.Options
{
    public class JWTSettingsOptions
    {
        public string Token { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationInMinutes { get; set; }
    }
}