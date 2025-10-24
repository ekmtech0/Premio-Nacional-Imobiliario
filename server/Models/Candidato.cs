using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class Candidato : User
    {

        [Required]
        public string PhotoUrl { get; set; } = null!;
        // Cada candidato pertence a apenas uma categoria
        public Guid CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;

        // Lista de votos que o candidato recebeu
        public List<Voto> Votos { get; set; } = new();

        [Required, MaxLength(200), MinLength(10)]
        public string Description { get; set; } = null!;
    }
}
