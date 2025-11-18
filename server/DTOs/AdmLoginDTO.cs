using System;

namespace server.DTOs;

public class AdmLoginDTO
{
    public string Email { get; set; } = null!;
    public string Senha { get; set; } = null!;

    public bool RememberMe { get; set; }
    
}
