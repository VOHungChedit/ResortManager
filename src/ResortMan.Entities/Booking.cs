﻿using System.ComponentModel.DataAnnotations;

namespace ResortMan.Entities;

public class Booking
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }

    public int AccomodationId { get; set; }
    
    public Accomodation Accomodation { get; set; } = null!;

    public DateTime FromDate { get; set; }

    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }

    public int Duration { get; set; }
    public string? Note { get; set; }
}
