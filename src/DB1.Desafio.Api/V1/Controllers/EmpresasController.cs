using DB1.Desafio.Application.Commands.Empresa.Criar;
using DB1.Desafio.Application.Commands.Empresa.Editar;
using DB1.Desafio.Application.Commands.Empresa.Excluir;
using DB1.Desafio.Application.Commands.Empresa.ObterComFiltro;
using DB1.Desafio.Application.Commands.Empresa.ObterPorId;
using DB1.Desafio.Application.Commands.Empresa.ObterTodos;
using DB1.Desafio.Application.Filtros;
using DB1.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DB1.Desafio.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EmpresasController : MainController
    {
        [HttpGet]
        [SwaggerOperation(Summary = "Obtém a lista com todos os registros")]
        [ProducesResponseType(typeof(ObterTodosEmpresaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundObjectResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromServices] IObterTodosEmpresaUseCase useCase)
        {
            var response = await useCase.ExecutarAsync();
            return CustomResponse(response);
        }

        [HttpGet("{id:guid}")]
        [SwaggerOperation(Summary = "Obtém o registro por id")]
        [ProducesResponseType(typeof(ObterPorIdEmpresaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundObjectResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromServices] IObterPorIdEmpresaUseCase useCase, Guid id)
        {
            var response = await useCase.ExecutarAsync(id);
            return CustomResponse(response);
        }

        [HttpGet("pesquisar")]
        [SwaggerOperation(Summary = "Obtém a lista dos registros por filtro")]
        [ProducesResponseType(typeof(ObterComFiltroEmpresaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundObjectResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(
            [FromServices] IObterComFiltroEmpresaUseCase useCase,
            [FromQuery] EmpresaFiltro filtro)
        {
            var response = await useCase.ExecutarAsync(filtro);
            return CustomResponse(response);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Adiciona novo registro")]
        [ProducesResponseType(typeof(CriarEmpresaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundObjectResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(
            [FromServices] ICriarEmpresaUseCase useCase,
            [FromBody] CriarEmpresaRequest request)
        {
            var response = await useCase.ExecutarAsync(request);
            return CustomResponse(response);
        }

        [HttpPut("{id:guid}")]
        [SwaggerOperation(Summary = "Atualiza registro com base no id")]
        [ProducesResponseType(typeof(EditarEmpresaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundObjectResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(
            [FromServices] IEditarEmpresaUseCase useCase,
            Guid id,
            [FromBody] EditarEmpresaRequest request)
        {
            var response = await useCase.ExecutarAsync(id, request);
            return CustomResponse(response);
        }

        [HttpDelete("{id:guid}")]
        [SwaggerOperation(Summary = "Remove registro com base no id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundObjectResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(
            [FromServices] IExcluirEmpresaUseCase useCase, Guid id)
        {
            var response = await useCase.ExecutarAsync(id);
            return CustomResponse(response);
        }
    }
}
