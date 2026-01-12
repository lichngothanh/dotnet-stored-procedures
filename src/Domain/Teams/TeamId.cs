using Domain.Common;
using Domain.Exceptions;

namespace Domain.Teams;

public sealed class TeamId : ValueObject
{
    private Guid Value { get; }

    private TeamId(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("TeamId cannot be empty");

        Value = value;
    }

    public static TeamId From(Guid value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();
}