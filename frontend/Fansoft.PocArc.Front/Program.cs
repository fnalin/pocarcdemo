using System.Security.Claims;
using Fansoft.PocArc.Front.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Configurar autenticação
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/";
        });
}
else
{
    builder.Services.AddAuthentication(Microsoft.AspNetCore.Server.IISIntegration.IISDefaults.AuthenticationScheme);
}

builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<TokenService>();
builder.Services.AddHttpClient<ApiService>();

var app = builder.Build();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

// SIMULAR LOGIN AUTOMÁTICO EM DESENVOLVIMENTO
if (app.Environment.IsDevelopment())
{
    app.Use(async (context, next) =>
    {
        if (!context.User.Identity.IsAuthenticated)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "dev.user@local"),
                new Claim(ClaimTypes.Email, "dev.user@local"),
                new Claim(ClaimTypes.Role, "User")
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        await next();
    });
}


app.MapDefaultControllerRoute();

await app.RunAsync();