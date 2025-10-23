using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class Eleitor : User
    {
        [Required]
        // Lista de votos que o eleitor fez
        public List<Voto> Votos { get; set; } = new();
        [EmailAddress]
        public virtual string Email { get; set; } = null!;
    }
}
