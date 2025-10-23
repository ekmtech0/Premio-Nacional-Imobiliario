using System;
using server.DTOs;

namespace server.Services.Interfaces;

public interface IEleitorService
{
    Task<string> CreateEleitorAsync(EleitorDTO eleitorDto);

    Task<bool> ConfirmEleitorEmailAsync(string code);
}
