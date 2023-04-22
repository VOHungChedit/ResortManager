namespace ResortMan.Entities;

[Flags]
public enum BookingStatus
{
	Created = 0,
	Paid = 1,
	CheckedIn = 2,
	CheckOut = 4,
}
