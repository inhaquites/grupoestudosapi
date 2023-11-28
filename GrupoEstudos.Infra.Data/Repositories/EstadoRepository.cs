using GrupoEstudos.Domain.Entities;
using GrupoEstudos.Domain.Interfaces;
using GrupoEstudos.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoEstudos.Infra.Data.Repositories
{
    public class EstadoRepository : IEstadoRepository
    {
        private ApplicationDbContext _context;

        public EstadoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Estado> CreateAsync(Estado estado, CancellationToken cancellationToken)
        {
            using (var dbTrans = _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
            {
                try
                {
                    _context.Add(estado);
                    await _context.SaveChangesAsync(cancellationToken);
                    dbTrans.Commit();
                }
                catch
                {
                    dbTrans.Rollback();
                }
                return estado;
            }
        }

        public async Task<List<Estado>> GetEstadosAsync(CancellationToken cancellationToken)
        {
            var estados = await _context.Estados.AsNoTracking().ToListAsync(cancellationToken);

            return estados;
        }
    }
}
