using ResortMan.Entities;
using System.ComponentModel.DataAnnotations;

namespace ResortMan.MvcApp.ViewModels;

public class BookingCreateDto
{
    public string FullName { get; set; }
    public string Email { get; set; }

    public int AccomodationId { get; set; }

    public DateTime FromDate { get; set; }

    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }

    public int Duration { get; set; }
    public string? Note { get; set; }
}
