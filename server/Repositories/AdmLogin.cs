using System;
using Microsoft.EntityFrameworkCore;
using server.ApplicationDbContext;
using server.DTOs;
using server.Helpers;
using server.Models;
using server.Repositories.Interfaces;
using server.Services.Interfaces;

namespace server.Repositories;

public class AdmLogin : IAdmLogin
{

    private readonly AppDbContext _context;
    private readonly ITokenService tokenService;
    public AdmLogin(AppDbContext context, ITokenService Tokenservice)
    {
        _context = context;
        tokenService = Tokenservice;
    }

    public async Task<(string, string)> LoginAsync(AdmLoginDTO model)
    {
        var adm = await _context.Adms.SingleOrDefaultAsync(a => a.Email == model.Email);
        if (adm is null || !HashHelper.VerifyHash(model.Senha, adm.Senha))
            return ("", "");

        //Gerando o Refresh Token 
        var refreshToken = tokenService.GenerateRefreshToken();
        var token = tokenService.GenerateToken(adm);

        return (token, refreshToken);
    }
    public async Task SaveRefreshTokenAsync(string email, RefreshToken refreshToken)
    {
        var adm = await _context.Adms
            .Include(a => a.RefreshTokens)
            .FirstOrDefaultAsync(a => a.Email == email);

        if (adm != null)
        {
            adm.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<string?> GetAccessTokenByRefreshTokenAsync(string refreshToken)
    {
        var adm = await _context.Adms
            .Include(a => a.RefreshTokens)
            .FirstOrDefaultAsync(a => a.RefreshTokens.Any(rt => rt.Token == refreshToken));

        if (adm == null)
            return null;

        var storedRefreshToken = adm.RefreshTokens
            .FirstOrDefault(rt => rt.Token == refreshToken);

        if (storedRefreshToken == null || storedRefreshToken.Expires < DateTime.UtcNow)
            return null;

        var newAccessToken = tokenService.GenerateToken(adm);
        return newAccessToken;
    }
}
