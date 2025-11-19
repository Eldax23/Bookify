
namespace Bookify.Domain.Shared.Records;

public record Money(decimal Amount, Currency currency)
{
    public static Money operator +(Money first, Money second)
    {
        if (first.currency != second.currency)
        {
            throw new InvalidOperationException("Currencies don't match");
        }

        return new Money(first.Amount + second.Amount, second.currency);
    }

    public static Money Zero() => new Money(0, Currency.None);
    public static Money Zero(Currency currency) => new Money(0, currency);

    public bool IsZero() => this == Zero();
};