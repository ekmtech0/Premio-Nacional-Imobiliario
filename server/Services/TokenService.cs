using Microsoft.IdentityModel.Tokens;
using server.Models;
using server.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace server.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateRefreshToken()
        {
            // Gera 64 bytes criptograficamente seguros e retorna em Base64 URL-safe
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            var refreshToken = Convert.ToBase64String(randomNumber)
                                    .Replace("+", "-")
                                    .Replace("/", "_")
                                    .TrimEnd('=');
            return refreshToken;
        }

        public string GenerateToken(User user)
        {
            var chave = "wedejdn983843ybrh21671tegbhd65627121bue12y6t8712ebg1y5e6387hb1budvt1563";
            // 1. Definir claims (informações do usuário dentro do token)
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Nome ?? ""),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
            };

            // 2. Criar chave e credenciais
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chave));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 3. Criar token
            var token = new JwtSecurityToken(
                issuer: "me",
                audience: "you",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: creds
            );

            // 4. Retornar token em string

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
