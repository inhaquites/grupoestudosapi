using GrupoEstudos.Application.DTOs.Usuario;

namespace GrupoEstudos.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO?> GetUsuario(string email, string senha, CancellationToken cancellationToken);
        
    }
}
