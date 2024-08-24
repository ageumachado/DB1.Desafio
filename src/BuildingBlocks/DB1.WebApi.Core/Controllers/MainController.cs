using DB1.Core.Communication;
using DB1.Core.Data;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DB1.WebApi.Core.Controllers
{
    /// <summary>
    /// Base controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected ICollection<string> Erros = new List<string>();

        private ActionResult CustomResponseBadRequest()
        {
            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "messages", Erros.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(object? result = null)
        {
            if (OperacaoValida())
            {
                return Ok(result);
            }

            return CustomResponseBadRequest();
        }

        protected async Task<IActionResult> CustomResponse(IUnitOfWork uow, object? result = null)
        {
            if (!await uow.Commit())
                AdicionarErroProcessamento("Ocorreu um erro ao persistir os dados.");
            return CustomResponse(result);
        }

        protected ActionResult CustomResponseFile<T>(ResponseResult<T> response, byte[] content, string contentType, string? fileName = null)// where T : class
        {
            ResponseProcessarErros(response?.Result);
            if (OperacaoValida())
            {
                return File(content, contentType, fileName);
            }
            return CustomResponseBadRequest();
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ResponseResult resposta)
        {
            ResponseProcessarErros(resposta);
            return CustomResponse(resposta.Data);
        }

        protected ActionResult CustomResponse<T>(ResponseResult<T> response)
        {
            ResponseProcessarErros(response.Result);
            return CustomResponse(response.Data);
        }

        protected void ResponseProcessarErros(ResponseResult? resposta)
        {
            if (resposta == null || resposta.EhValido()) return;

            foreach (var mensagem in resposta.GetErros())
            {
                AdicionarErroProcessamento(mensagem);
            }
        }

        protected bool OperacaoValida() => !Erros.Any();

        protected void AdicionarErroProcessamento(string erro)
        {
            Erros.Add(erro);
        }

        protected void LimparErrosProcessamento()
        {
            Erros.Clear();
        }
    }
}
