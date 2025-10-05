var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//Data Trnsfer object
app.MapPost("/login", (minimal_api.DTOs.LoginDTO LoginDTO) =>
{
    if (LoginDTO.Email == "adm@teste.com" && LoginDTO.Senha == "123456")
    {
        return Results.Ok("Sucesso ao logar."); //verificando se pode logar
    }
    else
    {
        return Results.Unauthorized(); //falha ao logar
    }
});


app.Run();


