using Domain.Common;
using Domain.Exceptions;

namespace Domain.SuperHeroes;

public sealed class HeroName : ValueObject
{
    private string Value { get; }

    private HeroName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Hero name is required");

        value = value.Trim();

        if (value.Length < 2)
            throw new DomainException("Hero name must be at least 2 characters");

        if (value.Length > 100)
            throw new DomainException("Hero name must be 100 characters or less");

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
