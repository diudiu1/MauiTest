namespace MauiApi
{
    public class JWTConfig
    {
        public string? ValidIssuer { get; set; }
        public string? ValidAudience { get; set; }
        public string? IssuerSigningKey { get; set; }
        public long Expired { get; set; }
    }
}
