using GrupoEstudos.Domain.Entities;
using System;

namespace GrupoEstudos.Domain.Entities
{
    public sealed class Usuario    
    {
        public Guid Id { get; set; }
        public DateTime DataCadastro { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; set; }
        public string Senha { get; set; }        
        public bool Ativo { get; set; }

    }

}
