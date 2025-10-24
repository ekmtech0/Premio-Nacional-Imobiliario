using server.Models;

namespace server.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        string GenerateRefreshToken(); 
    }
}
