using System;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using server.ApplicationDbContext;
namespace server.Hubs;

public class VotoHub : Hub
{
    private readonly AppDbContext _context;
    public VotoHub(AppDbContext context)
    {
        _context = context;
    }
    public async Task SendVoteQtdVotos()
    {
        var qtdVotos = await _context.Votos.CountAsync();
        await Clients.All.SendAsync("ReceiveVoteQtdVotos", qtdVotos);
    }
}
