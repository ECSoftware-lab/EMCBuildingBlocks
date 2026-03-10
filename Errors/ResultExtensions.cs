using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMC.BuildingBlocks.Errors
{
    public static class ResultExtensions
    {
        public static Result<TOut> PropagateFailure<TIn, TOut>(this Result<TIn> result)
        {
            return Result<TOut>.Failure(result.Errors);
        }
        public static Result<T> AddError<T>(this Result<T> result, ErrorResult error)
        {
            var errors = result.Errors.ToList();
            errors.Add(error);
            return Result<T>.Failure(errors);
        }
    }
}
