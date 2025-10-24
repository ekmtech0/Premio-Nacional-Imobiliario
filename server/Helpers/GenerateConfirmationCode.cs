using System;
using Microsoft.Extensions.Caching.Memory;
using server.Helpers.Interfaces;

namespace server.Helpers;

public class GenerateConfirmationCode : IGenerateConfirmationCode
{
    private readonly IMemoryCache _cache;
    private readonly IEmailSender _emailSender;
    public GenerateConfirmationCode(IMemoryCache cache, IEmailSender emailSender)
    {
        _cache = cache;
        _emailSender = emailSender;
    }
    public async Task<bool> GenerateCode(string email)
    {
        try
        {
            var code = Guid.NewGuid().ToString()[0..6].ToUpper();
            _cache.Set(code, email, TimeSpan.FromMinutes(15));
            await _emailSender.SendEmailAsync(email, "Código de Confirmação", code);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao gerar o código de confirmação: " + ex.Message);
            return false;
        }
    }

    public string? ValidateCode(string code)
    {
        if (_cache.TryGetValue(code, out string? email))
        {
            _cache.Remove(code);
            return email;
        }
        return null;
    }
}
