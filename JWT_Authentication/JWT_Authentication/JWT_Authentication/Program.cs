using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorizationBuilder();
    //.AddPolicy("premium-user", policy => policy
    //.RequireAuthenticatedUser()
    //.RequireClaim(CustomClaims.Subscription, 
    //allowedValues: [Subscription.Gold.ToString(),Subscription.Platinum.ToString()]));

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
