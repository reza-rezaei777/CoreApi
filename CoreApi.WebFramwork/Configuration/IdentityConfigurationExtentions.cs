using CoreApi.DataLayer;
using CoreApi.Domin;
using CoreApi.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CoreApi.WebFramework.Configuration
{
    public static class IdentityConfigurationExtentions
    {
        public static void AddCustomIdentity(this IServiceCollection services, IdentitySettings settings)
        {
            services.AddIdentity<User, Role>(identityoptions =>
            {
                //Password Settings
                identityoptions.Password.RequireDigit = settings.PasswordRequireDigit;
                identityoptions.Password.RequiredLength = settings.PasswordRequiredLength;
                identityoptions.Password.RequireNonAlphanumeric = settings.PasswordRequireNonAlphanumeric;//$@!
                identityoptions.Password.RequireLowercase = settings.PasswordRequireLowercase;
                identityoptions.Password.RequireUppercase = settings.PasswordRequireUppercase;
                //User Settings
                identityoptions.User.RequireUniqueEmail = settings.UserRequireUniqueEmail;
                
                //Signin Settings
                identityoptions.SignIn.RequireConfirmedPhoneNumber = settings.SignInConfirmedPhoneNumber;
                identityoptions.SignIn.RequireConfirmedEmail = settings.SignInRequireConfirmedEmail;

                //Lockout Settings
                identityoptions.Lockout.MaxFailedAccessAttempts = settings.LockoutMaxFailedAccessAttempts;
                identityoptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(settings.LockoutDefaultLockoutTimeSpan);
                identityoptions.Lockout.AllowedForNewUsers = settings.LockoutAllowedForNewUsers;//Active lock for new user
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
        }
    }
}
