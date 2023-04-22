using System.ComponentModel.DataAnnotations;

namespace ResortMan.Entities;

public class Booking
{
	public int Id { get; set; }
	public string FullName { get; set; } = null!;
	public string Email { get; set; } = null!;

	public int AccomodationId { get; set; }
	public Accomodation Accomodation { get; set; } = null!;

	public DateTime FromDate { get; set; }
	public int Duration { get; set; }

	[DataType(DataType.PhoneNumber)]
	public string PhoneNumber { get; set; } = null!;

	public string? Note { get; set; }

	public BookingStatus Status { get; set; } = BookingStatus.Created;
}
