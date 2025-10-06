using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using minimal_api.Dominio.Interfaces;
using minimal_api.DTOs;
using minimal_api.Infraestrutura.DB;
using minimal_api.Dominio.Entidades;

namespace minimal_api.Dominio.Servicos
{
    public class VeiculoServico : IVeiculoServico
    {

        private readonly DbContexto _contexto;
        public VeiculoServico(DbContexto contexto)
        {
            _contexto = contexto;
        }

        public void Apagar(Veiculo Veiculo)
        {
            _contexto.Veiculos.Remove(Veiculo);
            _contexto.SaveChanges();
        }

        public void Atualizar(Veiculo Veiculo)
        {
            _contexto.Veiculos.Update(Veiculo);
            _contexto.SaveChanges();
        }

        public Veiculo? BuscaPorId(int Id)
        {
            return _contexto.Veiculos.Where(v => v.Id == Id).FirstOrDefault();
        }

        public void Incluir(Veiculo Veiculo)
        {
            _contexto.Veiculos.Add(Veiculo);
            _contexto.SaveChanges();
        }

        public List<Veiculo> Todos(int? pagina = 1, string? nome = null, string? marca = null)
        {
            var query = _contexto.Veiculos.AsQueryable();
            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(v => EF.Functions.Like(v.Nome.ToLower(), $"%{nome.ToLower()}%"));
            }

            int itensPorPagina = 10;

            if (pagina != null)
            {
                 query = query.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina); //Aplicando paginação
            }
           
            return query.ToList();
        }
    }
}