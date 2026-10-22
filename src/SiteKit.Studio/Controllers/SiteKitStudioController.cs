using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiteKit.Studio.Security;

namespace SiteKit.Studio.Controllers;

[TypeFilter(typeof(SiteKitAdminAuthorizationFilter))]
public sealed class SiteKitStudioController : Controller
{
    [AllowAnonymous]
    [HttpGet("/login")]
    public IActionResult Login() => View("Login");

    [AllowAnonymous]
    [HttpPost("/login")]
    public IActionResult Login([FromForm] string email, [FromForm] string password, [FromServices] IConfiguration configuration)
    {
        var expectedEmail = configuration["SiteKit:AdminEmail"];
        var expectedPassword = configuration["SiteKit:AdminPassword"];
        if (string.IsNullOrWhiteSpace(expectedEmail) || string.IsNullOrEmpty(expectedPassword))
            return StatusCode(StatusCodes.Status503ServiceUnavailable, "Configure SiteKit:AdminEmail and SiteKit:AdminPassword before signing in.");
        var validEmail = string.Equals(email.Trim(), expectedEmail, StringComparison.OrdinalIgnoreCase);
        var expectedBytes = System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(expectedPassword));
        var suppliedBytes = System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(password ?? string.Empty));
        var validPassword = System.Security.Cryptography.CryptographicOperations.FixedTimeEquals(expectedBytes, suppliedBytes);
        System.Security.Cryptography.CryptographicOperations.ZeroMemory(expectedBytes);
        System.Security.Cryptography.CryptographicOperations.ZeroMemory(suppliedBytes);
        if (!validEmail || !validPassword)
        {
            ModelState.AddModelError(string.Empty, "Email or password is incorrect.");
            return View("Login");
        }
        HttpContext.Session.SetString("SiteKitAdminEmail", expectedEmail);
        return Redirect("/studio/projects");
    }

    [HttpPost("/logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return Redirect("/login");
    }

    [HttpGet("/studio/components")]
    public IActionResult Components() => View("Components");

    [HttpGet("/studio/projects")]
    public IActionResult Projects() => View("Projects");

    [HttpGet("/studio/screens")]
    public IActionResult Screens() => View("Screens");

    [HttpGet("/studio/modules")]
    public IActionResult Modules() => View("Modules");

    [HttpGet("/studio/imports")]
    public IActionResult Imports() => View("Imports");
}
