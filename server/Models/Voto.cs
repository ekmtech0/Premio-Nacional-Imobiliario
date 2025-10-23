using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class Voto
    {
        [Key]
        public Guid Id { get; set; }

        // Eleitor que fez o voto
        public Guid EleitorId { get; set; }
        public Eleitor Eleitor { get; set; } = null!;

        // Candidato que recebeu o voto
        public Guid CandidatoId { get; set; }
        public Candidato Candidato { get; set; } = null!;

        // Categoria do voto (para garantir "1 voto por categoria")
        public Guid CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;
    }
}
