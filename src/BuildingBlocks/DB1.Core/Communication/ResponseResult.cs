using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB1.Core.Communication
{
    public class ResponseResult
    {
        public static ResponseResult RetornoOk => new();

        private ResponseResult() : this(null, null) { }

        public ResponseResult(IEnumerable<string> errors) : this(errors, null)
        {
        }

        public ResponseResult(IEnumerable<string>? errors, object? data)
        {
            if (errors is not null && errors.Any())
            {
                Errors = new ResponseErrorMessages();
                AddError(errors);
            }
            Data = data;
        }

        public object? Data { get; private set; }

        private ResponseErrorMessages? Errors { get; }
        public IEnumerable<string> GetErros() => Errors?.Messages.Select(s => s).ToList() ?? Enumerable.Empty<string>();

        public void AddError(string errorMessage) => AddError(new string[] { errorMessage });
        public void AddError(IEnumerable<string> errorMessages) => Errors?.Messages.AddRange(errorMessages);

        public bool EhInvalido() => Errors?.Messages?.Any() ?? false;
        public bool EhValido() => !EhInvalido();
    }


    public class ResponseResult<TValue>
    {
        public TValue? Data { get; private set; }
        public ResponseResult? Result { get; private set; }

        public ResponseResult(TValue data, ResponseResult? result)
        {
            Data = data;
            Result = result;
        }

        public ResponseResult(ResponseResult result)
        {
            Result = result;
        }

        public static implicit operator ResponseResult<TValue>(TValue value)
        {
            return new ResponseResult<TValue>(value, null);
        }

        public static implicit operator ResponseResult<TValue>(ResponseResult result)
        {
            return new ResponseResult<TValue>(result);
        }

        public static implicit operator ResponseResult(ResponseResult<TValue> result)
        {
            if (result == null) return new ResponseResult(null, null);

            return new ResponseResult(result.Result?.GetErros(), result.Data);
        }

        public bool EhInvalido() => Result?.EhInvalido() ?? false;
        public bool EhValido() => !EhInvalido();

        public IEnumerable<string> GetErros()
        {
            if (Result == null) return Enumerable.Empty<string>();
            return Result.GetErros();
        }
    }

    public class ResponseErrorMessages
    {
        public ResponseErrorMessages()
        {
            Messages = new List<string>();
        }

        public List<string> Messages { get; set; }
    }
}
