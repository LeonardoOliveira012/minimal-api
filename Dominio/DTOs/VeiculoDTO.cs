using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace minimal_api.DTOs
{
    public record VeiculoDTO //Instancia menor que uma classe
    {
        
        public int Id { get; set; } = default!;

        
        
        public string Nome { get; set; } = default!;

        
    
        public string Marca { get; set; } = default!; 

        
        public int Ano { get; set; } = default!; 
    }
    
} 
