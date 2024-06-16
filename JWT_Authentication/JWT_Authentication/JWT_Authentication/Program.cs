using JWT_Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Admin", policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();

        policy.Requirements.Add(new AdminRequirement());
    })
     .AddPolicy("AtLeast21", policy =>
        policy.Requirements.Add(new MinimumAgeRequirement(21))); ;

builder.Services.AddSingleton<IAuthorizationHandler,AdminRequirementAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

//builder.Services.AddScoped<UserSubscriptionProvider>();
//builder.Services.AddTransient<IClaimsTransformation, CustomClaimTransformation>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/hello", (ClaimsPrincipal user) =>
{
    return $"Hello {user.Identity?.Name}";
}).RequireAuthorization();

app.MapGet("/policy2", (ClaimsPrincipal user) =>
{
    return "Successfulyy entered ";
}).RequireAuthorization("AtLeast21");

app.MapGet("/policy", () =>
{
    return "Successfulyy entered ";
}).RequireAuthorization("Admin");

app.Run();

//public static class CustomClaims
//{
//    public const string Subscription = "subscription";
//}

//public enum Subscription
//{
//    Standart = 0,
//    Gold = 1,
//    Platinum = 2
//}

//public class UserSubscriptionProvider
//{
//    public Subscription GetSubscription(string? userId)
//    {
//        if (userId == null)
//        {
//            return Subscription.Standart;
//        }

//        return Subscription.Gold;
//    }
//}

//public class CustomClaimTransformation(IServiceScopeFactory serviceScopeFactory) : IClaimsTransformation
//{
//    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
//    {
//        if (principal.HasClaim(p => p.Type == CustomClaims.Subscription))
//        {
//            return Task.FromResult(principal);
//        }

//        using var scope = serviceScopeFactory.CreateScope();

//        var subscription = scope.ServiceProvider
//            .GetRequiredService<UserSubscriptionProvider>();

//        var identity = new ClaimsIdentity();

//        identity.AddClaim(new Claim(CustomClaims.Subscription, subscription.ToString()));

//        principal.AddIdentity(identity);

//        return Task.FromResult(principal);
//    }
//}
