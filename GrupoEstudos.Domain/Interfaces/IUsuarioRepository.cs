using GrupoEstudos.Domain.Entities;

namespace GrupoEstudos.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<bool> Authenticate(string email, string password, CancellationToken cancellationToken);
    Task<Usuario> getUsuario(string email, string password, CancellationToken cancellationToken);
    
}
