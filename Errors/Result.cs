
namespace EMC.BuildingBlocks.Errors
{
    public class Result<T>
    {
        public bool IsSuccess { get; }

        public T? Value { get; }

        public List<ErrorResult> Errors { get; }
        public List<string> Remarks { get; }

        private Result(bool success, T? value, List<ErrorResult>? errors, List<string>? remarks = null)
        {
            IsSuccess = success;
            Value = value;
            Errors = errors ?? new List<ErrorResult>();
            Remarks = remarks ?? new List<string>();
        }

        public static Result<T> Success(T value, List<string>? remarks = null)
                                => new Result<T>(true, value, null, remarks);


        public static Result<T> Failure(List<ErrorResult> errors)
        {
            return new Result<T>(false, default, errors);
        }

        public static Result<T> Failure(ErrorResult error)
        {
            return new Result<T>(false, default, new List<ErrorResult> { error });
        }

        public static Result<T> Failure(int code, string message)
        {
            return new Result<T>(false, default,
                new List<ErrorResult>
                {
                    new ErrorResult
                    {
                        Code = code,
                        Message = message
                    }
                });
        }

        public static Result<T> Failure(ErrorDefinition definition)
        {
            return new Result<T>(
                false,
                default,
                new List<ErrorResult> { ErrorResult.FromDefinition(definition) }
            );
        }

        public static Result<T> Failure(List<ErrorDefinition> definitions)
        {
            return new Result<T>(
                false,
                default,
                definitions.Select(ErrorResult.FromDefinition).ToList()
            );
        }
    }
    public class ErrorResult
    {
        public int Code { get; set; }

        public string Message { get; set; } = string.Empty;

        public static ErrorResult FromDefinition(ErrorDefinition definition)
        {
            return new ErrorResult
            {
                Code = definition.Code,
                Message = definition.Message
            };
        }

        public static ErrorResult Create(int code, string message)
        {
            return new ErrorResult
            {
                Code = code,
                Message = message
            };
        }
    }

}
