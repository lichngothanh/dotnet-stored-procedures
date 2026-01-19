namespace Domain.Exceptions;

public class NotFoundException : DomainException
{
    public NotFoundException() : base() { }
    public NotFoundException(string message) : base(message) { }
}
