using Microsoft.EntityFrameworkCore;
using server.ApplicationDbContext;
using server.Extensions;
using server.Services;
using server.Services.Interfaces;
using server.Models;
using server.UnitOfWork;
using server.Endpoints;
using server.Repositories.Interfaces;
using server.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerAuth();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthenticationSuport();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<SaveChages>();
builder.Services.AddScoped<ICandidatoRepository, CandidatoRepository>();
builder.Services.AddScoped<ICandidatoService, CandidatoService>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseGlobalExceptionHandler();

//app.MapControllers();

app.MapGet("/", () => "Hello World!");
app.MapCandidatoEndpoints();
app.MapCategoriaEndpoints();

app.Run();
