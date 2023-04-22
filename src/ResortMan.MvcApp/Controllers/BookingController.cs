using Microsoft.AspNetCore.Mvc;
using ResortMan.MvcApp.ViewModels;
using ResortMan.Services;

namespace ResortMan.MvcApp.Controllers;

public class BookingController : Controller
{
    private readonly BookingService bookingService;

    public BookingController(BookingService bookingService)
    {
        this.bookingService = bookingService;
    }
    public IActionResult Index(BookingFilter filter)
    {
        var model = new BookingIndexViewModel
        {
            Filter = filter,
        };

        if (ModelState.IsValid)
        {
            model.Bookings = bookingService.GetBookingsForUser(filter.PhoneNumber, filter.UserEmail);
        }

        return View(model);
    }
}
