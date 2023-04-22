using Microsoft.AspNetCore.Mvc.ModelBinding;
using ResortMan.Entities;
using System.ComponentModel.DataAnnotations;

namespace ResortMan.MvcApp.ViewModels;

public class BookingIndexViewModel
{
    public BookingFilter Filter { get; set; } = null!;

    [BindNever]
    public ICollection<Booking>? Bookings { get; set; }
}

public class BookingFilter
{
    [DataType(DataType.EmailAddress)]
    public string UserEmail { get; set; } = null!;

    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; } = null!;
}