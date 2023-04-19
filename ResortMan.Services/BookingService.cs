using ResortMan.Data;
using ResortMan.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ResortMan.Services
{
    public class BookingService
    {
        private readonly ApplicationDbContext _context;
        private readonly AccomodationsService _accomodationService;

        public BookingService(ApplicationDbContext context, AccomodationsService accomodationService)
        {
            _context = context;
            _accomodationService = accomodationService;
        }

        public Booking? CreateBooking(Booking booking)
        {

            var accomodation = _accomodationService.GetAccomodationById(booking.AccomodationId);
            if (accomodation == null)
            {
                return null;
            }


            //var endDate = booking.FromDate.AddDays(booking.Duration);
            //if (enddate > accomodation.accomodationpackage)
            //{
            //    return null;
            //}


            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return booking;
        }

        public Booking GetBookingById(int id)
        {
            return _context.Bookings.FirstOrDefault(b => b.Id == id);
        }

        public List<Booking> GetBookingsByAccomodationId(int accomodationId)
        {
            return _context.Bookings.Where(b => b.AccomodationId == accomodationId).ToList();
        }

        public bool DeleteBooking(int id)
        {
            var booking = GetBookingById(id);

            if (booking == null)
            {
                return false;
            }

            _context.Bookings.Remove(booking);
            _context.SaveChanges();

            return true;
        }
    }
}