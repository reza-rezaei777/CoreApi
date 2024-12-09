using CoreApi.Domin;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CoreApi.WebFramework.Configuration
{
    public static class ServiceCollectionExtentions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, JWTSettings jWTSettings)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var secretkey = Encoding.UTF8.GetBytes(jWTSettings.SecretKey);
                var encrypttkey = Encoding.UTF8.GetBytes(jWTSettings.EncrypKey);

                var validationparameters = new TokenValidationParameters
                {
                    //تلورانس زمان توکن
                    ClockSkew = TimeSpan.Zero,
                    RequireSignedTokens = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretkey),
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ValidateAudience=true,
                    ValidAudience=jWTSettings.Audience,
                    ValidateIssuer=true,
                    ValidIssuer=jWTSettings.Issuer,
                    TokenDecryptionKey=new SymmetricSecurityKey(encrypttkey)
                


                };
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = validationparameters;
            });

        }
    }
}
