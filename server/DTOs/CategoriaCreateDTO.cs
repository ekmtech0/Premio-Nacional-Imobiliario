using server.Models;
using System.ComponentModel.DataAnnotations;

namespace server.DTOs
{
    public class CategoriaCreateDTO
    {
        public string Nome { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}

