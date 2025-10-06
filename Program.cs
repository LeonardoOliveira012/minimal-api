using minimal_api.Infraestrutura.DB;
using Microsoft.EntityFrameworkCore;
using minimal_api.Dominio.Interfaces;
using minimal_api.Dominio.Servicos;
using Microsoft.AspNetCore.Mvc;
using minimal_api.DTOs;
using minimal_api.Dominio.ModelViews;
using minimal_api.Dominio.Entidades;



#region Builder

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddScoped<IAdministradorServicos, AdiministradorServico>();// Resolvendo a depedendcia
builder.Services.AddScoped<IVeiculoServico, VeiculoServico>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("mysql"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
    );
});

#endregion


#region AppBiuld
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

#endregion

#region Home
app.MapGet("/", () => Results.Json(new Home())).WithTags("Home");// Chamndo o struct com o swagger
#endregion


#region Administradores
//Data Trnsfer object
app.MapPost("/administradores/login", ([FromBody] LoginDTO loginDTO, IAdministradorServicos administradorServico) =>
{
    if (administradorServico.Login(loginDTO) != null)
    {
        return Results.Ok("Sucesso ao logar."); //verificando se pode logar
    }
    else
    {
        return Results.Unauthorized(); //falha ao logar
    }
}).WithTags("Adiministradores");
#endregion




#region Veiculos

ErrosDeValidacao validaDTO(VeiculoDTO veiculoDTO)
{
    var validacao = new ErrosDeValidacao
    {
        Mensagens = new List<string>()
    };

    //Validando os campos
    if (string.IsNullOrEmpty(veiculoDTO.Nome))
    {
        validacao.Mensagens.Add("O nome não pode ser vazio");
    }
    if (string.IsNullOrEmpty(veiculoDTO.Marca))
    {
        validacao.Mensagens.Add("O nome não pode ser vazio");
    }
    if (veiculoDTO.Ano < 1950)
    {
        validacao.Mensagens.Add("Veiculo muito antigo, só aceitamos superiores á 1950");
    }

    return validacao;
}

app.MapPost("/veiculos", ([FromBody] VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
{       

    var validacao = validaDTO(veiculoDTO);
        if (validacao.Mensagens.Count > 0)
    {
        return Results.BadRequest(validacao);
    }
        
    //Cadastro dos veiculos
    var veiculo = new minimal_api.Dominio.Entidades.Veiculo
    {
        Nome = veiculoDTO.Nome,
        Marca = veiculoDTO.Marca,
        Ano = veiculoDTO.Ano,

    };

    veiculoServico.Incluir(veiculo);

    return Results.Created($"/veiculo/{veiculo.Id}", veiculo);

}).WithTags("Veiculos");

app.MapGet("/veiculos", ([FromQuery] int? pagina, IVeiculoServico veiculoServico) =>
{

    var veiculos = veiculoServico.Todos(pagina);

    return Results.Ok(veiculos);

}
).WithTags("Veiculos");



app.MapGet("/veiculos/{id}", ([FromRoute] int id, IVeiculoServico veiculoServico) =>
{

    var veiculos = veiculoServico.BuscaPorId(id);

    if (veiculos == null) return Results.NotFound();

    return Results.Ok(veiculos);

}
).WithTags("Veiculos");

app.MapPut("/veiculos/{id}", ([FromRoute] int id, VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
{   
    var veiculos = veiculoServico.BuscaPorId(id);

    if (veiculos == null) return Results.NotFound();

     var validacao = validaDTO(veiculoDTO);
        if (validacao.Mensagens.Count > 0)
    {
        return Results.BadRequest(validacao);
    }

    

    veiculos.Nome = veiculoDTO.Nome;
    veiculos.Marca = veiculoDTO.Marca;
    veiculos.Ano = veiculoDTO.Ano;

    veiculoServico.Atualizar(veiculos);

    return Results.Ok(veiculos);

}
).WithTags("Veiculos");

app.MapDelete("/veiculos/{id}", ([FromRoute]int id ,IVeiculoServico veiculoServico) =>
{

    var veiculos = veiculoServico.BuscaPorId(id);

    if (veiculos == null) return Results.NotFound();


    veiculoServico.Apagar(veiculos);

    return Results.NoContent();

}
).WithTags("Veiculos");



#endregion

#region App
app.Run();

#endregion