﻿namespace server.DTOs
{
    public class CategoriaReturnDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<CandidatoWithCategoriaDTO>? Candidatos { get; set; }
    }
}
