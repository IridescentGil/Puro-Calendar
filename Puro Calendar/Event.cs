namespace Puro_Calendar;

public enum WrestingPromotion
{
    WWE,
    AEW,
    NJPW,
    ROH,
    Impact,
    MLW,
    AAA,
    CMLL,
    Stardom,
    Noah,
    DragonGate,
    TJPW,
    DDT,
    Other
}
public record class Event
{
    public WrestingPromotion Promotion { get; init; }
    public string Title { get; init; }
    public DateTimeOffset Date { get; init; }
    public string Location { get; init; }
    public string Description { get; init; }

    public Event(WrestingPromotion promotion, string title, DateTimeOffset date, string location, string description)
    {
        Promotion = promotion;
        Title = title;
        Date = date;
        Location = location;
        Description = description;
    }
}
