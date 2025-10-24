using System;

namespace server.Helpers.Interfaces;

public interface IGenerateConfirmationCode
{
    Task<bool> GenerateCode(string email);

    string? ValidateCode(string code);
}
