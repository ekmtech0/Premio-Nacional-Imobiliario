using System;

namespace server.DTOs;

public class VotosPorCategoria
{
    public Guid CategoriaId { get; set; }
    public string Categoria { get; set; } = null!;
    public string Description { get; set; }
    public int QtdVotos{ get; set; }
}
