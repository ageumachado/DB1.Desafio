using FluentValidation.Results;

namespace DB1.Core.Extensions
{
    public static class ValidationResultExtensions
    {
        public static IEnumerable<string> ToErrorMessages(this ValidationResult validationResult)
            => validationResult.Errors.Select(x => x.ErrorMessage);
    }
}
