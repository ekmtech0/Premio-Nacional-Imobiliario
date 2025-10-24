namespace server.DTOs
{
    public class EleitorWithVotosDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<VotoDTO> Votos { get; set; } = new();
    }
}
