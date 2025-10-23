using System;
using server.DTOs;
using server.Helpers.Interfaces;
using server.Repositories.Interfaces;
using server.Services.Interfaces;
using server.UnitOfWork;

namespace server.Services;

public class EleitorService : IEleitorService
{
    private readonly IGenerateConfirmationCode _codeGenerator;
    private readonly IEleitorRepository _eleitorRepository;

    private readonly SaveChages saveChages;
    public EleitorService(IGenerateConfirmationCode codeGenerator, IEleitorRepository eleitorRepository, SaveChages saveChages)
    {
        _codeGenerator = codeGenerator;
        _eleitorRepository = eleitorRepository;
        this.saveChages = saveChages;
    }

    public async Task<bool> ConfirmEleitorEmailAsync(string code)
    {
        var email = _codeGenerator.ValidateCode(code);
        if (email is null)
        {
            return false;
        }
        var eleitor = await _eleitorRepository.GetByEmailAsync(email);
        if (eleitor is null)
        {
            return false;
        }
        eleitor.IsEmailConfirmed = true;
        _eleitorRepository.UpdateAsync(eleitor);
        await saveChages.SaveChangesAsync();
        return true;
    }

    public async Task<string> CreateEleitorAsync(EleitorDTO eleitorDto)
    {
        var isCreated = await _eleitorRepository.IsCreatedAsync(eleitorDto.Email);
        if (isCreated)
        {
            await _codeGenerator.GenerateCode(eleitorDto.Email);
            return "Um código de confirmação foi enviado para o seu email.";
        }
        var eleitor = new Models.Eleitor
        {
            Nome = eleitorDto.Nome,
            Email = eleitorDto.Email,
            Role = "Eleitor",
            // Confirmed = false
        };
        await _eleitorRepository.AddAsync(eleitor);
        await _codeGenerator.GenerateCode(eleitorDto.Email);
        await saveChages.SaveChangesAsync();
        return "Confirme o seu email para finalizar o cadastro.";
    }
}
