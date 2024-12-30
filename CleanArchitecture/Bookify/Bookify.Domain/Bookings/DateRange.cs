namespace Bookify.Domain.Bookings;

public class DateRange
{
    private DateRange() { }

    public DateOnly Start { get; init; }

    public DateOnly End { get; set; }

    public int LengthInDays => End.DayNumber - Start.DayNumber;

    public static DateRange Create(DateOnly start, DateOnly end)
    {
        if(start > end)
        {
            throw new ApplicationException("End data precedes start date.");
        }

        return new DateRange
        {
            Start = start,
            End = end
        };
    }
}
