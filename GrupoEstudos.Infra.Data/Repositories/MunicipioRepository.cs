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
    public class MunicipioRepository : IMunicipioRepository
    {
        private ApplicationDbContext _context;

        public MunicipioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Municipio> CreateAsync(Municipio municipio, CancellationToken cancellationToken)
        {
            using (var dbTrans = _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
            {
                try
                {
                    _context.Add(municipio);
                    await _context.SaveChangesAsync(cancellationToken);
                    dbTrans.Commit();
                }
                catch
                {
                    dbTrans.Rollback();
                }
                return municipio;
            }
        }

        public async Task<List<Municipio>> GetByUfAsync(string uf, CancellationToken cancellationToken)
        {
            var municipios = await _context.Municipios
                                           .AsNoTracking()
                                           .Where(x=>x.Estado.Sigla.Equals(uf))
                                           .ToListAsync(cancellationToken);

            return municipios;
        }
    }
}
