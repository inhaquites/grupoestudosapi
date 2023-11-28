using GrupoEstudos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoEstudos.Domain.Interfaces
{
    public interface IMunicipioRepository
    {
        Task<List<Municipio>> GetByUfAsync(string uf, CancellationToken cancellationToken);
        Task<Municipio> CreateAsync(Municipio municipio, CancellationToken cancellationToken);
    }
}
