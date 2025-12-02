namespace Bookify.Domain.Abstractions;

public record Error(string Code, string Name)
{
    public static Error None = new Error(string.Empty, string.Empty);

    public static Error NullVal = new Error("Error.NullValue", "Null value was provided.");
};