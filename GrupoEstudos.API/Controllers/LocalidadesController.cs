using GrupoEstudos.API.Attributes;
using GrupoEstudos.API.Controllers.Shared;
using GrupoEstudos.Application.DTOs.Localidade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GrupoEstudos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LocalidadesController : ApiController
    {

        private readonly IHttpClientFactory _clientFactory;

        public LocalidadesController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        /// <summary>
        /// Retorna uma tarefa por id.
        /// </summary>
        /// <param name="id">Id da tarefa</param>
        /// <returns>Retorna uma tarefa por id</returns>
        /// <response code="200">Retorna a tarefa encontrada</response>
        /// <response code="400">Se o id passado for nulo ou inexistente</response>
        [CustomResponse(StatusCodes.Status200OK)]
        [CustomResponse(StatusCodes.Status400BadRequest)]        
        [HttpGet]
        public async Task<IActionResult> GetEstados(CancellationToken cancellationToken)
        {           

            var client = _clientFactory.CreateClient();


            var url = "https://servicodados.ibge.gov.br/api/v1/localidades/estados";

            var result = new List<EstadoDTO>();

            var response = await client.GetAsync(url, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                result = System.Text.Json.JsonSerializer.Deserialize<List<EstadoDTO>>
                    (stringResponse, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return Ok(result);

        }
    }
}
