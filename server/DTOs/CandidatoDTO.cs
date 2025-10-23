using server.Models;
using System.ComponentModel.DataAnnotations;

namespace server.DTOs
{
    public class CandidatoDTO
    {
        public string Nome { get; set; } = null!;
        public string PhotoUrl { get; set; } = null!;
        public Guid CategoriaId { get; set; }

        [Required, MaxLength(200), MinLength(10)]
        public string Description { get; set; } = null!;
    }
}
