using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class Adm : User
    {
        [EmailAddress]
        public virtual string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
