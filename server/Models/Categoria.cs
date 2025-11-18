using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class Categoria
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Nome { get; set; } = null!;

        // Lista de candidatos desta categoria
        public List<Candidato> Candidatos { get; set; } = new();

        [Required, MaxLength(200), MinLength(10)]
        public string Description { get; set; } = null!;

        public bool SpecialCategory { get; set; } = false;
        //[Required]
        ////public int NumeroVencedores { get; set; }
    }
}
