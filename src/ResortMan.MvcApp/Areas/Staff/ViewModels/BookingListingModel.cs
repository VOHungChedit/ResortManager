using ResortMan.Entities;

namespace ResortMan.MvcApp.Areas.Staff.ViewModels;

public class BookingListingModel
{
	public IEnumerable<Booking> Bookings { get; set; } = null!;
}
