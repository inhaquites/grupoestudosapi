using AutoMapper;
using GrupoEstudos.Application.DTOs.Usuario;
using GrupoEstudos.Application.Interfaces;
using GrupoEstudos.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace GrupoEstudos.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;

    public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
    {
        _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        _mapper = mapper;
    }
    
    public async Task<UsuarioDTO?> GetUsuario(string email, string senha, CancellationToken cancellationToken)
    {
        try
        {
            var usuario = await _usuarioRepository.getUsuario(email, senha, cancellationToken);

            if (usuario == null)
            {
                return null;
            }            

            return new UsuarioDTO
            {
                Id = usuario.Id.ToString(),
                Email = usuario.Email,
                Nome = usuario.Nome,
                //Permissoes = tt
            };
        }
        catch (Exception)
        {
            return null;
            throw;
        }
    }

    

    
}
