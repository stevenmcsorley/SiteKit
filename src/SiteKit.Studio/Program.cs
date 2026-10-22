using SiteKit.Core.Components;
using SiteKit.Core.Data;
using SiteKit.Core.Importing;
using SiteKit.Core.Modules;
using SiteKit.Core.Projects;
using SiteKit.Studio.Security;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("SiteKitImportSource", client => client.Timeout = TimeSpan.FromSeconds(60));
builder.Services.AddScoped<SiteKitDataImportService>();

var configuredDataRoot = builder.Configuration["SiteKit:DataPath"];
var dataRoot = Path.GetFullPath(configuredDataRoot ?? Path.Combine(builder.Environment.ContentRootPath, "data", "sitekit"));
Directory.CreateDirectory(dataRoot);

builder.Services.AddSingleton<ISiteKitComponentRegistry>(_ => new JsonSiteKitComponentRegistry(Path.Combine(dataRoot, "components")));
builder.Services.AddSingleton<ISiteKitProjectRegistry>(_ => new JsonSiteKitProjectRegistry(Path.Combine(dataRoot, "projects")));
builder.Services.AddSingleton<ISiteKitPlacementRegistry>(_ => new JsonSiteKitPlacementRegistry(Path.Combine(dataRoot, "placements")));
builder.Services.AddSingleton<ISiteKitEntityRegistry>(_ => new JsonSiteKitEntityRegistry(Path.Combine(dataRoot, "entities")));
builder.Services.AddSingleton<ISiteKitRecordStore>(_ => new JsonSiteKitRecordStore(Path.Combine(dataRoot, "records")));
builder.Services.AddSingleton<ISiteKitPermissionRegistry>(_ => new JsonSiteKitPermissionRegistry(Path.Combine(dataRoot, "permissions")));
builder.Services.AddSingleton<ISiteKitProjectRoleRegistry>(_ => new JsonSiteKitProjectRoleRegistry(Path.Combine(dataRoot, "roles")));
builder.Services.AddSingleton<SiteKitPermissionEvaluator>();
builder.Services.AddSingleton<ISiteKitScreenRegistry>(_ => new JsonSiteKitScreenRegistry(Path.Combine(dataRoot, "screens")));
builder.Services.AddSingleton<ISiteKitModuleRegistry>(_ => new JsonSiteKitModuleRegistry(Path.Combine(dataRoot, "modules")));
builder.Services.AddSingleton<ISiteKitImportMappingRegistry>(_ => new JsonSiteKitImportMappingRegistry(Path.Combine(dataRoot, "imports")));
builder.Services.AddSingleton<ISiteKitImportRunRegistry>(_ => new JsonSiteKitImportRunRegistry(Path.Combine(dataRoot, "import-runs")));
builder.Services.AddSingleton<SiteKitAdminAuthorizationFilter>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.Cookie.SameSite = SameSiteMode.Strict;
});
builder.Services.AddDistributedMemoryCache();

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddComposers()
    .Build();

var app = builder.Build();
await app.BootUmbracoAsync();
if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/error");
app.UseSession();
app.UseUmbraco()
    .WithMiddleware(umbraco => { umbraco.UseBackOffice(); umbraco.UseWebsite(); })
    .WithEndpoints(umbraco =>
    {
        umbraco.UseBackOfficeEndpoints();
        umbraco.EndpointRouteBuilder.MapControllers();
        umbraco.UseWebsiteEndpoints();
    });
app.MapGet("/health", () => Results.Ok(new { status = "ok", product = "SiteKit" }));
app.Run();

public partial class Program;
