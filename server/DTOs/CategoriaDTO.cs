using server.Models;
using System.ComponentModel.DataAnnotations;

namespace server.DTOs
{
    public class CategoriaDTO
    {
        public string Nome { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
    public class CategoriaReturnDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Description { get; set; } = null!;
        //public int NumeroVencedores { get; set; }
    }
}

