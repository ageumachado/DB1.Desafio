using DB1.Desafio.Application.Commands.Empresa.ObterTodos;
using DB1.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DB1.Desafio.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EmpresasController : MainController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromServices] IObterTodosEmpresaUseCase useCase)
        {
            var response = await useCase.ExecutarAsync();
            return CustomResponse(response);
        }
    }
}
