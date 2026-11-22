using Library.Domain.Exceptions;
using Library.Domain.Results;
namespace Library.Application.Services;
public abstract class ServiceBase
{
    protected static ResultDto<T> Run<T>(Func<T> action, string message = "")
    {
        try { return ResultDto<T>.Ok(action(), message); }
        catch (DomainException ex) { return ResultDto<T>.Fail(ex.Message); }
    }
}
