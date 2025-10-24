namespace server.DTOs
{
    public class VotoDTO
    {
        public Guid Id { get; set; }
        public string Candidato { get; set; } = null!;
        public string Categoria { get; set; } = null!;
    }
}
