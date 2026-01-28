using Domain.Common;
using Domain.Exceptions;

namespace Domain.SuperHeroes;

public sealed class RealName : ValueObject
{
    private string Value { get; }

    private RealName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Real name is required");

        value = value.Trim();

        if (value.Length < 2)
            throw new DomainException("Real name must be at least 2 characters");

        if (value.Length > 150)
            throw new DomainException("Real name must be 150 characters or less");

        Value = value;
    }

    public static RealName From(string value)
        => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
