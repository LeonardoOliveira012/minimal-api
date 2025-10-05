using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace minimal_api.DTOs
{
    public class LoginDTO
    {


        public string Email { get; set; } = default!; //Declarando que a propriedade não pode ser nula
        public string Senha { get; set; } = default!;//Declarando que a propriedade não pode ser nula
    }
    
} 
