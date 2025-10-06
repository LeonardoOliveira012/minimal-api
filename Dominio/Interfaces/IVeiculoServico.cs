using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal_api.Dominio.Entidades;
using minimal_api.DTOs;
using minimal_api.Migrations;

namespace minimal_api.Dominio.Interfaces
{
    public interface IVeiculoServico
    {
        List<Veiculo> Todos(int? pagina = 1, string? nome = null, string? marca = null);

        Veiculo? BuscaPorId(int Id);

        void Incluir(Veiculo Veiculo);

        void Atualizar(Veiculo Veiculo);

        void Apagar(Veiculo Veiculo); //Passando o objeto diretamenete

    }
}