using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace minimal_api.Dominio.ModelViews;

public struct Home
{

    public string Mensagem { get => "Bem vindo a APPI de veiculos - Minimal API"; }
    public string Documentacao { get =>"http://localhost:5189/swagger";}  //Adicionando o swwager a pagina
}