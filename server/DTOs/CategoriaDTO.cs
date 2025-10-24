namespace server.DTOs
{
    public class CategoriaDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
