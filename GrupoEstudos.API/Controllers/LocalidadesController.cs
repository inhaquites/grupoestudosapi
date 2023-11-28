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

        
        [CustomResponse(StatusCodes.Status200OK)]
        [CustomResponse(StatusCodes.Status400BadRequest)]        
        [HttpGet("GetEstados")]
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

            return ResponseOk(result.OrderBy(x=>x.Sigla));

        }


        [CustomResponse(StatusCodes.Status200OK)]
        [CustomResponse(StatusCodes.Status400BadRequest)]
        [HttpGet("GetMunicipios")]
        public async Task<IActionResult> GetMunicipios(string uf, CancellationToken cancellationToken)
        {
            var client = _clientFactory.CreateClient();

            var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{uf}/municipios";

            var result = new List<MunicipioDTO>();

            var response = await client.GetAsync(url, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                result = System.Text.Json.JsonSerializer.Deserialize<List<MunicipioDTO>>
                    (stringResponse, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return ResponseOk(result.OrderBy(x => x.Nome));

        }



    }
}
