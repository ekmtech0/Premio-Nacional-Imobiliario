using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using server.ApplicationDbContext;
using server.DTOs;
using server.Helpers;
using server.Models;
using server.Repositories.Interfaces;
using System;
using System.Security.Claims;

namespace server.Endpoints;

public static class AdmEndpoints
{
    public static void MapAdmEndpoints(this IEndpointRouteBuilder app)
    {
        var routes = app.MapGroup("api/adm")
            .WithDescription("Endpoint de gerenciamente dos Adms")
            .WithTags("ADMs");

        routes.MapPost("login", async (IAdmLogin admLogin,AdmLoginDTO model, HttpContext ctx) =>
        {
            var adm = await admLogin.LoginAsync(model);
            if (adm is null)
                return Results.Unauthorized();
            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, adm.Id.ToString()),
                new (ClaimTypes.Name, adm.Nome),
                new (ClaimTypes.Email, adm.Email)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var props = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
            };

            await ctx.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);

            return Results.Ok(new { message = "Autenticado" });
        });

        routes.MapGet("me", (HttpContext ctx) =>
        {
            var claims = ctx.User.Claims.ToList();

            // Exemplos de claims comuns
            var userId = ctx.User.FindFirst("sub")?.Value; // ou ClaimTypes.NameIdentifier
            var userName = ctx.User.Identity?.Name;        // geralmente vem do ClaimTypes.Name
            var email = ctx.User.FindFirst("email")?.Value;

            return Results.Ok(new
            {
                Id = userId,
                Nome = userName,
                Email = email,
                TodasClaims = claims.Select(c => new { c.Type, c.Value })
            });
        }).RequireAuthorization();

        // routes.MapPost("addAdm", async (AppDbContext _context) =>
        // {
        //     await _context.AddAsync(new Adm
        //     {
        //         Email = "adm@linearcomunicacoes.com",
        //         Nome = "ADM1",
        //         Role = "adm",
        //         Senha = HashHelper.Hash("linearAdm123")
        //     });
        //     await _context.SaveChangesAsync();
       // });


        //routes.MapGet("refresh",async (HttpRequest request, IAdmLogin admLogin) =>
        //{
        //    // Pega o cookie "MeuCookie" enviado pelo navegador
        //    if (request.Cookies.TryGetValue("RefreshToken", out var valor))
        //    {
        //        var newAccessToken = await admLogin.GetAccessTokenByRefreshTokenAsync(valor);
        //        return Results.Ok(new { newAccessToken });
        //    }
            
        //    return Results.NotFound("Faça Login");
        //});

    }
}
