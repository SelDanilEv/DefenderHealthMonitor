using Microsoft.AspNetCore.Authorization;

namespace Defender.HealthMonitor.WebUI.Attributes;

public class AuthAttribute : AuthorizeAttribute
{
    public AuthAttribute(params string[] roles)
    {
        Roles = String.Join(",", roles);
    }
}

