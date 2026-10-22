using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Microsoft.AspNetCore.Http;

namespace SiteKit.Studio.Security;

/// <summary>
/// Restricts SiteKit Studio and management APIs to the local Studio administrator session.
/// </summary>
public sealed class SiteKitAdminAuthorizationFilter : IAsyncAuthorizationFilter
{
    private readonly IConfiguration _configuration;

    public SiteKitAdminAuthorizationFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (context.ActionDescriptor.EndpointMetadata.OfType<IAllowAnonymous>().Any())
            return Task.CompletedTask;
        var configuredAdmin = _configuration["SiteKit:AdminEmail"];
        var email = context.HttpContext.Session.GetString("SiteKitAdminEmail");
        var isAdministrator = !string.IsNullOrWhiteSpace(configuredAdmin) &&
            string.Equals(email, configuredAdmin, StringComparison.OrdinalIgnoreCase);

        if (!isAdministrator)
        {
            context.Result = context.HttpContext.Request.Path.StartsWithSegments("/api")
                ? new UnauthorizedObjectResult(new { error = "SiteKit administrator sign-in required." })
                : new RedirectResult("/login");
        }

        return Task.CompletedTask;
    }
}
