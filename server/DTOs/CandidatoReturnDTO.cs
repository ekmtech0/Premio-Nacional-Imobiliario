using System.ComponentModel.DataAnnotations;

namespace server.DTOs
{
    public class CandidatoReturnDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string PhotoUrl { get; set; } = null!;
        public string Categoria { get; set; }
        public Guid CategoriaId { get; set;}
        [Required, MaxLength(200), MinLength(10)]
        public string Description { get; set; } = null!;
        public int Votos { get; set; }
    }
}
