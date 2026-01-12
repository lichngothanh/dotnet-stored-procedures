using Domain.Exceptions;

namespace Domain.Common;

public sealed class Universe : ValueObject
{
    private string Code { get; }

    private Universe(string code)
    {
        Code = code;
    }

    private static readonly Universe Marvel = new("Marvel");
    private static readonly Universe Dc     = new("DC");

    public static Universe From(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Universe cannot be empty");

        return value.Trim() switch
        {
            "Marvel" => Marvel,
            "DC"     => Dc,
            _ => throw new DomainException($"Unknown Universe: {value}")
        };
    }

    public bool IsMarvel => Equals(this, Marvel);
    public bool IsDc     => Equals(this, Dc);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }

    public override string ToString() => Code;
}
