namespace Library.Domain.Results;
public class ResultDto<T>
{
    public bool IsSuccess { get; private set; }
    public string Message { get; private set; } = "";
    public T? Data { get; private set; }
    public static ResultDto<T> Ok(T data, string message = "") => new() { IsSuccess = true, Data = data, Message = message };
    public static ResultDto<T> Fail(string message) => new() { IsSuccess = false, Message = message };
}
