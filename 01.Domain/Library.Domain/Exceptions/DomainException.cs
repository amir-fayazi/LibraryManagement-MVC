namespace Library.Domain.Exceptions;
public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}
public sealed class BusinessRuleException : DomainException
{
    public BusinessRuleException(string message) : base(message) { }
}
public sealed class ValidationException : DomainException
{
    public ValidationException(string message) : base(message) { }
}
public sealed class NotFoundException : DomainException
{
    public NotFoundException(string message) : base(message) { }
}
