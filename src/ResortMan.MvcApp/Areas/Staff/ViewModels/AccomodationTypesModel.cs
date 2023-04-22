using ResortMan.Entities;
using System.ComponentModel.DataAnnotations;

namespace ResortMan.MvcApp.Areas.Staff.ViewModels;

public class BookingListingModel
{
   public IEnumerable<Booking>Bookings { get; set; }
}
public class BookingActionModel
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }

    public int AccomodationId { get; set; }

    public Accomodation Accomodation { get; set; } = null!;

    public DateTime FromDate { get; set; }

    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }

    public int Duration { get; set; }
    public string? Note { get; set; }
}
