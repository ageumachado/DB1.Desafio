using DB1.Desafio.Application.Commands.Cargo.Criar;
using DB1.Desafio.Application.Commands.Cargo.ObterPorId;
using DB1.Desafio.Application.Commands.Cargo.ObterTodos;
using DB1.Desafio.Application.Commands.Cargo.Editar;
using DB1.Desafio.Application.Commands.Cargo.Excluir;
using DB1.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DB1.Desafio.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CargosController : MainController
    {
        [HttpGet]
        [SwaggerOperation(Summary = "Obtém a lista com todos os registros")]
        [ProducesResponseType(typeof(ObterTodosCargoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundObjectResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromServices] IObterTodosCargoUseCase useCase)
        {
            var response = await useCase.ExecutarAsync();
            return CustomResponse(response);
        }

        [HttpGet("{id:guid}")]
        [SwaggerOperation(Summary = "Obtém o registro por id")]
        [ProducesResponseType(typeof(ObterPorIdCargoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundObjectResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromServices] IObterPorIdCargoUseCase useCase, Guid id)
        {
            var response = await useCase.ExecutarAsync(id);
            return CustomResponse(response);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Adiciona novo registro")]
        [ProducesResponseType(typeof(CriarCargoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundObjectResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(
            [FromServices] ICriarCargoUseCase useCase,
            [FromBody] CriarCargoRequest request)
        {
            var response = await useCase.ExecutarAsync(request);
            return CustomResponse(response);
        }

        [HttpPut("{id:guid}")]
        [SwaggerOperation(Summary = "Atualiza registro com base no id")]
        [ProducesResponseType(typeof(EditarCargoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundObjectResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(
            [FromServices] IEditarCargoUseCase useCase,
            Guid id,
            [FromBody] EditarCargoRequest request)
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
            [FromServices] IExcluirCargoUseCase useCase, Guid id)
        {
            var response = await useCase.ExecutarAsync(id);
            return CustomResponse(response);
        }
    }
}
