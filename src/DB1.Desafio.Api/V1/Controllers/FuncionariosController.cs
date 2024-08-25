using DB1.Desafio.Application.Commands.Funcionario.Criar;
using DB1.Desafio.Application.Commands.Funcionario.Editar;
using DB1.Desafio.Application.Commands.Funcionario.Excluir;
using DB1.Desafio.Application.Commands.Funcionario.ObterComFiltro;
using DB1.Desafio.Application.Commands.Funcionario.ObterPorId;
using DB1.Desafio.Application.Commands.Funcionario.ObterTodos;
using DB1.Desafio.Application.Filtros;
using DB1.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DB1.Desafio.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FuncionariosController : MainController
    {

        [HttpGet]
        [SwaggerOperation(Summary = "Obtém a lista com todos os registros")]
        [ProducesResponseType(typeof(ObterTodosFuncionarioResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundObjectResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromServices] IObterTodosFuncionarioUseCase useCase)
        {
            var response = await useCase.ExecutarAsync();
            return CustomResponse(response);
        }

        [HttpGet("{id:guid}")]
        [SwaggerOperation(Summary = "Obtém o registro por id")]
        [ProducesResponseType(typeof(ObterPorIdFuncionarioResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundObjectResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromServices] IObterPorIdFuncionarioUseCase useCase, Guid id)
        {
            var response = await useCase.ExecutarAsync(id);
            return CustomResponse(response);
        }

        [HttpGet("pesquisar")]
        [SwaggerOperation(Summary = "Obtém a lista dos registros por filtro")]
        [ProducesResponseType(typeof(ObterComFiltroFuncionarioResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundObjectResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(
            [FromServices] IObterComFiltroFuncionarioUseCase useCase,
            [FromQuery] FuncionarioFiltro filtro)
        {
            var response = await useCase.ExecutarAsync(filtro);
            return CustomResponse(response);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Adiciona novo registro")]
        [ProducesResponseType(typeof(CriarFuncionarioResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundObjectResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(
            [FromServices] ICriarFuncionarioUseCase useCase,
            [FromBody] CriarFuncionarioRequest request)
        {
            var response = await useCase.ExecutarAsync(request);
            return CustomResponse(response);
        }

        [HttpPut("{id:guid}")]
        [SwaggerOperation(Summary = "Atualiza registro com base no id")]
        [ProducesResponseType(typeof(EditarFuncionarioResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundObjectResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(
            [FromServices] IEditarFuncionarioUseCase useCase,
            Guid id,
            [FromBody] EditarFuncionarioRequest request)
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
            [FromServices] IExcluirFuncionarioUseCase useCase, Guid id)
        {
            var response = await useCase.ExecutarAsync(id);
            return CustomResponse(response);
        }
    }
}
