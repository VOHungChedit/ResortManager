using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using ResortMan.Entities;
using ResortMan.MvcApp.Areas.Dashboard.ViewModels;
using ResortMan.MvcApp.Areas.Staff.ViewModels;
using ResortMan.Services;

namespace ResortMan.MvcApp.Areas.Staff.Controllers;

[Area("Staff")]
public class BookingsController : Controller
{

    public BookingsController(BookingService bookingsService)
    {
        this.bookingsService = bookingsService;
    }

    public IActionResult Index()
    {
        return Ok("1");
    }

    private readonly BookingService bookingsService;


    public IActionResult Listing()
    {
        var model = new BookingListingModel();
        model.Bookings = bookingsService.GetBookings();
        return PartialView("_Listing", model);
    }
    public IActionResult Index(string? searchTerm)
    {
        BookingListingModel model;
        if (searchTerm != null)
        {
            model = new BookingListingModel();

            model.Bookings = bookingsService.SearchBooking(searchTerm);

            return View(model);
        }

        var list = bookingsService.GetBookings();
        model = new BookingListingModel()
        {
            Bookings = list,
        };
        return View(model);
        //return View();
    }

    [HttpGet]
    public ActionResult Action(int? Id)
    {
        var model = new BookingActionModel();

        if (Id.HasValue)
        {
            var booking = bookingsService.GetBookingById(Id.Value);

            model.Id = booking.Id;
            model.FullName = booking.FullName;
            model.Email = booking.Email;
            model.PhoneNumber = booking.PhoneNumber;
            model.Duration = booking.Duration;
            model.Note = booking.Note;
            model.FromDate = booking.FromDate;
            model.AccomodationId = booking.AccomodationId;

        }

        return PartialView("_Action", model);
    }

    [HttpPost]
    public JsonResult Action(BookingActionModel model)
    {
        var result = false;

        var booking = bookingsService.GetBookingById(model.Id);

        booking.FullName = model.FullName;
        booking.Email = model.Email;
        booking.PhoneNumber = model.PhoneNumber;
        booking.Duration = model.Duration;
        booking.Note = model.Note;
        booking.FromDate = model.FromDate;
        booking.AccomodationId = model.AccomodationId;

        result = bookingsService.UpdateBooking(booking);

        object json;
        if (result)
        {
            json = new { Success = true };

        }
        else
        {
            json = new { Success = false, Message = "Unable to perform action on Booking." };
        }

        return Json(json);

    }

    [HttpGet]
    public ActionResult Delete(int Id)
    {
        BookingActionModel model = new BookingActionModel();

        var booking = bookingsService.GetBookingById(Id);

        model.Id = booking.Id;

        return PartialView("_Delete", model);
    }

    [HttpDelete]
    [ActionName("Delete")]
    public JsonResult DeleteConfirm(int Id)
    {
        var result = false;

        var booking = bookingsService.GetBookingById(Id);

        result = bookingsService.DeleteBooking(booking.Id);
        object json;
        if (result)
        {
            json = new { Success = true };

        }
        else
        {
            json = new { Success = false, Message = "Unable to perform action on Accomodation Types." };
        }

        return Json(json);

    }
}
