using Microsoft.AspNetCore.Authentication;

namespace CustomAuthApi.Auth;

public class CustomAuthenticationOptions : AuthenticationSchemeOptions
{
    public const string SchemeName = "MyCustomScheme";
    public string HeaderName { get; set; } = "Authorization";
    
    // instead of this value you should validate the session
    public const string FakeSessionValue = "my-custom-auth-header";
}