using ChefHero.Web.Components;
using ChefHero.Web.Common.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebComponents();
builder.Services.AddWebAuthentication(builder.Configuration, builder.Environment);
builder.Services.AddWebPersistence(builder.Configuration);
builder.Services.AddUserSession();
builder.Services.AddChefHeroApiClients(builder.Configuration, builder.Environment);

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapWebAuthenticationEndpoints();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
