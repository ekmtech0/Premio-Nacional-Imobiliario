using System.ComponentModel.DataAnnotations;
using server.Models;

namespace server.DTOs
{
    public class VotoCreateDTO
    {
        // Eleitor que fez o voto
        public Guid EleitorId { get; set; }

        // Candidato que recebeu o voto
        public Guid CandidatoId { get; set; }

        // Categoria do voto (para garantir "1 voto por categoria")
        public Guid CategoriaId { get; set; }
    }
}
