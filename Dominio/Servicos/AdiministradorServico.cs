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
    public class AdiministradorServico : IAdministradorServicos
    {

        private readonly DbContexto _contexto;
        public AdiministradorServico(DbContexto contexto)
        {
            _contexto = contexto;
        }
        public Adiministrador? Login(LoginDTO loginDTO)
        {
            var adm = _contexto.Adiministradores.Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha).FirstOrDefault();// ou ele retorna ou anulla
            return adm;
        }
    }
}