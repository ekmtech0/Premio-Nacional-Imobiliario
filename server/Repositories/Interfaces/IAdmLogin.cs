using System;
using server.DTOs;
using server.Models;

namespace server.Repositories.Interfaces;

public interface IAdmLogin
{
    Task<Adm> LoginAsync(AdmLoginDTO model);
    Task SaveRefreshTokenAsync(string email, RefreshToken refreshToken);
    Task<string?> GetAccessTokenByRefreshTokenAsync(string refreshToken);
}
