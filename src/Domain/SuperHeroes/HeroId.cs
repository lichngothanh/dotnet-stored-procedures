using Domain.Common;
using Domain.Exceptions;

namespace Domain.SuperHeroes;

public sealed class HeroId : ValueObject
{
    public Guid Value { get; }

    private HeroId(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("HeroId cannot be empty");

        Value = value;
    }

    public static HeroId From(Guid value)
        => new(value);

    public static HeroId NewId()
        => new(Guid.NewGuid());

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();
}