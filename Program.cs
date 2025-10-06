using minimal_api.Infraestrutura.DB;
using Microsoft.EntityFrameworkCore;
using minimal_api.Dominio.Interfaces;
using minimal_api.Dominio.Servicos;
using Microsoft.AspNetCore.Mvc;
using minimal_api.DTOs;





var builder = WebApplication.CreateBuilder(args);



builder.Services.AddScoped<IAdministradorServicos, AdiministradorServico>();// Resolvendo a depedendcia


builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("mysql"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
    );
});


var app = builder.Build();


app.MapGet("/", () => "Hello World!");




//Data Trnsfer object
app.MapPost("/login", ([FromBody] LoginDTO loginDTO, IAdministradorServicos administradorServico) =>
{
    if (administradorServico.Login(loginDTO) != null)
    {
        return Results.Ok("Sucesso ao logar."); //verificando se pode logar
    }
    else
    {
        return Results.Unauthorized(); //falha ao logar
    }
});


app.Run();


