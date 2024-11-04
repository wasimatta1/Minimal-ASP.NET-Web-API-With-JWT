using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Minimal_ASP.NET_With_JWT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddAuthentication(k =>
            {
                k.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                k.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(p =>
            {
                var key = Encoding.UTF8.GetBytes(builder.Configuration["JWTToken:Key"]);
                p.SaveToken = true;
                p.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWTToken:Issuer"],
                    ValidAudience = builder.Configuration["JWTToken:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)

                };
            });




            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }

    }
}
