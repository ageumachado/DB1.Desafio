using DB1.Core.Communication;
using DB1.Core.Extensions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace DB1.Core.Messages
{
    public abstract class Command : Message, IRequest<ResponseResult>
    {
        [JsonIgnore]
        public DateTime Timestamp { get; private set; }
        [JsonIgnore]
        public ValidationResult? ValidationResult { get; set; }

        public ResponseResult ToResponseResultError() => ValidationResult is null
                                                         ? new ResponseResult(Enumerable.Empty<string>())
                                                         : ValidationResult.ToResponseResult();
        public abstract void AddValidationResult();

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : class
        {
            ValidationResult = validacao.Validate(entidade);

            if (ValidationResult.IsValid) return true;

            return false;
        }

        public bool EhValido()
        {
            AddValidationResult();
            return ValidationResult?.IsValid ?? true;
        }
        public bool EhInvalido() => !EhValido();
    }
}
