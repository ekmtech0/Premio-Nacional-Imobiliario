namespace server.DTOs
{
    public class VotoReturnDTO
    {
        public Guid Id { get; set; }
        public string Eleitor { get; set; } = null!;
        public string Candidato { get; set; } = null!;
        public string Categoria { get; set; } = null!;
    }
}
