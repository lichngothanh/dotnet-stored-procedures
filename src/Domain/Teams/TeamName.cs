using Domain.Common;
using Domain.Exceptions;

namespace Domain.Teams;

public sealed class TeamName : ValueObject
{
    private string Value { get; }

    private TeamName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("HeroName cannot be empty");

        value = value.Trim();

        if (value.Length > 100)
            throw new DomainException("HeroName must be 100 characters or less");

        Value = value;
    }

    public static TeamName From(string value)
        => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}