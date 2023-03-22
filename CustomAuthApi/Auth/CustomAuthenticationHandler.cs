using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace CustomAuthApi.Auth;

public class CustomAuthenticationHandler : AuthenticationHandler<CustomAuthenticationOptions>
{
    public CustomAuthenticationHandler(IOptionsMonitor<CustomAuthenticationOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {

        if (!Request.Headers.TryGetValue("Authorization", out var authHeader))
            return Task.FromResult(AuthenticateResult.Fail("Authorization header not found."));
        
        // instead of this check you should validate the session here
        if (authHeader != CustomAuthenticationOptions.FakeSessionValue)
            return Task.FromResult(AuthenticateResult.Fail("Invalid authorization header."));
        
        
        var claims = new List<Claim>()
        {
            new (ClaimTypes.Name, "user123"),
        };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}