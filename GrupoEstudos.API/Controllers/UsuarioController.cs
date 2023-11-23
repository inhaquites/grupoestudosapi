using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GrupoEstudos.API.Attributes;
using GrupoEstudos.API.Controllers.Shared;
using GrupoEstudos.Application.Interfaces;

namespace GrupoEstudos.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsuarioController : ApiController
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    
}
