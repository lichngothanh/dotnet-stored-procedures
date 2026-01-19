namespace Domain.Exceptions;

public class BadRequestException : DomainException
{
    public BadRequestException() : base() { }
    public BadRequestException(string message) : base(message) { }
}