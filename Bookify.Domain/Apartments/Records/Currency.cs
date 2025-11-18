namespace Bookify.Domain.Apartments.Records;

public record Currency
{
    public Currency(string code) => Code = code;
    
    internal static readonly Currency None = new Currency("");
    public static readonly Currency EUR = new Currency("EUR");
    public static readonly Currency USD = new Currency("USD");
    public static readonly Currency EGP = new Currency("EGP");

    public string Code { get; init; }

    public static Currency FromCode(string code)
    {
        return AvailableCurrencies.FirstOrDefault(x => x.Code == code) ?? 
               throw new ApplicationException($"Currency code {code} not found");
    }

    public static IReadOnlyCollection<Currency> AvailableCurrencies = new[]
    {
        EUR,
        USD,
        EGP
    };
};