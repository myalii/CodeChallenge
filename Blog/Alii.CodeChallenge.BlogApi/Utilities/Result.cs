namespace Alii.CodeChallenge.BlogApi.Utilities;

public class Result<T>
    {
        public required bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public ResultTypeEnum ErrorType { get; set; } = ResultTypeEnum.None;
    }