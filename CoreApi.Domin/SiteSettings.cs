namespace CoreApi.Domin
{
    public class SiteSettings
    {
        public string ElmahPath { get; set; }
        public JWTSettings JWTSettings { get; set; }

    }
    public class JWTSettings
    {
        public string SecretKey { get; set; }
        public string EncrypKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int NotBeforeMinutes { get; set; }
        public int ExpirationMinutes { get; set; }
    }
}
