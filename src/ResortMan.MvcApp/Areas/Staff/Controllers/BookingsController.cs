using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResortMan.MvcApp.Areas.Staff.ViewModels;
using ResortMan.Services;

namespace ResortMan.MvcApp.Areas.Staff.Controllers;

[Authorize(Roles = "Staff")]
[Area("Staff")]
public class BookingsController : Controller
{
	private readonly BookingService bookingsService;
	private readonly AccomodationsService accomodationsService;

	public BookingsController(BookingService bookingsService, AccomodationsService accomodationsService)
	{
		this.bookingsService = bookingsService;
		this.accomodationsService = accomodationsService;
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
	}

	[HttpGet]
	public ActionResult Action(int? id)
	{
		var model = new BookingActionModel();

		if (id == null)
			return BadRequest();

		var booking = bookingsService.GetBookingById(id.Value);

		if (booking == null)
			return NotFound();

		model.Id = booking.Id;
		model.FullName = booking.FullName;
		model.Email = booking.Email;
		model.PhoneNumber = booking.PhoneNumber;
		model.Duration = booking.Duration;
		model.Note = booking.Note;
		model.FromDate = booking.FromDate;
		model.AccomodationId = booking.AccomodationId;
		model.Accomodations = accomodationsService.GetAccomodationsByPackageId(booking.Accomodation.AccomodationPackageId);

		return PartialView(model);
	}

	[HttpPost]
	public IActionResult Action(BookingActionModel model)
	{
		var result = false;

		var booking = bookingsService.GetBookingById(model.Id);

		if (booking == null)
			return BadRequest();

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
	public ActionResult Delete(int? id)
	{
		BookingActionModel model = new BookingActionModel();

		if (id == null)
			return BadRequest();

		var booking = bookingsService.GetBookingById(id.Value);

		if (booking == null)
			return NotFound();

		model.Id = booking.Id;

		return PartialView(model);
	}

	[HttpDelete]
	[ActionName("Delete")]
	public IActionResult DeleteConfirm(int id)
	{
		var booking = bookingsService.GetBookingById(id);

		bool result;
		if (booking == null)
			result = true;
		else
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
