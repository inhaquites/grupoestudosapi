using Microsoft.EntityFrameworkCore;
using GrupoEstudos.Domain.Entities;
using GrupoEstudos.Domain.Interfaces;
using GrupoEstudos.Infra.Data.Context;

namespace GrupoEstudos.Infra.Data.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private ApplicationDbContext _context;

    public UsuarioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Authenticate(string email, string senha, CancellationToken cancellationToken)
    {
        var result = await _context.Usuarios.Where(x => x.Email.Equals(email) && x.Senha.Equals(senha)).FirstOrDefaultAsync();
        return result != null;
    }
    public async Task<Usuario> getUsuario(string email, string senha, CancellationToken cancellationToken)
    {
        var result = await _context.Usuarios.Where(x => x.Email.Equals(email) && x.Senha.Equals(senha) && x.Ativo)            
            .FirstOrDefaultAsync();
        return result;
    }
    


}
