using System.ComponentModel.DataAnnotations;

namespace server.DTOs
{
    public class CandidatoWithCategoriaDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string PhotoUrl { get; set; } = null!;

        [Required, MaxLength(200), MinLength(10)]
        public string Description { get; set; } = null!;
    }
}
