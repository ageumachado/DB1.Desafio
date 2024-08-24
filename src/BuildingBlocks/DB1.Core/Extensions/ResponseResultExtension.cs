using DB1.Core.Communication;
using FluentValidation.Results;

namespace DB1.Core.Extensions
{
    public static class ResponseResultExtension
    {
        public static ResponseResult ToResponseResult(this ValidationResult validationResult)
            => new(validationResult.ToErrorMessages());

        public static ResponseResult ToResponseResult(this ValidationResult validationResult, object response)
            => new(validationResult.ToErrorMessages(), response);

        public static ResponseResult<T> ToResponseResult<T>(this ValidationResult validationResult, T response)// where T : class, new()
            => new(response, validationResult.ToResponseResult());

        public async static Task<ResponseResult<IEnumerable<T>>> ToResponseResult<T>(this Task<IEnumerable<T>> response)
            => new(await response, null);

        public static ResponseResult<IEnumerable<T>> ToResponseResult<T>(this IEnumerable<T> response)
            => new(response, null);

        public static ResponseResult<T> ToResponseResult<T>(this T response)
            => new(response, null);
    }
}
