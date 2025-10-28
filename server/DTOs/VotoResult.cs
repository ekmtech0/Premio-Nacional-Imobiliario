using System;

namespace server.DTOs;

public class VotoResult
{
    public bool Sucesso { get; set; }
    public string Mensagem { get; set; } = "";
    public VotoReturnDTO? Dados { get; set; }
}

