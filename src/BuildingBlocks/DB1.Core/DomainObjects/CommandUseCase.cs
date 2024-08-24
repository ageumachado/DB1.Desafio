using DB1.Core.Communication;
using DB1.Core.Data;
using DB1.Core.Extensions;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB1.Core.DomainObjects
{
    public abstract class CommandUseCase
    {
        protected ValidationResult ValidationResult;

        protected CommandUseCase()
        {
            ValidationResult = new ValidationResult();
        }

        protected bool OperacaoValida()
        {
            return ValidationResult.IsValid;
        }

        protected void AdicionarErroProcessamento(params string[] mensagemErro)
        {
            if (mensagemErro is null || !mensagemErro.Any()) return;

            foreach (var error in mensagemErro)
            {
                ValidationResult.Errors.Add(new ValidationFailure(string.Empty, error));
            }
        }

        protected async Task<ValidationResult> PersistirDados(IUnitOfWork uow)
        {
            if (!OperacaoValida()) return ValidationResult;

            //Error ao tentar realizar cadastro
            //Ocorreu um erro ao persistir os dados
            if (!await uow.Commit())
                AdicionarErroProcessamento("Ocorreu um erro ao persistir os dados.");
            //AdicionarErroProcessamento("There was an error persisting the data.");
            return ValidationResult;
        }

        protected ResponseResult ResponseResultError()
        {
            return ValidationResult.ToResponseResult();
        }

        protected ResponseResult ResponseResultError(params string[] mensagemErro)
        {
            AdicionarErroProcessamento(mensagemErro);
            return ValidationResult.ToResponseResult();
        }

        protected ResponseResult ResponseResultError(string? message = null)
        {
            if (!string.IsNullOrEmpty(message))
                AdicionarErroProcessamento(message);
            return ValidationResult.ToResponseResult();
        }

        protected ResponseResult ResponseResultOk()
        {
            return ValidationResult.ToResponseResult();
        }
    }
}
