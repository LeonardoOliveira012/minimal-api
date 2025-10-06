using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal_api.Dominio.Entidades;
using minimal_api.DTOs;
using minimal_api.Migrations;

namespace minimal_api.Dominio.Interfaces
{
    public interface IAdministradorServicos
    {
        Adiministrador? Login(LoginDTO loginDTO);
    }
}