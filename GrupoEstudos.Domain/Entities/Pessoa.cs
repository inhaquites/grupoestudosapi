using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoEstudos.Domain.Entities
{
    public sealed class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? CPF { get; private set; }
        public DateTime DataNascimento { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }

        public List<Endereco> Enderecos { get; set; }
    }
}
