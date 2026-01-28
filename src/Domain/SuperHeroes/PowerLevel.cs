using Domain.Common;
using Domain.Exceptions;

namespace Domain.SuperHeroes;

public sealed class PowerLevel : ValueObject
{
    public int Value { get; }

    private PowerLevel(int value)
    {
        if (value is < 1 or > 100)
            throw new DomainException("Power level must be between 1 and 100");

        Value = value;
    }

    public static PowerLevel From(int value)
        => new(value);

    public bool IsGodTier => Value >= 95;
    public bool IsHigh     => Value >= 80;
    public bool IsNormal   => Value < 80;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();
}
