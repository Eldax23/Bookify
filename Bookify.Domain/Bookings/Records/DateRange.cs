namespace Bookify.Domain.Bookings.Records;

public record DateRange
{
    public DateOnly Start { get; init; }
    public DateOnly End { get; init; }
    public int LengthInDays => End.DayNumber - Start.DayNumber;


    public static DateRange Create(DateOnly start, DateOnly end)
    {
        if (start > end)
        {
            throw new ApplicationException("the end date precedes  the start date");
        }

        return new DateRange
        {
            Start = start,
            End = end,
        };
    }
};