using BookIT.WebApp.Application.Services;
using BookIT.WebApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BookIT.WebApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
            })
            .AddOpenIdConnect(options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority = "https://localhost:5005";
                options.ClientId = "mvc-client";
                options.ClientSecret = "secret";
                options.ResponseType = "code";
                options.SaveTokens = true;

                options.Scope.Add("roles");
                options.Scope.Add("booking_api");
                options.Scope.Add("profile");
                options.Scope.Add("openid");
                options.Scope.Add("booking_api");
                options.Scope.Add("bookit_api.write");


                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = ClaimTypes.Role
                };
                options.Events = new OpenIdConnectEvents
                {
                    OnTokenValidated = context =>
                    {
                        var identity = (ClaimsIdentity)context.Principal.Identity;
                        var roles = identity.FindAll("role").Select(c => c.Value);
                        foreach (var role in roles)
                        {
                            Console.WriteLine($"Role claim: {role}");
                        }
                        return Task.CompletedTask;
                    },
                    OnRedirectToIdentityProvider = context =>
                    {
                        context.ProtocolMessage.Prompt = "login";
                        return Task.CompletedTask;
                    }
                };

                options.CallbackPath = "/signin-oidc";
                options.SignedOutCallbackPath = "/signout-callback-oidc";
                options.SignedOutRedirectUri = "https://localhost:7179/";
            });
            services.AddTransient<AccessTokenHandler>();
            services.AddHttpClient("ApiGateway", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7099");
            })
            .AddHttpMessageHandler<AccessTokenHandler>();

            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IBookingService, BookingService>();


            services.AddHttpContextAccessor();
            return services;
        }
    }
}
