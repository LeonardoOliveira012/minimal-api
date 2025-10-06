using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using minimal_api.Dominio.Entidades;

namespace minimal_api.Infraestrutura.DB
{
    public class DbContexto : DbContext
    {

        private readonly IConfiguration _configuracaoAppSettings;

        public DbContexto(IConfiguration configuracaoAppSettings)
        {
            _configuracaoAppSettings = configuracaoAppSettings;
        }

        public DbSet<Adiministrador> Adiministradores { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adiministrador>().HasData(
                new Adiministrador
                {
                    Id = 1 ,
                    Email = "adiministrado@teste.com",
                    Senha = "123456",      //em um contexti real usar criptografia
                    Perfil = "Adm"

                }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var stringConexao = _configuracaoAppSettings.GetConnectionString("mysql")?.ToString();

            if (!optionsBuilder.IsConfigured)
            {

                if (!string.IsNullOrEmpty(stringConexao))
                {
                    optionsBuilder.UseMySql(stringConexao,
                    ServerVersion.AutoDetect(stringConexao));
                }


            }

        }
    }
}