namespace EmailDelivery.OAuth2.Google.v4
{
    internal class TokenDownload
    {
        public string? access_token { get; set; }
        public int? expires_in { get; set; }
        public string? refresh_token { get; set; }
        public string? scope { get; set; }
        public string? token_type { get; set; }
        public string? id_token { get; set; }
    }
}