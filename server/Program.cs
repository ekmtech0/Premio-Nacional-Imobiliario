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
using server.Helpers.Interfaces;
using server.Helpers;
using server.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthenticationSuport();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5173",                       // ambiente local (Vite)
            "https://premio-nacional-imobiliario.netlify.app", // domínio Netlify
            "https://ekmtech0.github.io"                     // domínio GitHub Pages
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials(); // necessário para SignalR
    });
});


builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<SaveChages>();
builder.Services.AddScoped<ICandidatoRepository, CandidatoRepository>();
builder.Services.AddScoped<ICandidatoService, CandidatoService>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IGenerateConfirmationCode, GenerateConfirmationCode>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IVotoRepository, VotoRepository>();
builder.Services.AddScoped<IVotoService, VotoService>();
builder.Services.AddScoped<IAdmLogin, AdmLogin>();
builder.Services.AddSignalR();

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
app.UseCors("AllowFrontend");


app.MapCandidatoEndpoints();
app.MapCategoriaEndpoints();
app.MapVotoEndpoints();
app.MapAdmEndpoints();
app.MapHub<VotoHub>("/votohub");

app.UseDefaultFiles();
app.UseStaticFiles(); 
app.MapFallbackToFile("index.html");

app.Run();
