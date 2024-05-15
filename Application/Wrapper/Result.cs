using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T Value { get; set; }
        public string Error { get; set; }

        public static Task<Result<T>> SuccessAsync(T value)
        {
            return Task.FromResult(new Result<T> { IsSuccess = true, Value = value });
        }

        public static Task<Result<T>> FailureAsync(string error)
        {
            return Task.FromResult(new Result<T> { IsSuccess = false, Error = error });
        }
    }
}
