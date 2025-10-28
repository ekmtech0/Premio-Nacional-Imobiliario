namespace server.DTOs
{
    public class VotoReturnDTO
    {
        public Guid Id { get; set; }
        public string BrowserId { get; set; } = null!;
        public Guid CandidatoId { get; set; }
        public Guid CategoriaId { get; set; }
        public DateTime DataVoto { get; set; }
    }
}
