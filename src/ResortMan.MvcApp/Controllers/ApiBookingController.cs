using Microsoft.AspNetCore.Mvc;
using ResortMan.Entities;
using ResortMan.MvcApp.ViewModels;
using ResortMan.Services;

namespace ResortMan.Controllers
{
    [ApiController]
    [Route("api/Booking")]
    public class ApiBookingController : ControllerBase
    {
        private readonly BookingService _bookingService;

        public ApiBookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public ActionResult<Booking> CreateBooking([FromForm] BookingCreateDto dto)
        {
            var booking = new Booking()
            {
                FullName = dto.FullName,
                Email = dto.Email,
                AccomodationId = dto.AccomodationId,
                FromDate = dto.FromDate,
                PhoneNumber = dto.PhoneNumber,
                Duration = dto.Duration,
                Note = dto.Note
            };
            var createdBooking = _bookingService.CreateBooking(booking);

            if (createdBooking == null)
            {
                return BadRequest("Invalid booking data.");
            }

            return RedirectToAction("Index", "Booking", new
            {
                UserEmail = createdBooking.Email,
                PhoneNumber = createdBooking.PhoneNumber
            });
        }

        [HttpGet("{id}")]
        public ActionResult<Booking> GetBookingById(int id)
        {
            var booking = _bookingService.GetBookingById(id);

            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        [HttpGet("accomodation/{accomodationId}")]
        public ActionResult<Booking> GetBookingsByAccomodationId(int accomodationId)
        {
            var bookings = _bookingService.GetBookingsByAccomodationId(accomodationId);

            if (bookings == null || bookings.Count == 0)
            {
                return NotFound();
            }

            return Ok(bookings);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteBooking(int id)
        {
            if (_bookingService.DeleteBooking(id))
            {
                return Ok();
            }

            return NotFound();
        }
    }
}