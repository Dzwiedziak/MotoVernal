namespace BusinessLogic.Services.Response
{
    public class Result<TValue, TError> where TError : Enum
    {
        public readonly TValue? Value;
        public readonly TError? Error;

        private bool _isSuccess;

        protected Result(TValue value)
        {
            Value = value;
            Error = default;
            _isSuccess = true;
        }

        protected Result(TError error)
        {
            Value = default;
            Error = error;
            _isSuccess = false;
        }

        public static implicit operator Result<TValue, TError>(TValue value) =>
            new Result<TValue, TError>(value);

        public static implicit operator Result<TValue, TError>(TError error) =>
            new Result<TValue, TError>(error);

        public Result<TValue, TError> Match(Func<TValue, Result<TValue, TError>> success, Func<TError, Result<TValue, TError>> error)
        {
            if (_isSuccess)
                return success(Value!);
            else
                return error(Error!);
        }
    }
}
