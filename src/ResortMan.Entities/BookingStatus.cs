namespace ResortMan.Entities;

[Flags]
public enum BookingStatus
{
	Created = 0,
	Confirmed = 1,
	CheckedIn = 2,
	CheckOut = 4,
	Paid = 8,
}
