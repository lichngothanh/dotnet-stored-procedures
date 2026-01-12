using Domain.Common;
using Domain.Exceptions;

namespace Domain.SuperHeroes;

public sealed class HeroName : ValueObject
{
    private string Value { get; }

    private HeroName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("HeroName cannot be empty");

        value = value.Trim();

        if (value.Length > 100)
            throw new DomainException("HeroName must be 100 characters or less");

        Value = value;
    }

    public static HeroName From(string value)
        => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
