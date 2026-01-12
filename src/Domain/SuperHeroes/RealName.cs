using Domain.Common;
using Domain.Exceptions;

namespace Domain.SuperHeroes;

public sealed class RealName : ValueObject
{
    private string Value { get; }

    private RealName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("RealName cannot be empty");

        value = value.Trim();

        if (value.Length > 150)
            throw new DomainException("RealName must be 150 characters or less");

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
