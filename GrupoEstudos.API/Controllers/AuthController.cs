using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Fluent;
using GrupoEstudos.Application.DTOs.Usuario;
using GrupoEstudos.API.Controllers.Shared;
using GrupoEstudos.API.Models;
using GrupoEstudos.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GrupoEstudos.API.Controllers;

[Route("api/[controller]")]
public class AuthController : ApiController
{
    private readonly IConfiguration _configuration;    
    private readonly IUsuarioService _usuarioService;


    public AuthController(IConfiguration configuration, IUsuarioService usuarioService)
    {
        _configuration = configuration;
        _usuarioService = usuarioService;
    }
    

    [HttpPost("LoginUser")]
    [AllowAnonymous]
    public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel userInfo, CancellationToken cancellationToken)
    {    
        var usuario = await _usuarioService.GetUsuario(userInfo.Email, userInfo.Password, cancellationToken);

        if (usuario != null)
        {
            return GenerateToken(usuario);
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Falha no login!");

            userInfo.Password = "*****";
            
            string strJson = JsonConvert.SerializeObject(
                                new
                                {
                                    jsonCreditApplication = userInfo
                                });

            var logger = LogManager.GetCurrentClassLogger();
            logger.Info()
                .LoggerName("AuthController.LoginUser")
                .Level(NLog.LogLevel.Warn)
                .Message("FALHA NO LOGIN! Email ou senha inválido")
                .Property("Erropayload", strJson)
                .Write();

            return BadRequest(ModelState);
        }
    }
    
    private UserToken GenerateToken(UsuarioDTO? userInfo)
    {
        IList<Claim> Claims22 = new List<Claim>();
        Claims22.Add(new Claim("email", userInfo.Email));
        Claims22.Add(new Claim("nome", userInfo.Nome));
        Claims22.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        //userInfo.Permissoes.ToList().ForEach(x =>
        //{
        //    Claims22.Add(new Claim("role", x));
        //});

        //gerar chave privada para assinar o token
        var privateKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

        //gerar a assinatura digital
        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

        //definir o tempo de expiração
        var expiration = DateTime.UtcNow.AddMinutes(120);

        //gerar o token
        JwtSecurityToken token = new JwtSecurityToken(
            //emissor
            issuer: _configuration["Jwt:Issuer"],
            //audiencia
            audience: _configuration["Jwt:Audience"],
            //claims
            //claims: claims,
            claims: Claims22,
            //data de expiracao
            expires: expiration,
            //assinatura digital
            signingCredentials: credentials
            );


        return new UserToken()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration,
            Mensagem = String.Concat("Seja bem vindo ", userInfo.Nome),
            Id = userInfo.Id,
            Nome = userInfo.Nome
        };
    }

    
}
