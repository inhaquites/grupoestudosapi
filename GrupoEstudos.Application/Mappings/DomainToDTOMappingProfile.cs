using AutoMapper;

using GrupoEstudos.Application.DTOs.Usuario;
using GrupoEstudos.Domain.Entities;

namespace GrupoEstudos.Application.Mappings;

public class DomainToDTOMappingProfile : Profile
{
    public DomainToDTOMappingProfile()
    {
        CreateMap<Usuario, UsuarioSimplificadoDTO>();

        
        


    }
}
