using GrupoEstudos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoEstudos.Domain.Interfaces
{
    public interface IEstadoRepository
    {
        Task<List<Estado>> GetEstadosAsync(CancellationToken cancellationToken);
        Task<Estado> CreateAsync(Estado estado, CancellationToken cancellationToken);
    }
}
