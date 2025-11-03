using System;
using server.ApplicationDbContext;
using server.DTOs;
using server.Helpers;
using server.Models;
using server.Repositories.Interfaces;

namespace server.Endpoints;

public static class AdmEndpoints
{
    public static void MapAdmEndpoints(this IEndpointRouteBuilder app)
    {
        var routes = app.MapGroup("adm")
            .WithDescription("Endpoint de gerenciamente dos Adms")
            .WithTags("ADMs");

        routes.MapPost("login", async (IAdmLogin admLogin, HttpResponse resp, AdmLoginDTO model) =>
        {
            var (token, refreshToken) = await admLogin.LoginAsync(model);
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(refreshToken))
                return Results.NotFound("Credenciais inválidas");

            var rt = new RefreshToken
            {
                Token = refreshToken,
                Expires = DateTime.UtcNow.AddDays(5),
                CreatedAt = DateTime.UtcNow
            };
            await admLogin.SaveRefreshTokenAsync(model.Email, rt);
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(5),
                SameSite = SameSiteMode.None,
                HttpOnly = true,
                Secure = true,
            };

            resp.Cookies.Append("RefreshToken", refreshToken, cookieOptions);
            return Results.Ok( new {token});
        });
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
        routes.MapGet("refresh",async (HttpRequest request, IAdmLogin admLogin) =>
        {
            // Pega o cookie "MeuCookie" enviado pelo navegador
            if (request.Cookies.TryGetValue("RefreshToken", out var valor))
            {
                var newAccessToken = await admLogin.GetAccessTokenByRefreshTokenAsync(valor);
                return Results.Ok(new { newAccessToken });
            }
            
            return Results.NotFound("Faça Login");
        });

    }
}
