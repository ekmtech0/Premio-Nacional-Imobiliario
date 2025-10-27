using System;

namespace server.DTOs;

public class CategoriaWithNoUserDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;

    public int QtdCandidatos { get; set; }
}
