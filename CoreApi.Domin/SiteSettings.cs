namespace CoreApi.Domin
{
    public class SiteSettings
    {
        public string ElmahPath { get; set; }
        public JWTSettings JWTSettings { get; set; }
        public IdentitySettings IdentitySettings { get; set; }

    }
    public class IdentitySettings
    {
        public bool PasswordRequireDigit { get; set; }
        public int PasswordRequiredLength { get; set; }
        public bool PasswordRequireNonAlphanumeric { get; set; }
        public bool PasswordRequireLowercase { get; set; }
        public bool PasswordRequireUppercase { get; set; }
        public bool UserRequireUniqueEmail { get; set; }
        public bool SignInConfirmedPhoneNumber { get; set; }
        public bool SignInRequireConfirmedEmail { get; set; }
        public byte LockoutMaxFailedAccessAttempts { get; set; }
        public byte LockoutDefaultLockoutTimeSpan { get; set; }
        public bool LockoutAllowedForNewUsers { get; set; }
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
