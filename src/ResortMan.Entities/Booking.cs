namespace ResortMan.Entities;

public class Booking
{
    public int Id { get; set; }

    public int AccomodationId { get; set; }

    public Accomodation Accomodation { get; set; } = null!;

    public DateTime FromDate { get; set; }

    public int Duration { get; set; }
}
